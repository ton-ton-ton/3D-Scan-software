using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace _3D_Scan_software
{
    public class KalmanFilter
    {
        private Matrix<double> A;  // 状态转移矩阵
        private Matrix<double> B;  // 输入矩阵
        private Matrix<double> H;  // 观测矩阵
        private Matrix<double> Q;  // 过程噪声协方差矩阵
        private Matrix<double> R;  // 观测噪声协方差矩阵
        private Matrix<double> P;  // 估计误差协方差矩阵
        private Matrix<double> x;  // 状态估计向量
        public double prevVelocity;  // 上一个时刻的速度
        public double prevPosition;  // 上一个时刻的位置
        public double prevAccelerometerValue; // 上一个时刻的加速度
        double dt;

        public KalmanFilter(double processNoise, double observationNoise)
        {
            // 初始化矩阵和向量
            A = Matrix<double>.Build.DenseOfArray(new double[,] { { 1, dt }, { 0, 1 } });
            B = Matrix<double>.Build.DenseOfArray(new double[,] { { 0.5 * dt * dt }, { dt } });
            H = Matrix<double>.Build.DenseOfArray(new double[,] { { 1, 0 } });
            Q = Matrix<double>.Build.DenseOfDiagonalArray(new double[] { processNoise, processNoise });
            R = Matrix<double>.Build.DenseOfDiagonalArray(new double[] { observationNoise });
            P = Matrix<double>.Build.DenseIdentity(2);
            x = Matrix<double>.Build.Dense(2, 1);
            prevVelocity = 0;
            prevPosition = 0;
            prevAccelerometerValue = 0;
        }

        public double Filter(double accelerometerValue, double gyroValue, double dt, double residualThreshold)
        {
           

            if (Math.Abs(gyroValue) > residualThreshold)
            {
                // 觀測殘差超過閾值，將測量值視為無效，僅更新速度
                //xPredicted[0, 0] = prevPosition; // 還原位置為上一時刻的值
                x[0, 0] = prevPosition;
                x[1, 0] = 0;
            }
            else
            {
                double predictedVelocity; double predictedPosition;

                //if (accelerometerValue > 0.02)
                ///{
                    // 預測步驟 ( 𝑝𝑖 = 𝑝𝑖−1 + 1/2×(𝑣𝑖−1+𝑣𝑖) × Δ𝑡 / 𝑣𝑖 = 𝑣𝑖−1 + 1/2 ×(𝑎N𝑖−1 +𝑎N𝑖) × Δ𝑡)
                    double predictedVelocity1 = prevVelocity + 0.5 * (accelerometerValue + prevAccelerometerValue) * dt;
                    double predictedPosition1 = prevPosition + 0.5 * (prevVelocity + predictedVelocity1) * dt;
                //}
                //else
                //{
                    // 預測步驟( 𝑝𝑘 = 𝑝𝑘−1 + 𝑣𝑘−1 Δ𝑡 + 0.5(Δ𝑡)2𝑎N𝑥, /  𝑣𝑘 = 𝑣𝑘−1 + Δ𝑡𝑎N𝑥.)
                    predictedPosition = prevPosition + prevVelocity * dt + 0.5 * dt * dt * accelerometerValue;
                    predictedVelocity = prevVelocity + dt * accelerometerValue;
                //}

                //  轉換為矩陣形式
                Matrix<double> xPredicted = Matrix<double>.Build.DenseOfArray(new double[,] { { predictedPosition }, { predictedVelocity } });
                Matrix<double> PPredicted = A * P * A.Transpose() + Q;

                // 觀測殘差
                Matrix<double> residual = Matrix<double>.Build.DenseOfArray(new double[,] { { predictedPosition1 } }) - H * xPredicted;

                // 判斷觀測殘差是否超過閾值
                double residualMagnitude = residual[0, 0];  // 觀測殘差的幅值

             
                // 觀測殘差在閾值內，進行更新步驟
                Matrix<double> S = H * PPredicted * H.Transpose() + R;
                Matrix<double> K = PPredicted * H.Transpose() * S.Inverse();

                x = xPredicted + K * residual;
                P = (Matrix<double>.Build.DenseIdentity(2) - K * H) * PPredicted;
            }

            // 更新速度和位置
            prevVelocity = x[1, 0];
            prevPosition = x[0, 0];
            prevAccelerometerValue = accelerometerValue;

            return x[0, 0];  // 返回濾波後的位置
        }

        public void getVelocity(double velo)
        {
            velo = x[0, 1];
        }
    }



}
