using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Factorization;
using System;

namespace _3D_Scan_software
{
    public class UKF
    {
        /// <summary>
        /// States number
        /// </summary>
        private int L;

        /// <summary>
        /// Measurements number
        /// </summary>
        private int m;

        /// <summary>
        /// The alpha coefficient, characterize sigma-points dispersion around mean
        /// </summary>
        private double alpha;

        /// <summary>
        /// The ki.
        /// </summary>
        private double ki;

        /// <summary>
        /// The beta coefficient, characterize type of distribution (2 for normal one) 
        /// </summary>
        private double beta;

        /// <summary>
        /// Scale factor
        /// </summary>
        private double lambda;

        /// <summary>
        /// Scale factor
        /// </summary>
        private double c;

        /// <summary>
        /// Means weights
        /// </summary>
        private Matrix<double> Wm;

        /// <summary>
        /// Covariance weights
        /// </summary>
        private Matrix<double> Wc;

        /// <summary>
		/// State
		/// </summary>
        private Matrix<double> x;

        /// <summary>
		/// Covariance
		/// </summary>
        private Matrix<double> P;

        /// <summary>
		/// Std of process 
		/// </summary>
        private double q;

        /// <summary>
		/// Std of measurement 
		/// </summary>
        private double r;

        /// <summary>
		/// Covariance of process
		/// </summary>
        private Matrix<double> Q;

        /// <summary>
		/// Covariance of measurement 
		/// </summary>
        private Matrix<double> R;

        /// <summary>
        /// Constructor of Unscented Kalman Filter
        /// </summary>
        /// <param name="L">States number</param>
        /// <param name="m">Measurements number</param>
        public UKF(int L = 0, int m = 0)
        {
            this.L = L;
            this.m = m;
        }

        private void init()
        {
            q = 0.01;
            r = 0.033294;

            x = q * Matrix.Build.Random(L, 1);  //  初始化狀態，設定數量、加入噪聲
            P = Matrix.Build.Diagonal(L, L, 1); //  初始化狀態協方差矩陣，初始化為 1

            Q = Matrix.Build.Diagonal(L, L, q * q);     //  初始化過程噪聲協方差矩陣 
            R = Matrix.Build.Dense(m, m, r * r);        //  初始化測量噪聲協方差矩陣

            //  自訂參數，用於計算權重
            alpha = 1e-3f;  
            ki = 0;
            beta = 2f;
            lambda = alpha * alpha * (L + ki) - L;      
            c = L + lambda;     //  用於對協方差矩陣進行適當的縮放

            //weights for means
            Wm = Matrix.Build.Dense(1, (2 * L + 1), 0.5 / c);   //  初始化平均值權重矩陣
            Wm[0, 0] = lambda / c;                              //  將特定位置的元素修改為 lambda / c

            //weights for covariance
            Wc = Matrix.Build.Dense(1, (2 * L + 1));    //  初始化協方差權重矩陣
            Wm.CopyTo(Wc);                              //  Wm 的值複製到 Wc 中
            Wc[0, 0] = Wm[0, 0] + 1 - alpha * alpha + beta;

            c = Math.Sqrt(c);   //  參數 c 進行開平方運算
        }

