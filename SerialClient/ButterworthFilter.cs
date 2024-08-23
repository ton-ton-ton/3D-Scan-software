using System;

namespace SerialClient
{
    public class LowPassFilter
    {
        private float[] a;
        private float[] b;
        private float omega0;
        private float dt;
        private bool adapt;
        private float tn1 = 0;
        private float[] x;
        private float[] y;

        public LowPassFilter(float f0, float fs, bool adaptive)
        {
            omega0 = 6.28318530718f * f0;
            dt = 1.0f / fs;
            adapt = adaptive;
            tn1 = -dt;

            // Set the filter order (1 or 2)
            int order = 2;
            a = new float[order];
            b = new float[order + 1];
            x = new float[order + 1];
            y = new float[order + 1];

            for (int k = 0; k < order + 1; k++)
            {
                x[k] = 0;
                y[k] = 0;
            }

            SetCoef();
        }

        private void SetCoef()
        {
            if (adapt)
            {
                float t = (float)DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
                dt = t - tn1;
                tn1 = t;
            }

            float alpha = omega0 * dt;

            if (a.Length == 1)
            {
                a[0] = -(alpha - 2.0f) / (alpha + 2.0f);
                b[0] = alpha / (alpha + 2.0f);
                b[1] = alpha / (alpha + 2.0f);
            }
            else if (a.Length == 2)
            {
                float alphaSq = alpha * alpha;
                float[] beta = { 1, (float)Math.Sqrt(2), 1 };
                float D = alphaSq * beta[0] + 2 * alpha * beta[1] + 4 * beta[2];
                b[0] = alphaSq / D;
                b[1] = 2 * b[0];
                b[2] = b[0];
                a[0] = -(2 * alphaSq * beta[0] - 8 * beta[2]) / D;
                a[1] = -(beta[0] * alphaSq - 2 * beta[1] * alpha + 4 * beta[2]) / D;
            }
        }

        public float Filter(float xn)
        {
            if (adapt)
            {
                SetCoef();
            }

            y[0] = 0;
            x[0] = xn;

            for (int k = 0; k < a.Length; k++)
            {
                y[0] += a[k] * y[k + 1] + b[k] * x[k];
            }
            y[0] += b[a.Length] * x[a.Length];

            for (int k = a.Length; k > 0; k--)
            {
                y[k] = y[k - 1];
                x[k] = x[k - 1];
            }

            return y[0];
        }
    }
}
