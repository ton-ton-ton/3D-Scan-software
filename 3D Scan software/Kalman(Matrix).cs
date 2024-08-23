using MathNet.Numerics.LinearAlgebra;
using System.Windows.Media.Media3D;

namespace _3D_Scan_software
{
    public class KalmanFilter_acc
    {
        private Matrix<double> state; // 狀態向量
        private Matrix<double> covariance; // 協方差矩陣

        private Matrix<double> transitionMatrix; // 狀態轉換矩陣
        private Matrix<double> observationMatrix; // 觀測矩陣
        private Matrix<double> processNoiseCovariance; // 過程噪聲協方差矩陣
        private Matrix<double> measurementNoiseCovariance; // 測量噪聲協方差矩陣
    

        public KalmanFilter_acc(double processNoise, double measurementNoiseCovarianceValue)
        {
            state = Matrix<double>.Build.DenseOfColumnVectors(Vector<double>.Build.DenseOfArray(new double[] { 0.0, 0.0, 0.0 }));
            covariance = Matrix<double>.Build.DenseIdentity(3) ;
            this.transitionMatrix = Matrix<double>.Build.DenseIdentity(3);
            this.observationMatrix = Matrix<double>.Build.DenseIdentity(3);
            this.processNoiseCovariance = Matrix<double>.Build.DenseIdentity(3) * processNoise;
            this.measurementNoiseCovariance = Matrix<double>.Build.DenseIdentity(3) * measurementNoiseCovarianceValue;
           

        }

        public void Update(Vector<double> measurement, Vector3D qua)
        {
            // 預測步驟
            Matrix<double> predictedState = Matrix<double>.Build.DenseOfColumnVectors(Vector<double>.Build.DenseOfArray(new double[] { qua.X, qua.Y, qua.Z}));
            Matrix<double> predictedCovariance = transitionMatrix * covariance * transitionMatrix.Transpose() + processNoiseCovariance;

            // 更新步驟
            Matrix<double> innovation = Matrix<double>.Build.DenseOfColumnVectors(measurement) - observationMatrix * predictedState;
            Matrix<double> innovationCovariance = observationMatrix * predictedCovariance * observationMatrix.Transpose() + measurementNoiseCovariance;
            Matrix<double> kalmanGain = predictedCovariance * observationMatrix.Transpose() * innovationCovariance.Inverse();

            state = predictedState + kalmanGain * innovation;
            covariance = (Matrix<double>.Build.DenseIdentity(state.RowCount) - kalmanGain * observationMatrix) * predictedCovariance;
        }

        public Vector<double> GetState()
        {
            return state.Column(0);
        }

    }
}