        public void Update(double[] measurements, String Axis)
        {
            //  初始化檢查
            //if (m == 0)
            //{
            //    var mNum = measurements.Length;
            //    if (mNum > 0)
            //    {
            //        m = mNum;
            //        if (L == 0) L = mNum;
            //        init();
            //    }
            //}

            var z = Matrix.Build.Dense(m, 1, 0);    //  建立測量向量 z
            double[] regressiveMeasurement = RegressionFunc2(RegressionFunc(measurements, Axis), Axis);
            z.SetColumn(0, regressiveMeasurement);           //  填入測量數組

            //  sigma points around x
            Matrix<double> X = GetSigmaPoints(x, P, c);     //  獲取 sigma 點

            //  unscented transformation of process
            //  X1=sigmas(x1,P1,c) - sigma points around x1
            //  X2=X1-x1(:,ones(1,size(X1,2))) - deviation of X1
            Matrix<double>[] ut_f_matrices = UnscentedTransform(X, Wm, Wc, L, Q);   //  進行無感測轉換【預測】
            Matrix<double> x1 = ut_f_matrices[0];   //  估計值
            Matrix<double> X1 = ut_f_matrices[1];   //  轉換後的 sigma 點矩陣 
            Matrix<double> P1 = ut_f_matrices[2];   //  協方差矩陣 
            Matrix<double> X2 = ut_f_matrices[3];   //  {轉換後的 sigma 點矩陣 - 估計值}

            //unscented transformation of measurments
            Matrix<double>[] ut_h_matrices = UnscentedTransform(X1, Wm, Wc, m, R);  //  對測量進行無感測轉換
            Matrix<double> z1 = ut_h_matrices[0];   //  估計值
            Matrix<double> Z1 = ut_h_matrices[1];   //  轉換後的 sigma 點矩陣 , 不需要用到
            Matrix<double> P2 = ut_h_matrices[2];   //  協方差矩陣 
            Matrix<double> Z2 = ut_h_matrices[3];   //  {轉換後的 sigma 點矩陣 - 估計值}

            //transformed cross-covariance
            Matrix<double> P12 = (X2.Multiply(Matrix.Build.Diagonal(Wc.Row(0).ToArray()))).Multiply(Z2.Transpose());    //  計算轉換後的交叉協方差 P12

            Matrix<double> K = P12.Multiply(P2.Inverse());  //  計算增益 K

            //state update
            x = x1.Add(K.Multiply(z.Subtract(z1)));         //  更新狀態 x，估計 + K(量測-估計)
            //covariance update 
            P = P1.Subtract(K.Multiply(P12.Transpose()));   //  更新協方差 P
        }

        public double[] getState()
        {
            return x.ToColumnArrays()[0];
        }

        public double[,] getCovariance()
        {
            return P.ToArray();
        }

        /// <summary>
        /// Unscented Transformation
        /// </summary>
        /// <param name="f">nonlinear map</param>
        /// <param name="X">sigma points</param>
        /// <param name="Wm">Weights for means</param>
        /// <param name="Wc">Weights for covariance</param>
        /// <param name="n">numer of outputs of f</param>
        /// <param name="R">additive covariance</param>
        /// <returns>[transformed mean, transformed smapling points, transformed covariance, transformed deviations</returns>
        private Matrix<double>[] UnscentedTransform(Matrix<double> X, Matrix<double> Wm, Matrix<double> Wc, int n, Matrix<double> R)
        {
            int L = X.ColumnCount;      //  多少筆 sigma 點
            Matrix<double> y = Matrix.Build.Dense(n, 1, 0);     //  創建儲存估計狀態的矩陣
            Matrix<double> Y = Matrix.Build.Dense(n, L, 0);     //  創建轉換後的 sigma 點的矩陣

            Matrix<double> row_in_X;        //  創建一個用於迴圈中暫存 sigma 點列的矩陣 row_in_X。
            for (int k = 0; k < L; k++)     //  取用儲存在 X 內的每列 sigma 點
            {
                row_in_X = X.SubMatrix(0, X.RowCount, k, 1);        //  從 sigma 點矩陣 X 中取出第 k 列，即一個 sigma 點。
                Y.SetSubMatrix(0, Y.RowCount, k, 1, row_in_X);      //  將這個 sigma 點的值填入 Y 矩陣的相對位置，用於後續的運算。
                y = y.Add(Y.SubMatrix(0, Y.RowCount, k, 1).Multiply(Wm[0, k]));  //  計算估計值 y，將 y 加上 Y 矩陣中對應列的值乘以對應的權重
            }

            Matrix<double> Y1 = Y.Subtract(y.Multiply(Matrix.Build.Dense(1, L, 1)));        //  計算 Y 減去 y 乘以一個全1的行向量，獲得轉換後的 sigma 點矩陣 Y1。
            Matrix<double> P = Y1.Multiply(Matrix.Build.Diagonal(Wc.Row(0).ToArray()));     //  計算 Y1 的加權協方差 P，對 Y1 的每一列乘以對應的權重。
            P = P.Multiply(Y1.Transpose());     //  將加權的 Y1 轉置後與 Y1 相乘，得到加權協方差。
            P = P.Add(R);                       //  將加權協方差 P 與外部給定的協方差矩陣 R 相加，得到最終的協方差矩陣。

            Matrix<double>[] output = { y, Y, P, Y1 };   //  { 將計算得到的估計值 y、轉換後的 sigma 點矩陣 Y、協方差矩陣 P 、轉換後的 Y1 }
            return output;
        }

