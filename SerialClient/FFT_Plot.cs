using System;
using System.Numerics;
using System.Windows.Forms;
using System.Drawing;
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace SerialClient
{
    public partial class FFT_Plot : Form
    {
        private double[] RawSignal; // 欲轉換的數據
        private int SampleRate; // 取樣率
        private PlotView Plot;
        PlotModel DataPlot;


        public FFT_Plot(double[] signal, int samplerate)
        {
            InitializeComponent();
            RawSignal = signal;
            SampleRate = samplerate;

            //Add new Plot
            Plot = new PlotView();

            //Add the plot form
            Plot.Location = new Point(0, 20);
            Plot.Width = this.Width;
            Plot.Height = this.Height;

            Plot.Model = DataPlot;
            this.Controls.Add(this.Plot);
            Plot.Parent = panel2; //控制項(Plot)動態加入至視窗(Panel1)中的常見方法
            Plot.Update();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            // 執行傅立葉轉換
            Complex[] spectrum = new Complex[RawSignal.Length];
            RawSignal.CopyTo(spectrum, 0);
            Fourier.Forward(spectrum);

            // 計算頻譜
            double[] frequencies = Fourier.FrequencyScale(SampleRate, spectrum.Length);
            double[] amplitudes = new double[spectrum.Length / 2];
            for (int i = 0; i < amplitudes.Length; i++)
            {
                amplitudes[i] = Complex.Abs(spectrum[i]);
            }

            // 生成 OxyPlot 圖表
            DataPlot = new PlotModel
            {
                Title = "Acceleration Spectrum",
                Subtitle = "FFT Analysis",
                PlotType = PlotType.XY,
            };

            var lineSeries = new LineSeries();
            for (int i = 0; i < frequencies.Length; i++)
            {
                lineSeries.Points.Add(new DataPoint(frequencies[i], amplitudes[i]));
            }

            DataPlot.Series.Add(lineSeries);
            Plot.Model = DataPlot;
        }
    }
}
