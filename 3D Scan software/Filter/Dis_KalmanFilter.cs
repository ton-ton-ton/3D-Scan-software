
using System;

namespace _3D_Scan_software
{
    public class Dis_KalmanFilter
    {
        private double x;  // 狀態估計值（位置）
        private double v;  // 狀態估計值（速度）
        private double P;  // 狀態協方差（位置）
        private double V;  // 狀態協方差（速度）
        private double Q;  // 過程噪聲協方差 (難以估計)
        private double R;  // 觀測噪聲協方差 (實驗測得)

        public Dis_KalmanFilter(double processNoise, double measurementNoise)
        {
            x = 0.0;
            v = 0.0;
            P = 0.0;
            V = 0.0;
            Q = processNoise;
            R = measurementNoise;
        }

        public double Update(double measurement, double dt, String Axis)
        {
            double regressiveMeasurement = RegressionFunc2(RegressionFunc(measurement, Axis), Axis);

            // 預測步驟
            double x_pred = x + v * dt;         // 預測位置
            double v_pred = v;                  // 預測速度
            double P_pred = P + Q;              // 預測位置的協方差
            double V_pred = V + Q;              // 預測速度的協方差

            // 更新步驟
            double K = P_pred / (P_pred + R);    // 卡爾曼增益

            x = x_pred + K * (regressiveMeasurement - x_pred);  // 更新位置
            v = v_pred + K * (regressiveMeasurement - x_pred) / dt;  // 更新速度
            P = (1 - K) * P_pred;               // 更新位置的協方差
            V = (1 - K) * V_pred;               // 更新速度的協方差

            return x;
        }

        private double RegressionFunc(double measurement, String axis)
        {
            double dis;
            double _dis1 = measurement;

            if (axis == "X")
            {
                if (_dis1 > 0) { dis = (1.0 / 0.6167) * (_dis1); }
                else if (_dis1 < 0) { dis = (1.0 / 0.6087) * (_dis1); }
                else { dis = _dis1; }
            }
            else
            {
                if (_dis1 > 0) { dis = (1.0 / 0.5777) * (_dis1); }
                else if (_dis1 < 0) { dis = (1.0 / 0.5759) * (_dis1); }
                else { dis = _dis1; }
            }

            return Math.Round(dis, 3);
        }

        private double RegressionFunc2(double measurement, String axis)
        {
            double dis;
            double _dis2 = measurement;

            if (axis == "X")
            {
                if (_dis2 > 0) { dis = (1.0 / 1.096) * (_dis2); }
                else if (_dis2 < 0) { dis = (1.0 / 1.097) * (_dis2); }
                else { dis = _dis2; }
            }
            else
            {
                if (_dis2 > 0) { dis = (1.0 / 1.082) * (_dis2); }
                else if (_dis2 < 0) { dis = (1.0 / 1.097) * (_dis2); }
                else { dis = _dis2; }
            }

            return Math.Round(dis, 3);
        }
    }
}