        /// <summary>
        /// Sigma points around reference point
        /// </summary>
        /// <param name="x">reference point</param>
        /// <param name="P">covariance</param>
        /// <param name="c">coefficient</param>
        /// <returns>Sigma points</returns>
        private Matrix<double> GetSigmaPoints(Matrix<double> x, Matrix<double> P, double c)
        {
            Matrix<double> A = P.Cholesky().Factor;     //  分解為一個下三角矩陣，僅取用特定狀態的誤差

            A = A.Multiply(c);  //  乘上係數c
            A = A.Transpose();  //  轉置

            int n = x.RowCount; //  取得x的維度

            Matrix<double> Y = Matrix.Build.Dense(n, n, 1); //  創建矩陣，所有元素初始化為 1
            for (int j = 0; j < n; j++)         //  生成一個 n * L 大小的矩陣，其中每列都是 x
            {
                Y.SetSubMatrix(0, n, j, 1, x);
            }

            Matrix<double> X = Matrix.Build.Dense(n, (2 * n + 1));   //  創建儲存 sigma 點的矩陣
            X.SetSubMatrix(0, n, 0, 1, x);              //  X 的第 0 列往下數 n 個位置、第 0 行往右數 1個位置等於 x 內的值
            
            Matrix<double> Y_plus_A = Y.Add(A);         //  此刻狀態加上誤差，取得分布的"正列"
            X.SetSubMatrix(0, n, 1, n, Y_plus_A);       //  存入 sigjma 點內

            Matrix<double> Y_minus_A = Y.Subtract(A);   //  此刻狀態減去誤差，取得分布的"負列"
            X.SetSubMatrix(0, n, n + 1, n, Y_minus_A);  //  存入 sigjma 點內

            return X;   //返回 sigma 點
        }


        private double[] RegressionFunc(double[] measurement, String axis)
        {
            double[] dis = new double[measurement.Length];

            if (axis == "X")
            {
                foreach (int i in measurement)
                {
                    if (measurement[i] > 0) { dis[i] = (1.0 / 0.6167) * measurement[i]; }
                    else if (measurement[i] < 0) { dis[i] = (1.0 / 0.6087) * measurement[i]; }
                    else { dis[i] = measurement[i]; }
                }
            }
            else
            {
                foreach (int i in measurement)
                {
                    if (measurement[i] > 0) { dis[i] = (1.0 / 0.5777) * (measurement[i]); }
                    else if (measurement[i] < 0) { dis[i] = (1.0 / 0.5759) * (measurement[i]); }
                    else { dis[i] = measurement[i]; }
                }
            }
            double[] dis2 = new double[measurement.Length];

            foreach (int i in measurement)
            {
                dis2[i] = Math.Round(dis[i], 3);
            }

            return dis2;
        }

        private double[] RegressionFunc2(double[] measurement, String axis)
        {
            double[] dis3 = new double[measurement.Length];

            if (axis == "X")
            {
                foreach (int i in measurement)
                {
                    if (measurement[i] > 0) { dis3[i] = (1.0 / 1.096) * measurement[i]; }
                    else if (measurement[i] < 0) { dis3[i] = (1.0 / 1.097) * measurement[i]; }
                    else { dis3[i] = measurement[i]; }
                }
            }
            else
            {
                foreach (int i in measurement)
                {
                    if (measurement[i] > 0) { dis3[i] = (1.0 / 1.082) * (measurement[i]); }
                    else if (measurement[i] < 0) { dis3[i] = (1.0 / 1.097) * (measurement[i]); }
                    else { dis3[i] = measurement[i]; }
                }
            }
            double[] dis4 = new double[measurement.Length];

            foreach (int i in measurement)
            {
                dis4[i] = Math.Round(dis3[i], 3);
            }

            return dis4;
        }
    }
}
