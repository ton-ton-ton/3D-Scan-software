using DefButton;
using IOPointCloud;
using PointCloudSharp;
using SerialClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using UserControlEditor;


namespace _3D_Scan_software
{
    public partial class MainScanAPI : Form
    {
        #region 創建對象
        //創建對象(攤開藍圖)
        Form currentChildForm;  //  自定義顯示，域於判斷panelVirsualixtion是否被使用
        IOpcForm io;       //   PCL對象

        RealTimeDB DB;     //   SQL對象
        FrameCapture frameCapture;  //  滑鼠影像擷取對象

        //MadgwickAHRS madgwickFilter;

        public static SerialPort com;   //  初始化串口對象
        Client serial;      //  序列埠對象
        Scanbtn scan;       //  按鈕實作對象
        loadFile loadfile;
        AnalysisChart AnalysisChart;    //  分析表對象
        //Stopwatch sw = null;   //紀錄時間間格
        StreamWriter writer;    //  輸出txt檔

        //ValueChangeChart valueChangechart;

        /// <summary>
        ///濾波
        /// </summary>
        //卡爾曼
        //KalmanFilter kalman_X;
        //KalmanFilter kalman_Y;
        //KalmanFilter kalman_Z;
        //KalmanFilter_acc kalmanFilter_;
        Dis_KalmanFilter dis_Kalman_X;
        Dis_KalmanFilter dis_Kalman_Y;
        double dt = 0.005;  // 時間間隔
        //double processNoise = 0.01;  // 系統噪聲的標準差
        //double observationNoise = 0.1;  // 測量噪聲的標準差

        // UKF
        UKF UKFilter_X = new UKF(1,1);
        UKF UKFilter_Y = new UKF(1,1);


        System.Threading.Timer sampleTimer;     //  時鐘，用於控制刷新點雲畫面的時間
        System.Threading.Timer dataTimer;       //  儲存數據
        //private CancellationTokenSource cancellationTokenSource;
        #endregion

        #region 數據變量
        //緩存資料
        //Dictionary<String, List<Tuple<float, float>>> Buffer;
        Quaternion qua = new Quaternion(1, 0, 0, 0);
        float[] dataflow;       //串口接收數據集

        //各類別數據變量
        //(List<float> x, List<float> y, List<float> z) accelData = (new List<float>(), new List<float>(), new List<float>());
        //(List<float> x, List<float> y, List<float> z) gyroData = (new List<float>(), new List<float>(), new List<float>());
        //List<Quaternion> quaternionData = new List<Quaternion>();
        //(List<float> x, List<float> y) displacementData = (new List<float>(), new List<float>());
        //List<ulong> time = new List<ulong>();
        public PointCloudXYZ point_posi;

        //判斷式
        bool isScanning = false;
        bool firstFrame_;
        bool isTimerRunning = false;
        bool isAnalysisChartOpen = false;
        bool initial2DModeScanning = true;
        private bool isRecording;
        bool isInitialRecordingTime = false;

        //===運算變量===
        ulong prev_time_;
        ulong buf_time;
        double deltaT_;
        Vector3D gyro_b, gyro_w;    // 角速度
        Vector3D gravity_b, gravity_w; // 重力
        Vector3D acc_b, acc_w, acc_linear, acc_Filtered_w, prevAccelerometerValue;     // 加速度
        //Vector3D acc_KF;
        Vector3D linear_vel_w, linear_Vel_Filterd, prevVelocity; // 線性速度
        Vector3D Pos, Pos_Filtered, prevPosition;         // 空間座標
        Vector3D PointCloud;  // 點雲座標
        Vector3D includedTotalLenght;   // 位移
        Vector3D distance_transformed;
        Vector3D distance_raw, distance_filterd;
        Quaternion Orein_qua; // 姿態角
        Quaternion Orein_qua_madiwi; // 姿態角
        Quaternion Orein_filtered;
        double TotalLenght_2D = 0;
        double TotalLenghtofX_2D = 0;
        double TotalLenghtofY_2D = 0;
        double TotalLenghtofX_3D = 0;
        double TotalLenghtofY_3D = 0;
        double TotalLenghtofX_buf = 0;
        double TotalLenghtofY_buf = 0;
        Direction previousDirectionX = Direction.None;  // 初始為 None
        Direction previousDirectionY = Direction.None;  // 初始為 None
        double maxSpeed = 0.0;  // 顯示最大瞬時速度
        double CurrentSpeed = 0.0;  // 顯示瞬時速度
        double Deltatime = 0.0; //  用於計算速度的時間間格
        double CPI = 8000.0;    //  預設CPI為8000
        double Threshold_X = 0.00;  //0.0051
        double Threshold_Y = 0.00;  //0.0047
        private List<string> dataToSave;
        double pre_distanceRawdataBuf = 0;
        int NumofData = 0;
        ulong pre_RecordingTime = 0;


        //計算線性加速度偏差的變量
        ulong SampleNum;
        Vector3D acc_Noise;
        Vector3D acc_Bias = new Vector3D(0, 0, 0);
        double[] buf_bias_x;
        double[] buf_bias_y;
        double[] buf_bias_z;
        int filter_N = 5;
        int BiasSample;
        int lightness = 0;  //  LED亮度

        private String Dimention { get; set; }  // 預設有 Tracking、2D、3D，

        #endregion

        #region 功能宣告

        // 用於互斥的鎖對象
        private object PointCloudDataLock = new object();
        private object AccWorldDataLock = new object();

        //用於控制按鈕位置
        //private int BorderSize = 1;
        //private Size formOriginalSize;
        //private Rectangle iconBtnOutput1;
        //private Rectangle iconBtnScan1;
        //private Rectangle iconBtnSetting1;
        //private Rectangle iconBtnCurFil1;

        #endregion
     
        #region Enum
        enum DataList
        {
            Ax = 1, Ay, Az,
            gx, gy, gz,
            qw, qx, qy, qz,
            X, Y
        };

        enum subMenu1Status { SubMenu1_Setting, SubMenu1_Scan, SubMenu1_CurFil, SubMenu1_Output }
        subMenu1Status subMenu;

        enum Direction
        {
            None,
            Positive,
            Negative
        }

        #endregion

        #region 建構函式與載入函式
        public MainScanAPI()
        {
            InitializeComponent();

            com = subMenu1_Setting.editorConnect.COM; //取得editorConnect類別中已經初始化的comport
            DB = new RealTimeDB();
            scan = new Scanbtn();
            loadfile = new loadFile();


            //com連線狀態事件更新
            subMenu1_Setting.editorConnect.ComPortStatusChanged += new EventHandler(editorConnect_ComPortStatusChanged);

            //頁面控件初始化位置
            SplitterEditorVisua.SplitterDistance = 350; //分隔器

            //左半部頁面初始化狀態修正
            subMenu1_Setting.ShowStatus = false;
            SubMenu1_Output.ShowStatus = false;

            Dimention = SubMenu1_Scan.comboBox_ScanMode.Text;

        }

        /// <summary>
        /// 載入MainScanAPI時會觸發執行的任務
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainScanAPI_Load(object sender, EventArgs e)
        {

            //加入UserControls內的事件，當觸發UserControls上的控件時，觸發執行委派任務(MinimezedClick)並調用註冊的事件處理函數(editorMinimized)
            subMenu1_Setting.MinimezedClick += new EventHandler(editorMinimized);
            SubMenu1_Output.MinimezedClick += new EventHandler(editorMinimized);
            SubMenu1_Scan.MinimezedClick += new EventHandler(editorMinimized);
            SubMenu1_CurFil.MinimezedClick += new EventHandler(editorMinimized);

            SubMenu1_Scan.ScanStartClick += new EventHandler(btnScanStart_Click);
            SubMenu1_Scan.ScanPauseClick += new EventHandler(btnScanPause_Click);
            SubMenu1_Scan.ScanResetClick += new EventHandler(btnScanReset_Click);
            SubMenu1_Scan.ScalarBarCheck += new EventHandler(ScalarBar_CheckedChanged);
            SubMenu1_Scan.SensorCtrClick += new EventHandler(btnSensorCTR_Click);
            SubMenu1_Scan.RestartSensorClick += new EventHandler(iconBtn_RestartSensor_Click);
            SubMenu1_Scan.liftDetecUPClick += new EventHandler(iconBtn_liftDetecUP_Click);
            SubMenu1_Scan.liftDetecDownClick += new EventHandler(iconBtn_liftDetecDown_Click);
            SubMenu1_Scan.ShutterUPClick += new EventHandler(iconBtn_ShutterUP_Click);
            SubMenu1_Scan.ShutterDownClick += new EventHandler(iconBtn_ShutterDown_Click);
            SubMenu1_Scan.MinBoundUPClick += new EventHandler(iconBtn_MinBoundUP_Click);
            SubMenu1_Scan.MinBoundDownClick += new EventHandler(iconBtn_MinBoundDown_Click);
            SubMenu1_Scan.CPIChanged += new EventHandler(SubMenu1_Scan_trackBar_CPIchanged_MouseUp);
            SubMenu1_Scan.LASERIntensityChanged += new EventHandler(SubMenu1_Scan_LASERIntensityChanged);
            SubMenu1_Scan.DimentionChanged += new EventHandler(SubMenu1_Scan_DimentionChanged);
            SubMenu1_Scan.SaveandResetClick += new EventHandler(SubMenu1_Scan_SaveAndReset_Click);



            SubMenu1_Output.PcdSaveClick += new EventHandler(SubMenu1_Output_PcdSaveClick);
            SubMenu1_Output.TxtSaveClick += new EventHandler(SubMenu1_Output_TxtSaveClick);



            //其他專案內的判斷式初始化
            scan.ScanStatus = false;      //是否已經讀取過COM上的數據

            //初始化SubMenu狀態
            subMenu = subMenu1Status.SubMenu1_Setting;

        }

        /// <summary>
        /// 在panelVirsualixtion上顯示任意專案畫面
        /// </summary>
        /// <param name="childform"></param>
        private void OpenChildform(Form childform)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childform;
            childform.TopLevel = false;  //降階以實現納入父窗體(MainScanAPI)內，共享父窗體樣式和行為(下列兩行所示)
            childform.FormBorderStyle = FormBorderStyle.None;
            childform.Dock = DockStyle.Fill;

            panelVirsualixtion.Controls.Add(childform); //創建物件(實際蓋房子)
            panelVirsualixtion.Tag = childform;     //取得chilform自己畫面上的物件(像是按鈕等等)
            childform.BringToFront();
            childform.Show();
            childform.Refresh();
        }

        #endregion

        #region 關閉、縮小、最小化
        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                DB.CloseLocalDB();
                Application.Exit();
            }
            catch { };
        }
        private void BtnMaximum_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }
        private void BtnMinimize_Click(object sender, EventArgs e)
        {

            WindowState = FormWindowState.Minimized;
        }
        #endregion

        #region Win套件
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int LParam);
        #endregion

        #region Main畫面控件修正

        /// <summary>
        /// 畫面大小改變時觸發的事件處理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainScanAPI_Resize(object sender, EventArgs e)
        {
            BorderAdust();
            FrmSize();

            /*
              protected override void WndProc(ref Message m)
              {
                  const int WM_NCCALCSIZE = 0x0083;
                  if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1) return;
                  base.WndProc(ref m);
              }*/

        }

        /// <summary>
        /// 畫面邊界修正
        /// </summary>
        private void BorderAdust()
        {
            switch (this.WindowState)
            {
                case FormWindowState.Maximized:
                    this.Padding = new Padding(0, 8, 0, 0);
                    break;

                case FormWindowState.Normal:
                    this.Padding = new Padding(0, 8, 0, 0);
                    //this.FormBorderStyle = FormBorderStyle.Sizable;
                    break;
            }
        }

        /// <summary>
        /// 滑鼠左鍵壓住TitleBar時可控制視窗移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainScanAPI_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        #endregion

        #region 控件視覺化
        /// <summary>
        /// 滑鼠離控件後顏色變為白色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeactivateChangeModeColor(object sender, EventArgs e)
        {
            if (sender == iconBtnSetting) iconBtnSetting.IconColor = System.Drawing.Color.White;
            else if (sender == iconBtnScan) iconBtnScan.IconColor = System.Drawing.Color.White;
            else if (sender == iconBtnCurFil) iconBtnCurFil.IconColor = System.Drawing.Color.White;
            else iconBtnOutput.IconColor = System.Drawing.Color.White;
        }
        /// <summary>
        /// 滑鼠在控件上時，顏色轉變為綠色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActivateChangeModeColor(object sender, MouseEventArgs e)
        {
            if (sender == iconBtnSetting) iconBtnSetting.IconColor = System.Drawing.Color.FromArgb(60, 110, 56);
            else if (sender == iconBtnScan) iconBtnScan.IconColor = System.Drawing.Color.FromArgb(60, 110, 56);
            else if (sender == iconBtnCurFil) iconBtnCurFil.IconColor = System.Drawing.Color.FromArgb(60, 110, 56);
            else iconBtnOutput.IconColor = System.Drawing.Color.FromArgb(60, 110, 56);

        }
        #endregion

        #region 掃描點雲

        /// <summary>
        ///【 Scan開始】  -按鈕執行程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScanStart_Click(object sender, EventArgs e)
        {
            if (com.IsOpen)
            {
                if (!isScanning)
                {
                    // 物件(按鈕)狀態改變
                    this.SubMenu1_Scan.iconbtn_Scan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
                    this.SubMenu1_Scan.iconbtn_Scan.Font = new System.Drawing.Font("微軟正黑體", 7.8F, System.Drawing.FontStyle.Bold);
                    this.SubMenu1_Scan.comboBox_ScanMode.Enabled = false;

                    //初始化數據陣列變量
                    dataflow = new float[(int)DataList.Y];   //暫存一筆的感測數據(全域變數)
                    point_posi = new PointCloudXYZ(); //PCL點雲

                    //初始化KalmanFilter
                    //kalman_X = new KalmanFilter(dt, processNoise, observationNoise);
                    //kalman_Y = new KalmanFilter(dt, processNoise, observationNoise);
                    //kalman_Z = new KalmanFilter(dt, processNoise, observationNoise);

                    //位移感測 - Kalman Filter
                    dis_Kalman_X = new Dis_KalmanFilter(0.001, 0.033);
                    dis_Kalman_Y = new Dis_KalmanFilter(0.001, 3);

                    //madgwickFilter = new MadgwickAHRS(0.01f, 0.3f);//有點難用，靈敏度需再調參數來優化

                    //空間定位變量
                    gravity_b = new Vector3D(0, 0, 0);  //重力加速度
                    acc_w = new Vector3D(0, 0, 0);          //加速度
                    acc_linear = new Vector3D(0, 0, 0);
                    acc_Filtered_w = new Vector3D(0, 0, 0); //====
                    //Vector3D acc_KF = new Vector3D(0, 0, 0);
                    linear_vel_w = new Vector3D(0, 0, 0);       //速度
                    linear_Vel_Filterd = new Vector3D(0, 0, 0); //====
                    Pos = new Vector3D(0, 0, 0);   //位移
                    prev_time_ = 0; //時間
                    deltaT_ = 0;    //====
                    buf_time = 0;   //====
                    acc_Noise = new Vector3D(0, 0, 0); //雜訊
                    //acc_Bias = new Vector3D(0, 0, 0);       //偏差
                    buf_bias_x = new double[filter_N + 1];  //====
                    buf_bias_y = new double[filter_N + 1];  //====
                    buf_bias_z = new double[filter_N + 1];  //====
                    prevVelocity = new Vector3D(0, 0, 0);
                    prevPosition = new Vector3D(0, 0, 0);
                    prevAccelerometerValue = new Vector3D(0, 0, 0);

                    //  點雲掃描變量
                    includedTotalLenght = new Vector3D(0, 0, 0);
                    distance_raw = new Vector3D(0, 0, 0);
                    distance_transformed = new Vector3D(0,0,0);
                    BiasSample = 0;

                    //com連線後觸發事件(DataReceived)，執行com_DataReceived()，數據開始接收
                    firstFrame_ = true;
                    com.DataReceived += new SerialDataReceivedEventHandler(com_DataReceived);

                    //開啟可視化畫面
                    io = new IOpcForm();
                    OpenChildform(io);
                    io.ResetPC();

                    //開啟計時器，更新可視化點雲
                    AutoResetEvent autoEvent = new AutoResetEvent(false);   //  同步基元類型，用於多執行緒間同步
                    TimerCallback scanCallBack = Scan2Display;
                    sampleTimer = new System.Threading.Timer(scanCallBack, autoEvent, 100, 33);

                    isTimerRunning = true;  //  【主要】暫停掃描狀態，【影響】畫面啟動更新、點雲座標計算
                    isScanning = true;      //  【主要】Scan按鈕狀態，【影響】所有掃描相關狀態、分析表更新、空間定位更新
                    rtb_MainStatusAutoScroll("開始掃描");
                }
                else
                {
                    this.SubMenu1_Scan.iconbtn_Scan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
                    this.SubMenu1_Scan.iconbtn_Scan.Font = new System.Drawing.Font("微軟正黑體", 7.8F, System.Drawing.FontStyle.Regular);
                    this.SubMenu1_Scan.comboBox_ScanMode.Enabled = true;


                    //com取消註冊事件
                    com.DataReceived -= com_DataReceived;

                    //關閉計時器
                    isTimerRunning = false;
                    sampleTimer.Dispose();
                    
                    //關閉畫面
                    io.Close();
                    io = null;
                    
                    //釋放變量內存
                    gravity_b = default(Vector3D);            // 重力加速度
                    acc_w = default(Vector3D);                // 加速度
                    acc_linear = default(Vector3D);
                    acc_Filtered_w = default(Vector3D);
                    //acc_KF = default(Vector3D);
                    linear_vel_w = default(Vector3D);         // 速度
                    linear_Vel_Filterd = default(Vector3D);
                    Pos = default(Vector3D);                  // 位移
                    includedTotalLenght = default(Vector3D);
                    distance_raw = default(Vector3D);
                    distance_transformed   = default(Vector3D);
                    acc_Noise = default(Vector3D);            //  雜訊
                    //acc_Bias = default(Vector3D);   

                    buf_bias_x = null;
                    buf_bias_y = null;
                    buf_bias_z = null;
                    dataflow = null;
                    point_posi.Clear();
                    isScanning = false;
                    updatePosOnDisplay(Pos);
                    rtb_MainStatusAutoScroll("結束掃描");

                }
            }
            else { rtb_MainStatusAutoScroll("COM未開啟"); }

        }

        /// <summary>
        ///【畫面更新】 -主要掃描點雲可視化內容
        /// </summary>
        private void Scan2Display(object state)
        {
            if (isScanning && isTimerRunning)
            {
                
                Task.Run(() =>
                {
                    //判斷是否正在處理顯示
                    if (io != null && !(io.IsDisposed) && io.renderProcessingLock != true)
                    {
                        //判斷是否需要加入主頁面UI執行緒裡
                        if (this.InvokeRequired)
                        {
                            //以委任事件方式加載任務至主線程內執行，避免跨執行緒錯誤
                            this.Invoke((MethodInvoker)delegate
                            {
                                //更新顯示的點雲座標

                                //lock (PointCloudDataLock)
                                //{
                                updatePosOnDisplay(Pos);    //  更新空間定位座標

                                //  更新分析表數據
                                if (isAnalysisChartOpen)
                                {
                                    //  記得更新 analysisChartToolStripMenuItem_Click()標註的名稱
                                    PrintOnAnalysisCahrt(includedTotalLenght, "A1");
                                    PrintOnAnalysisCahrt(distance_raw, "A2");
                                    PrintOnAnalysisCahrt(PointCloud, "A3");
                                    PrintOnAnalysisCahrt(gravity_b, "A4");
                                    PrintOnAnalysisCahrt(acc_w, "A5");
                                    PrintOnAnalysisCahrt(Orein_qua, "A6");
                                    UpdateFPS(deltaT_);
                                    UpdateDataNum(NumofData);
                                    AnalysisChart.lbl_StoragedNum.Text = AnalysisChart.Storagednum.ToString();
                                }
                                if (io != null) {  io.ScanePointsReader(point_posi, Orein_qua); }  //  更新點雲座標
                                   
                                //}
                            });
                        }
                        else
                        {
                           // lock(PointCloudDataLock)
                           // {
                                updatePosOnDisplay(Pos);
                                if (isAnalysisChartOpen)
                                {
                                    PrintOnAnalysisCahrt(includedTotalLenght, "A1");
                                    PrintOnAnalysisCahrt(distance_raw, "A2");
                                    PrintOnAnalysisCahrt(PointCloud, "A3");
                                    PrintOnAnalysisCahrt(gravity_b, "A4");
                                    PrintOnAnalysisCahrt(acc_w, "A5");
                                    PrintOnAnalysisCahrt(Orein_qua, "A6");
                                    UpdateFPS(deltaT_);
                                    UpdateDataNum(NumofData);
                                    AnalysisChart.lbl_StoragedNum.Text = AnalysisChart.Storagednum.ToString();

                            }

                            if (io != null) { io.ScanePointsReader(point_posi, Orein_qua); }  //  更新點雲座標
                            // }
                        }

                        if (io != null) { io.renderProcessingLock = false; }
                    }
                }); 
            }
        }

        /// <summary>
        /// 【數據更新】 -資料流事件處理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void com_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
             Task.Run(() =>
             {
                if (com.IsOpen && com != null)
                {
                    //  取得串口數據
                    scan.StringDataReceived(com, dataflow, ref buf_time);   //  透過StringDataReceived()存入Acell、Gyro、quater、dist等數據至dataflow

                    bool allNonZero = dataflow.All(value => value == 0);    //  判斷資料取得正確，都為零 = true 

                    if (!allNonZero)
                    {
                         // 取得空間定位(Pos)  {I}：MPU6050 座標系、 {D}：ADNS 座標系 {N}： 導航座標系(起始時固定)
                         acc_b = new Vector3D(dataflow[0], dataflow[1], dataflow[2]);   //  {I}
                         gyro_b = new Vector3D(dataflow[3], dataflow[4], dataflow[5]);  //  {I}
                         distance_raw = new Vector3D(dataflow[10], dataflow[11], 0);   //  {D}
                         Orein_qua = new Quaternion(dataflow[7], dataflow[8], dataflow[9], dataflow[6]);  //  {N}

                         if (firstFrame_)
                         {
                             // 初始化變量
                             prev_time_ = buf_time;

                             // 卡爾曼濾波加速度
                             //processNoiseCovarianceValue：通常在0.001到0.1之間。較小的值表示對系統模型的信任度較高，較大的值表示對系統模型的信任度較低。
                             //measurementNoiseCovarianceValue：通常在0.1到10之間。較小的值表示對測量模型的信任度較高，較大的值表示對測量模型的信任度較低。
                             //kalmanFilter_ = new KalmanFilter_acc(0.05, 3.0);

                             // 進行下一Frame
                             firstFrame_ = false;
                            
                         }
                        else
                        {
                             deltaT_ = Convert.ToDouble(buf_time - prev_time_) * 1e-6;  //  秒(s)
                             prev_time_ = buf_time;
                            if (deltaT_ < 0.02 && deltaT_ > 0.0 ) 
                            { 
                                 //UpdateGravity(Orein_qua); //   更新物體座標系上的重力加速度
                                 calPos(acc_b, gyro_b, Orein_qua, deltaT_); //  計算空間座標

                                 if (isTimerRunning)
                                 {
                                     lock (PointCloudDataLock)
                                     {
                                         //取得量測座標(point_posi)
                                         CalPosition(distance_raw, Orein_qua, ref point_posi, deltaT_, Dimention);
                                        Array.Clear(dataflow, 0, dataflow.Length);
                                    }
                                 }
                            }
         
                        }

                        //儲存數據，可用於後處理與分析
                        //if (dataflow.Length > 0)
                        //{
                        //    //將數據儲存至暫存內
                        //    /*加速度*/
                        //    accelData.x.Append(dataflow[0]); accelData.y.Append(dataflow[1]); accelData.z.Append(dataflow[2]);
                        //    /*陀螺儀*/
                        //    gyroData.x.Append(dataflow[3]); gyroData.y.Append(dataflow[4]); gyroData.z.Append(dataflow[5]);
                        //    /*四元數*/
                        //    qua.W = dataflow[6]; qua.X = dataflow[7]; qua.Y = dataflow[8]; qua.Z = dataflow[9];
                        //    quaternionData.Add(qua);
                        //    /*位移*/
                        //    displacementData.x.Append(dataflow[10]); displacementData.y.Append(dataflow[11]);
                        //    /*時間*/
                        //    time.Append(buf_time);
                        //    Array.Clear(dataflow, 0, dataflow.Length);

                        //    //分別儲存置DB
                        //}
                    }

                }
            });
        }

        /// <summary>
        /// 【掃描物體】 -主程式
        /// </summary>
        /// <param name="disMesurment">位移</param>
        /// <param name="orien">姿態角</param>
        /// <param name="position">點雲座標</param>
        /// <param name="dimention">二維、 三維</param>
        private void CalPosition(Vector3D distMesurment, Quaternion orien, ref PointCloudXYZ position, double dt, string dimention)
        {
            Vector3D prePointCloud = PointCloud;    //  預儲存上一刻座標
            switch (dimention)
            {
                case "Tracking": //  位移路徑追蹤顯示--MPU6050計算
                    PointCloud = Pos;
                    
                    if (!IsNanData(PointCloud)) { position.Push(PointCloud.X, PointCloud.Y, PointCloud.Z); }   //  更新空間座標
                    else { position.Push(prePointCloud.X, prePointCloud.Y, prePointCloud.Z); }                 //  停留原空間座標
                        
                    break; 
                default:    //  位移路徑追蹤顯示--ADNS9800計算
                    if (MotionDetec(distMesurment) && dt > 0.0)
                    {

                        double Dis_X_Raw = distMesurment.X;
                        double Dis_Y_Raw = distMesurment.Y;
                        double DeltaTime = dt;

                        Vector3D buf_dis_Filtered;  //  用於 2D 與 3D 的暫存值
                        double Dis_Y_filtered, Dis_X_filtered;  //  kalman濾波
                        bool X_isOutlier, Y_isOutlier;          //  判斷異常值

                        Vector3D buf_dis = new Vector3D(Math.Round(2.54 * Dis_X_Raw / CPI , 4), Math.Round(2.54 * Dis_Y_Raw / CPI, 4) , 0); //  原數據透過解析度換算理論位移(cm)，取到小數點後第四位
                        Direction directionX = GetDirection(buf_dis.X);     //  X 位移方向
                        Direction directionY = GetDirection(buf_dis.Y);     //  Y 位移方向


                        // 檢查 X 方向
                        if (directionX != previousDirectionX && directionX != Direction.None)
                        {
                            TotalLenghtofX_buf = 0;
                            TotalLenghtofX_buf += buf_dis.X;
                        }
                        else { TotalLenghtofX_buf += buf_dis.X; }
                        previousDirectionX = directionX;    //  更新前一刻的方向

                        // 檢查 Y 方向
                        if (directionY != previousDirectionY && directionY != Direction.None)
                        {
                            TotalLenghtofY_buf = 0;
                            TotalLenghtofY_buf += buf_dis.Y;
                        }
                        else { TotalLenghtofY_buf += buf_dis.Y; }
                        previousDirectionY = directionY;    //  更新前一刻的方向

                        Deltatime += DeltaTime;    //  計算每次顯示的時間間格

                        //  取得瞬時速度
                        double length = Math.Sqrt(Math.Pow(buf_dis.X, 2) + Math.Pow(buf_dis.Y, 2));
                        CurrentSpeed = Math.Round((length / DeltaTime) / 0.6, 3);

                        //  取得最大瞬時速度
                        //CalculatemaxSpeed(length, DeltaTime);


                        //  大於自訂的最小解析度(閾值濾波)
                        if ( Math.Abs(TotalLenghtofX_buf) > Threshold_X || Math.Abs(TotalLenghtofY_buf) > Threshold_Y)
                        {
                            //  卡爾曼濾波
                            //Dis_X_filtered = dis_Kalman_X.Update(TotalLenghtofX_buf, Deltatime, "X");
                            //Dis_Y_filtered = dis_Kalman_Y.Update(TotalLenghtofY_buf, Deltatime, "Y");
                            //TotalLenghtofX_buf = 0;
                            //TotalLenghtofY_buf = 0;
                            //Deltatime = 0;

                            //  無跡卡爾曼濾波
                            //UKFilter_X.Update(new[] { TotalLenghtofX_buf }, "X");
                            //UKFilter_Y.Update(new[] { TotalLenghtofY_buf }, "Y");
                            //Dis_X_filtered = UKFilter_X.getState()[0];
                            //Dis_Y_filtered = UKFilter_Y.getState()[0];
                            //TotalLenghtofX_buf = 0;
                            //TotalLenghtofY_buf = 0;

                            //  無濾波
                            //Dis_X_filtered = TotalLenghtofX_buf;
                            //Dis_Y_filtered = TotalLenghtofY_buf;
                            //TotalLenghtofX_buf = 0;
                            //TotalLenghtofY_buf = 0;

                            //  回歸分析
                            Dis_X_filtered = MainRegressionFunc('X');
                            Dis_Y_filtered = MainRegressionFunc('Y');

                            //  排除異常值(無限大或 NaN)
                            X_isOutlier = (double.IsNaN(Dis_X_filtered) || double.IsInfinity(Dis_X_filtered)) ? true : false;
                            Y_isOutlier = (double.IsNaN(Dis_Y_filtered) || double.IsInfinity(Dis_Y_filtered)) ? true : false;
                            if (!X_isOutlier && !Y_isOutlier)
                            {
                                switch (dimention)
                                {
                                    default:  //   掃描二維
                                        if (initial2DModeScanning)
                                        {
                                            initial2DModeScanning = false;
                                        }
                                        else
                                        {
                                            // 取得微小位移長度
                                            // length = Math.Sqrt(Math.Pow(Dis_X_filtered, 2) + Math.Pow(Dis_Y_filtered, 2));

                                            // TotalLenght_2D += length;        //  總位移長度
                                            TotalLenghtofX_2D += Dis_X_filtered;    //  總 X 位移長度
                                            TotalLenghtofY_2D += Dis_Y_filtered;    //  總 Y 位移長度

                                            buf_dis_Filtered = new Vector3D(Dis_X_filtered, -Dis_Y_filtered, 0);    //  {D} -> 螢幕坐標系

                                            double outlier = buf_dis_Filtered.Length;   //  計算異常值
                                            if (outlier < 1000)
                                            {
                                                PointCloud += buf_dis_Filtered;    //  更新此刻二維點雲座標
                                                NumofData++;    //  紀錄點雲數量
                                                if (isRecording)
                                                {
                                                    DataTimer_Elapsed(includedTotalLenght, 'Z', prev_time_);
                                                }

                                                bool Samedata_2D = PointCloud == prePointCloud ? true : false;  //  false ：與前一刻座標不同
                                                if (!IsNanData(PointCloud) && !Samedata_2D)
                                                { position.Push(PointCloud.X, PointCloud.Y, PointCloud.Z); }    //  更新空間定位座標
                                            }
                                        }

                                        break;

                                    case "3D":

                                        // TotalLenght_2D += length;        //  總位移長度
                                        TotalLenghtofX_2D += Dis_X_filtered;    //  總 X 位移長度
                                        TotalLenghtofY_2D += Dis_Y_filtered;    //  總 Y 位移長度

                                        buf_dis_Filtered = new Vector3D(0, -Dis_X_filtered, Dis_Y_filtered);        //  {D} -> {I}
                                        Vector3D _distance_transformed = RotateVectorByQuaternion(buf_dis_Filtered, orien);   //  {I} 座標轉換至導航座標系{N}
                                        PointCloud += _distance_transformed;   //  更新此刻三維點雲座標

                                        bool Samedata_3D = PointCloud == prePointCloud ? true : false;  //  判斷是否與前一刻座標不同

                                        if (!IsNanData(PointCloud) && !Samedata_3D)
                                        { position.Push(PointCloud.X, PointCloud.Y, PointCloud.Z); }    //  更新空間定位座標

                                        break;
                                }
                            }
                        }

                        includedTotalLenght = new Vector3D(TotalLenghtofX_2D, TotalLenghtofY_2D, CurrentSpeed);    //  存入總長變量，顯示在分析表內

                    }
                    else
                    {
                        CurrentSpeed = 0.0;
                        includedTotalLenght = new Vector3D(TotalLenghtofX_2D, TotalLenghtofY_2D, CurrentSpeed);    //  存入總長變量，顯示在分析表內
                    }
                    break;
            }
        }

        /// <summary>
        /// 主要回歸分析，包含一次與二次迴歸分析
        /// </summary>
        /// <param name="Axis"></param>
        /// <returns></returns>
        private double MainRegressionFunc(char Axis)
        {
            double RregressedData = 0;
            if (Axis == 'X')
            {
                double Data_X = 0;
                Data_X = RegressionFunc2(RegressionFunc(TotalLenghtofX_buf, 'X'), 'X');
                //Data_X = RegressionFunc(TotalLenghtofX_buf, 'X');
                TotalLenghtofX_buf = 0;
                RregressedData = Data_X;
            }
            else
            {
                double Data_Y = 0;
                Data_Y = RegressionFunc2(RegressionFunc(TotalLenghtofY_buf, 'Y'), 'Y');
                //Data_Y = RegressionFunc(TotalLenghtofY_buf, 'Y');
                TotalLenghtofY_buf = 0;
                RregressedData = Data_Y;
            }
            NumofData++;
            return RregressedData;
        }

        /// <summary>
        /// 判斷位移方向
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private Direction GetDirection(double displacement)
        {
            if (displacement > 0)
            {
                return Direction.Positive;
            }
            else if (displacement < 0)
            {
                return Direction.Negative;
            }
            else
            {
                return Direction.None;
            }
        }
         
        /// <summary>
        /// 【掃描物體】 -顯示顏色刻度表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScalarBar_CheckedChanged(object sender, EventArgs e)
        {
            if (io != null && !io.IsDisposed)
            {
                if (SubMenu1_Scan.ScalarBar.CheckState == CheckState.Checked) { io.isScalarBarOpen = true; }
                else { io.isScalarBarOpen = false; }
            }
        }

        #region 空間定位

        /// <summary>
        /// 【空間定位】 -主程式
        /// </summary>
        /// <param name="msg"></param>
        //private void calPos(Vector3D msg, Vector3D gyroscop, Quaternion orienQuat, double time)
        //{
        //    double gyroThreshold = 35.0; // 角速度閾值
        //    if (gyroscop.Length <= gyroThreshold)
        //    {
        //        //感測加速度轉為世界座標
        //        acc_w = RotateVectorByQuaternion(msg, orienQuat);   //(m/s^2)

        //        // 感測重力加速度轉換到世界座標系
        //        Vector3D gravi_b = new Vector3D(1, 0, 0);
        //        Vector3D gravity_w = RotateVectorByQuaternion(gravi_b, orienQuat);

        //        //獲取穩定的線性加速度，即加速度減去重力
        //        Vector3D linear_acc  = acc_w - gravity_w;

        //        //計算偏差
        //        acc_Bias = MovingAvarageBiasValue(linear_acc);
        //        //計算雜訊
        //        acc_Noise = AvarageNoisyValue(linear_acc - acc_Bias, new Vector3D(0, 0, 0));

        //        //閾值濾波加速度
        //        Vector3D acc_Filtered_w = (linear_acc - acc_Bias) * 9.806;
        //        acc_Filtered_w = DataThrFilter(acc_Filtered_w, 0.001, 3.0);

        //        //Vector<double> measurement = Vector<double>.Build.DenseOfArray(new double[] { acc_Filtered_w.X, acc_Filtered_w.Y, acc_Filtered_w .Z});
        //        //kalmanFilter_.Update(measurement, gravity_w);
        //        //Vector<double> compensatedAcceleration = kalmanFilter_.GetState();

        //        //acc_KF.X = compensatedAcceleration[0];
        //        //acc_KF.Y = compensatedAcceleration[1];
        //        //acc_KF.Z = compensatedAcceleration[2];

        //        bool haveacc = MotionDetec(acc_Filtered_w);
        //        if (haveacc)
        //        {
        //            //if(prevVelocity)
        //            //計算線性速度並濾波
        //            Vector3D linear_Vel_ = prevVelocity + acc_Filtered_w * time;

        //            //高通濾波器
        //            double frequency = 2000; // 過濾頻率
        //            int sampleRate = 10;  // 采樣率
        //            FilterButterworth.PassType passType = FilterButterworth.PassType.Highpass; // 過濾類型，可以選擇 Highpass 或 Lowpass
        //            double resonance = 0.707; // 過濾共振值

        //            FilterButterworth filterX = new FilterButterworth(frequency, sampleRate, passType, resonance);
        //            FilterButterworth filterY = new FilterButterworth(frequency, sampleRate, passType, resonance);
        //            FilterButterworth filterZ = new FilterButterworth(frequency, sampleRate, passType, resonance);

        //            filterX.Update(linear_Vel_.X);
        //            filterY.Update(linear_Vel_.Y);
        //            filterZ.Update(linear_Vel_.Z);

        //            Vector3D linear_vel_HP = new Vector3D(filterX.Value, filterY.Value, filterZ.Value);
        //            linear_Vel_Filterd = DataThrFilter(linear_vel_HP, 0.001, 0.8);  //濾波速度

        //            prevVelocity = linear_Vel_Filterd;
        //        }
        //        else
        //        {
        //            linear_Vel_Filterd = new Vector3D(0, 0, 0);
        //            prevVelocity = new Vector3D(0, 0, 0);
        //        }

        //        //計算位移與速度
        //        //UpdateVelocity(acc_Filtered_w.X, time, "X");
        //        //UpdateVelocity(acc_Filtered_w.Y, time, "Y");
        //        //UpdateVelocity(acc_Filtered_w.Z, time, "Z");

        //        //計算位置座標 - Kalman Filter
        //        //double filtered_X_Position = kalman_X.Filter(acc_Filtered_w.X, gyro_b.X, time, 35.0);
        //        //double filtered_Y_Position = kalman_Y.Filter(acc_Filtered_w.Y, gyro_b.Y, time, 35.0);
        //        //double filtered_Z_Position = kalman_Z.Filter(acc_Filtered_w.Z, gyro_b.Z, time, 35.0);
        //        //linear_vel_w = new Vector3D(kalman_X.prevVelocity, kalman_Y.prevVelocity, kalman_Z.prevVelocity);

        //        bool Motion = MotionDetec(linear_Vel_Filterd);
        //        if (Motion)
        //        {
        //            MotionStatusOnDotCircle(Motion);    //狀態改變 -(綠)

        //            //計算位移
        //            Vector3D linear_posi_ = prevPosition + linear_Vel_Filterd * time;

        //            double frequency = 2500; // 過濾頻率
        //            int sampleRate = 10;  // 采樣率
        //            FilterButterworth.PassType passType = FilterButterworth.PassType.Highpass; // 過濾類型，可以選擇 Highpass 或 Lowpass
        //            double resonance = 0.707; // 過濾共振值

        //            FilterButterworth filterX = new FilterButterworth(frequency, sampleRate, passType, resonance);
        //            FilterButterworth filterY = new FilterButterworth(frequency, sampleRate, passType, resonance);
        //            FilterButterworth filterZ = new FilterButterworth(frequency, sampleRate, passType, resonance);

        //            filterX.Update(linear_posi_.X);
        //            filterY.Update(linear_posi_.Y);
        //            filterZ.Update(linear_posi_.Z);

        //            Vector3D linear_Posi_filtered = new Vector3D(filterX.Value, filterY.Value, filterZ.Value);

        //            prevPosition = linear_Posi_filtered;

        //            if (!IsNanData(linear_Posi_filtered)) { Pos = linear_Posi_filtered * 100.0; }
        //            else { Pos = new Vector3D(0, 0, 0); }
        //        }
        //        else
        //        {
        //            MotionStatusOnDotCircle(Motion);    //  狀態改變 -(紅)
        //            //linear_vel_w = new Vector3D(0, 0, 0);   //    歸零速度
        //            //linear_Vel_Filterd = new Vector3D(0, 0, 0);
        //            //Pos = new Vector3D(0, 0, 0);
        //        }

        //    }
        //    else
        //    {
        //        // 角速度高於閾值，不更新加速度和位移
        //        acc_Filtered_w = prevAccelerometerValue;
        //        linear_Vel_Filterd =prevVelocity;
        //        Pos = prevPosition;
        //    }

        //    //紀錄
        //    //prevVelocity = linear_Vel_Filterd;
        //    prevAccelerometerValue = acc_Filtered_w;
        //}
        // ==================================================================================================================
        // ==================================================================================================================
        //private void calPos(Vector3D msg, Vector3D gyroscop, Quaternion orienQuat, double deltatime)
        //{
        //    //Matrix3D rotMatrix_ = QuaternionToRotationMatrix(orienQuat);  //    計算慣性座標(I)轉為世界座標(W)的旋轉矩陣

        //    //UpdateGravity(orienQuat);   //  取得慣性座標上的重力向量gravity_b
        //    //gravity_w = rotMatrix_.Transform(gravity_b);    //  重力向量(I) -> (W)

        //    //Vector3D acc_b_ = msg;    //  加速度向量(I)
        //    //double length = acc_b_.Length;  //  歸一化向量
        //    //acc_b = acc_b_ / length;
        //    //acc_w = rotMatrix_.Transform(acc_b);    //  加速度向量(I) -> (W)

        //    ////acc_linear = new Vector3D(acc_w.X, acc_w.Y, acc_w.Z - 1);  //  線性加速度
        //    //Vector3D acc_linear_ = acc_w - gravity_w;

        //    //acc_Bias = MovingAvarageBiasValue(acc_linear_);

        //    acc_linear = msg;
        //    //acc_linear = DataThrFilter(acc_linear, 0.001, 1.5);

        //    //double gyroThreshold = 10000.0; // 角速度閾值
        //    //if (gyroscop.Length <= gyroThreshold)
        //    //{
        //    //  高通濾波加速度
        //    //acc_Filtered_w.X = LowPassFilter(acc_Filtered_w.X, acc_linear.X, 0.25, 1.5);
        //    //acc_Filtered_w.Y = LowPassFilter(acc_Filtered_w.Y, acc_linear.Y, 0.25, 1.5);
        //    //acc_Filtered_w.Z = LowPassFilter(acc_Filtered_w.Z, acc_linear.Z, 0.25, 1.5);
        //    //acc_Filtered_w.X = HighPassFilter(acc_Filtered_w.X, acc_linear.X, 0.9);
        //    //acc_Filtered_w.Y = HighPassFilter(acc_Filtered_w.Y, acc_linear.Y, 0.9);
        //    //acc_Filtered_w.Z = HighPassFilter(acc_Filtered_w.Z, acc_linear.Z, 0.9);

        //    //acc_Filtered_w = DataThrFilter(acc_Filtered_w, 0.0001, 0.01);
        //    acc_Filtered_w = acc_linear;
        //        prevAccelerometerValue = acc_Filtered_w;


        //        //加速度檢測
        //        //bool haveacc = MotionDetec(acc_Filtered_w);
        //        //if (haveacc)
        //        //{
        //        //  計算線性速度並濾波
        //        Vector3D linear_Vel_ = prevVelocity + acc_Filtered_w * deltatime;

        //        //  低通濾波速度
        //        //linear_Vel_Filterd.X = LowPassFilter(linear_Vel_Filterd.X, linear_Vel_.X, 0.3, 0.001);
        //        //linear_Vel_Filterd.Y = LowPassFilter(linear_Vel_Filterd.Y, linear_Vel_.Y, 0.3, 0.001);
        //        //linear_Vel_Filterd.Z = LowPassFilter(linear_Vel_Filterd.Z, linear_Vel_.Z, 0.3, 0.001);
        //        linear_Vel_Filterd.X = HighPassFilter(linear_Vel_Filterd.X, linear_Vel_.X, 0.8);
        //        linear_Vel_Filterd.Y = HighPassFilter(linear_Vel_Filterd.Y, linear_Vel_.Y, 0.8);
        //        linear_Vel_Filterd.Z = HighPassFilter(linear_Vel_Filterd.Z, linear_Vel_.Z, 0.8);

        //        prevVelocity = linear_Vel_Filterd;
        //        //}
        //        //else
        //        //{
        //        //    linear_Vel_Filterd = new Vector3D(0, 0, 0);
        //        //    prevVelocity = new Vector3D(0, 0, 0);
        //        //}

        //        //速度檢測
        //        //bool Motion = MotionDetec(linear_Vel_Filterd);
        //        if (gyroscop.Length > 5)
        //        {
        //            MotionStatusOnDotCircle(true);    //  狀態改變 -(綠)

        //            //  計算位移
        //            Vector3D linear_posi_ = prevPosition + linear_Vel_Filterd * deltatime;

        //        //  低通濾波位移
        //        //Pos_Filtered.X = LowPassFilter(Pos_Filtered.X, linear_posi_.X, 0.4, 0.001);
        //        //Pos_Filtered.Y = LowPassFilter(Pos_Filtered.Y, linear_posi_.Y, 0.4, 0.001);
        //        //Pos_Filtered.Z = LowPassFilter(Pos_Filtered.Z, linear_posi_.Z, 0.4, 0.001);
        //        //Pos_Filtered.X = HighPassFilter(Pos_Filtered.X, linear_posi_.X, 0.8);
        //        //Pos_Filtered.Y = HighPassFilter(Pos_Filtered.Y, linear_posi_.Y, 0.8);
        //        //Pos_Filtered.Z = HighPassFilter(Pos_Filtered.Z, linear_posi_.Z, 0.8);

        //            Pos_Filtered = linear_posi_;
        //            prevPosition = Pos_Filtered;

        //            if (!IsNanData(Pos_Filtered)) { Pos = Pos_Filtered ; }
        //            else { Pos = new Vector3D(0, 0, 0); }
        //        }
        //        else
        //        {
        //            MotionStatusOnDotCircle(false);    //  狀態改變 -(紅)
        //            linear_Vel_Filterd = new Vector3D(0, 0, 0);
        //            prevVelocity = new Vector3D(0, 0, 0);
        //        }
        //    //}
        //    //else
        //    //{
        //    //    // 角速度高於閾值，不更新加速度和位移
        //    //    acc_Filtered_w = prevAccelerometerValue;
        //    //    linear_Vel_Filterd = prevVelocity;
        //    //    Pos = prevPosition;
        //    //}
        //}
        // ==================================================================================================================
        // ==================================================================================================================
        //private void calPos_1(Vector3D msg, List<float[]> dataflow)
        //{
        //    for(int i =0; i <= dataflow.Count; i++)
        //    {
        //        //四元數
        //        Quaternion orien_QuatConj = new Quaternion(-(float)dataflow[i].GetValue(6), -(float)dataflow[i].GetValue(7), -(float)dataflow[i].GetValue(8), (float)dataflow[i].GetValue(5));
        //        Quaternion orien_Quat = new Quaternion((float)dataflow[i].GetValue(6), (float)dataflow[i].GetValue(7), (float)dataflow[i].GetValue(8), (float)dataflow[i].GetValue(5));

        //        //感測加速度轉為世界座標
        //        Vector3D acc_w = RotateVectorByQuaternion(msg, orien_QuatConj);

        //        //重力加速度轉為世界座標
        //        gra_w = RotateVectorByQuaternion(gravity_b, orien_QuatConj);

        //        //計算此刻瞬時速度位置與，閾值濾波
        //        Vector3D linear_vel = new Vector3D(0, 0, 0);
        //        Vector3D acc_W = DataThrFilter(acc_w - gra_w, 0.1f);
        //        linear_vel = linear_vel_pre + 0.5f * deltaT_ * (acc_W + acc_pre_W);
        //        acc_pre_W = acc_W;

        //        //計算此刻位置，閾值濾波
        //        linear_vel = DataThrFilter(linear_vel, 0.05f);
        //        Pos += 0.5f * (linear_vel_pre + linear_vel) * deltaT_ * 100.0f;
        //        linear_vel_pre = linear_vel;
        //        acc_W = linear_vel = new Vector3D(0, 0, 0);

        //    }

        //}





        //private void calOrien(Vector3D msg)
        //{
        //    if (orien == null)
        //    {
        //        orien = Matrix3D.Identity;
        //    }
        //    Matrix3D B = new Matrix3D(); ; // 角速度 * 时间 = 角度（表示为反对称矩阵）
        //    B.M11 = 0; B.M12 = -msg.Z * deltaT_; B.M13 = msg.Y * deltaT_;
        //    B.M21 = msg.Z * deltaT_; B.M22 = 0; B.M23 = -msg.X * deltaT_;
        //    B.M31 = -msg.Y * deltaT_; B.M32 = msg.X * deltaT_; B.M33 = 0;

        //    double sigma = Math.Sqrt(Math.Pow(msg.X, 2) + Math.Pow(msg.Y, 2) + Math.Pow(msg.Z, 2)) * deltaT_;
        //    Matrix3D B2 = Matrix3D.Multiply(B, B);
        //    double sin_sig = (Math.Sin(sigma) / sigma); //垂直分量
        //    double cos_sig = -((1 - Math.Cos(sigma)) / Math.Pow(sigma, 2));  //水平分量
        //    Matrix3D sin = new Matrix3D(sin_sig, 0, 0, 0, 0, sin_sig, 0, 0, 0, 0, sin_sig, 0, 0, 0, 0, 1);
        //    Matrix3D cos = new Matrix3D(cos_sig, 0, 0, 0, 0, cos_sig, 0, 0, 0, 0, cos_sig, 0, 0, 0, 0, 1);

        //    Matrix3D result = new Matrix3D();
        //    result.Append(Matrix3D.Identity);
        //    result.Append(Matrix3D.Multiply(B, sin));
        //    result.Append(Matrix3D.Multiply(B2, cos));
        //    orien *= result;
        //}


        private void calPos(Vector3D msg, Vector3D gyroscop, Quaternion orienQuat, double deltatime)
        {
            //acc_linear = msg;
            //orienQuat.Conjugate();
            acc_w = msg;
            gravity_b = gyroscop;
            //RotateVectorByQuaternion(acc_linear, orienQuat);
            //Matrix3D traslationMatrix = QuaternionToRotationMatrix(orienQuat);
            //acc_w = traslationMatrix.Transform(acc_linear);

            //double gyroThreshold = 10000.0; // 角速度閾值
            //if (gyroscop.Length <= gyroThreshold)
            //{
            //  高通濾波加速度
            //acc_Filtered_w.X = LowPassFilter(acc_Filtered_w.X, acc_linear.X, 0.25, 1.5);
            //acc_Filtered_w.Y = LowPassFilter(acc_Filtered_w.Y, acc_linear.Y, 0.25, 1.5);
            //acc_Filtered_w.Z = LowPassFilter(acc_Filtered_w.Z, acc_linear.Z, 0.25, 1.5);
            //acc_Filtered_w.X = HighPassFilter(acc_Filtered_w.X, acc_linear.X, 0.8);
            //acc_Filtered_w.Y = HighPassFilter(acc_Filtered_w.Y, acc_linear.Y, 0.8);
            //acc_Filtered_w.Z = HighPassFilter(acc_Filtered_w.Z, acc_linear.Z, 0.8);
            acc_Filtered_w = acc_w;

            //acc_Filtered_w = DataThrFilter(acc_Filtered_w, 0.005, 0.9);

            //加速度檢測
            //bool haveacc = MotionDetec(acc_Filtered_w);
            //if (haveacc)
            //{

            //  計算線性速度並濾波
            Vector3D linear_Vel_ = prevVelocity + 0.5 * (acc_Filtered_w + prevAccelerometerValue) * deltatime;

            //  低通濾波速度
            //linear_Vel_Filterd.X = LowPassFilter(linear_Vel_Filterd.X, linear_Vel_.X, 0.9, 0.001);
            //linear_Vel_Filterd.Y = LowPassFilter(linear_Vel_Filterd.Y, linear_Vel_.Y, 0.6, 0.001);
            //linear_Vel_Filterd.Z = LowPassFilter(linear_Vel_Filterd.Z, linear_Vel_.Z, 0.6, 0.001);
            //  高通濾波速度
            //linear_Vel_Filterd.X = HighPassFilter(linear_Vel_Filterd.X, linear_Vel_.X, 0.5);
            //linear_Vel_Filterd.Y = HighPassFilter(linear_Vel_Filterd.Y, linear_Vel_.Y, 0.5);
            //linear_Vel_Filterd.Z = HighPassFilter(linear_Vel_Filterd.Z, linear_Vel_.Z, 0.5);
            linear_Vel_Filterd = linear_Vel_;

            //}
            //else
            //{
            //    linear_Vel_Filterd = new Vector3D(0, 0, 0);
            //    prevVelocity = new Vector3D(0, 0, 0);
            //}

            //速度檢測
            //bool Motion = MotionDetec(linear_Vel_Filterd);
            if (gyroscop.Length > 5)
            {
                MotionStatusOnDotCircle(true);    //  狀態改變 -(綠)

                //  計算位移
                Vector3D linear_posi_ = prevPosition + 0.5 * (prevVelocity + linear_Vel_Filterd) * deltatime;
                //prevPosition += 0.5 * (prevVelocity + linear_Vel_Filterd) * deltatime;
                Pos_Filtered = linear_posi_;
                //  低通濾波位移
                //Pos_Filtered.X = LowPassFilter(Pos_Filtered.X, linear_posi_.X, 0.9, 0.01);
                //Pos_Filtered.Y = LowPassFilter(Pos_Filtered.Y, linear_posi_.Y, 0.9, 0.01);
                //Pos_Filtered.Z = LowPassFilter(Pos_Filtered.Z, linear_posi_.Z, 0.9, 0.01);
                //  高通濾波位移
                //Pos_Filtered.X = HighPassFilter(Pos_Filtered.X, prevPosition.X, 0.6);
                //Pos_Filtered.Y = HighPassFilter(Pos_Filtered.Y, prevPosition.Y, 0.6);
                //Pos_Filtered.Z = HighPassFilter(Pos_Filtered.Z, prevPosition.Z, 0.6);

                if (!IsNanData(Pos_Filtered)) { Pos = Pos_Filtered * 100.0; }
                else { Pos = prevPosition * 100.0; }

                //儲存此刻速度，用以下一刻計算用
                prevPosition = Pos_Filtered;
                prevVelocity = linear_Vel_Filterd;
                prevAccelerometerValue = acc_Filtered_w;
            }
            else
            {
                MotionStatusOnDotCircle(false);    //  狀態改變 -(紅)
                linear_Vel_Filterd = new Vector3D(0, 0, 0);
                prevVelocity = new Vector3D(0, 0, 0);
            }
            //}
            //else
            //{
            //    // 角速度高於閾值，不更新加速度和位移
            //    acc_Filtered_w = prevAccelerometerValue;
            //    linear_Vel_Filterd = prevVelocity;
            //    Pos = prevPosition;
            //}
        }
        private void UpdateVelocity(double linear_acc, double dt, string axis)
        {
            double predictedVelocity_ = 0; double predictedPosition_ = 0;
            switch (axis)
            {
                case "X":
                    if (linear_acc > 0.0)
                    {
                        //預測步驟 ( 𝑝𝑖 = 𝑝𝑖−1 + 1/2×(𝑣𝑖−1+𝑣𝑖) × Δ𝑡 / 𝑣𝑖 = 𝑣𝑖−1 + 1/2 ×(𝑎N𝑖−1 +𝑎N𝑖) × Δ𝑡)
                        predictedVelocity_ = prevVelocity.X + 0.5 * (linear_acc + prevAccelerometerValue.X) * dt;
                        predictedPosition_ = prevPosition.X + 0.5 * (prevVelocity.X + predictedVelocity_) * dt;
                    }
                    else
                    {
                        //預測步驟( 𝑝𝑘 = 𝑝𝑘−1 + 𝑣𝑘−1 Δ𝑡 + 0.5(Δ𝑡)2𝑎N𝑥, /  𝑣𝑘 = 𝑣𝑘−1 + Δ𝑡𝑎N𝑥.)
                        predictedPosition_ = prevPosition.X + prevVelocity.X * dt + 0.5 * dt * dt * linear_acc;
                        predictedVelocity_ = prevVelocity.X + dt * linear_acc;
                    }
                    prevAccelerometerValue.X = linear_acc;
                    prevVelocity.X = predictedVelocity_;
                    prevPosition.X = predictedPosition_;
                    break;

                case "Y":
                    if (linear_acc > 0.01)
                    {
                        //預測步驟 ( 𝑝𝑖 = 𝑝𝑖−1 + 1/2×(𝑣𝑖−1+𝑣𝑖) × Δ𝑡 / 𝑣𝑖 = 𝑣𝑖−1 + 1/2 ×(𝑎N𝑖−1 +𝑎N𝑖) × Δ𝑡)
                        predictedVelocity_ = prevVelocity.Y + 0.5 * (linear_acc + prevAccelerometerValue.Y) * dt;
                        predictedPosition_ = prevPosition.Y + 0.5 * (prevVelocity.Y + predictedVelocity_) * dt;
                    }
                    else
                    {
                        //預測步驟( 𝑝𝑘 = 𝑝𝑘−1 + 𝑣𝑘−1 Δ𝑡 + 0.5(Δ𝑡)2𝑎N𝑥, /  𝑣𝑘 = 𝑣𝑘−1 + Δ𝑡𝑎N𝑥.)
                        predictedPosition_ = prevPosition.Y + prevVelocity.Y * dt + 0.5 * dt * dt * linear_acc;
                        predictedVelocity_ = prevVelocity.Y + dt * linear_acc;
                    }
                    prevAccelerometerValue.Y = linear_acc;
                    prevVelocity.Y = predictedVelocity_;
                    prevPosition.Y = predictedPosition_;
                    break;

                case "Z":
                    if (linear_acc > 0.01)
                    {
                        //預測步驟 ( 𝑝𝑖 = 𝑝𝑖−1 + 1/2×(𝑣𝑖−1+𝑣𝑖) × Δ𝑡 / 𝑣𝑖 = 𝑣𝑖−1 + 1/2 ×(𝑎N𝑖−1 +𝑎N𝑖) × Δ𝑡)
                        predictedVelocity_ = prevVelocity.Z + 0.5 * (linear_acc + prevAccelerometerValue.Z) * dt;
                        predictedPosition_ = prevPosition.Z + 0.5 * (prevVelocity.Z + predictedVelocity_) * dt;
                    }
                    else
                    {
                        //預測步驟( 𝑝𝑘 = 𝑝𝑘−1 + 𝑣𝑘−1 Δ𝑡 + 0.5(Δ𝑡)2𝑎N𝑥, /  𝑣𝑘 = 𝑣𝑘−1 + Δ𝑡𝑎N𝑥.)
                        predictedPosition_ = prevPosition.Z + prevVelocity.Z * dt + 0.5 * dt * dt * linear_acc;
                        predictedVelocity_ = prevVelocity.Z + dt * linear_acc;
                    }
                    prevAccelerometerValue.Z = linear_acc;
                    prevVelocity.Z = predictedVelocity_;
                    prevPosition.Z = predictedPosition_;
                    break;
            }

        }

        /// <summary>
        /// 【空間定位】 -更新空間座標顯示
        /// </summary>
        /// <param name="pos"></param>
        private void updatePosOnDisplay(Vector3D pos)
        {
            if (isScanning)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    lb_xPos.Text = pos.X.ToString("F2");
                    lb_yPos.Text = pos.Y.ToString("F2");
                    lb_zPos.Text = pos.Z.ToString("F2");
                });
            }
            else
            {
                lb_xPos.Text = "0.00";
                lb_yPos.Text = "0.00";
                lb_zPos.Text = "0.00";

            }

            //for (int i = j; i < point_posi.Size; i++)
            //{
            //    this.Invoke((MethodInvoker)delegate
            //    {
            //        lb_xPos.Text = point_posi.GetX(i).ToString("F3");
            //        lb_yPos.Text = point_posi.GetY(i).ToString("F3");
            //        lb_zPos.Text = point_posi.GetZ(i).ToString("F3");
            //        j++;
            //    });

            //}
        }

        /// <summary>
        ///【空間定位】-設定環境的重力加速度
        /// </summary>
        /// <param name="msg"></param>
        private void UpdateGravity(Quaternion orienQua)
        {
            double q0 = orienQua.W;
            double q1 = orienQua.X;
            double q2 = orienQua.Y;
            double q3 = orienQua.Z;
            gravity_b.X = 2 * (q1 * q3 - q0 * q2);
            gravity_b.Y = 2 * (q0 * q1 + q2 * q3);
            gravity_b.Z = (q0 * q0 - q1 * q1 - q2 * q2 + q3 * q3);
            //gravity_b = RotateVectorByQuaternion(msg, orienQua);
            //if (msg.X> 8) { gravity_b = new Vector3D(9.80665, 0, 0); }
            //else if (msg.Y > 8) { gravity_b = new Vector3D(0, 9.80665, 0); }
            //else if (msg.Z > 8){ gravity_b = new Vector3D(0, 0, 9.80665); }
            //else if (msg.Z < 8){ gravity_b = new Vector3D(0, 0, -9.80665); }
            //else if (msg.Y < 8){ gravity_b = new Vector3D(0, -9.80665, 0); }
            //else { gravity_b = new Vector3D(-9.80665, 0, 0); }
            //gravity_b = new Vector3D(0, 0, 9.80665);
        }

        /// <summary>
        /// 【空間定位】 -感測有無在位移，有則回傳true
        /// </summary>
        /// <param name="inputdata"></param>
        /// <returns></returns>
        private bool MotionDetec(Vector3D acc)
        {
            bool Motioning;
            if (acc.X != 0 || acc.Y != 0 || acc.Z != 0) { Motioning = true; }
            else { Motioning = false; }
            return Motioning;
        }
        private bool MotionDetec(Quaternion dis)
        {
            bool Motioning;
            if (dis.X != 0 || dis.Y != 0 || dis.Z != 0) { Motioning = true; }
            else { Motioning = false; }
            return Motioning;
        }

        #endregion // 空間定位

        #region Scan子頁面
        /// <summary>
        /// 【Scan子頁面】 - 掃描模式選單
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubMenu1_Scan_DimentionChanged(object sender, EventArgs e)
        {
            Dimention = SubMenu1_Scan.comboBox_ScanMode.Text;
        }

        /// <summary>
        /// 【Scan子頁面】 - Pause按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScanPause_Click(object sender, EventArgs e)
        {
            if (com.IsOpen && isScanning)
            {
                if (isTimerRunning)
                {
                    //  暫停計時器
                    isTimerRunning = false;
                    //  控件更動
                    SubMenu1_Scan.iconbtn_Pause.Text = "Continue";
                    this.SubMenu1_Scan.iconbtn_Pause.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
                    this.SubMenu1_Scan.iconbtn_Pause.Font = new System.Drawing.Font("微軟正黑體", 7.8F, System.Drawing.FontStyle.Bold);
                    this.SubMenu1_Scan.iconbtn_Scan.Enabled = false;
                    rtb_MainStatusAutoScroll("暫停掃描");
                    //  暫停讀取數據
                    com.DataReceived -= com_DataReceived;
                    //  畫面更新
                    if (this.InvokeRequired)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            io.UpdateForm(point_posi);
                        });
                    }
                    else { io.UpdateForm(point_posi); }
                    io.renderProcessingLock = false;
                }
                else
                {
                    //  控件更動
                    this.SubMenu1_Scan.iconbtn_Pause.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
                    this.SubMenu1_Scan.iconbtn_Pause.Font = new System.Drawing.Font("微軟正黑體", 7.8F, System.Drawing.FontStyle.Regular);
                    SubMenu1_Scan.iconbtn_Pause.Text = "Pause";
                    this.SubMenu1_Scan.iconbtn_Scan.Enabled = true;
                    rtb_MainStatusAutoScroll("繼續掃描");
                    //  重啟讀取數據
                    com.DataReceived += com_DataReceived;
                    //  重啟計時器
                    isTimerRunning = true;
                    //  讀取顏色條狀態
                    bool isScalarBarOpend = io.isScalarBarOpen;
                    //  畫面更新
                    if (io != null)
                    {
                        
                        io.Dispose();
                        io = new IOpcForm();
                        OpenChildform(io);
                    }
                    io.isScalarBarOpen = isScalarBarOpend;  //  回存狀態
                }
            }
            else { rtb_MainStatusAutoScroll("COM未開啟或未開啟掃描模式"); }
        }

        /// <summary>
        /// 【Scan子頁面 】- Reset按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScanReset_Click(object sender, EventArgs e)
        {
            if (!isTimerRunning)
            {
                resetAllVariableValue();
                rtb_MainStatusAutoScroll("已重置完成！");
            }
            else { rtb_MainStatusAutoScroll("掃描中不可重置，需先暫停後再試一次"); }
        }

        /// <summary>
        /// 【Scan子頁面 】- Save & Reset 按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubMenu1_Scan_SaveAndReset_Click(object sender, EventArgs e)
        {
            //===== 儲存一筆數據並重置欄位 =====
            SaveforAnalysis(includedTotalLenght);
            resetAllVariableValue();
            rtb_MainStatusAutoScroll("已重置完成！");
            //==================================

            //======== 連續儲存多筆數據 =========
            //if (!isRecording)
            //{
            //    // 物件(按鈕)狀態改變
            //    this.SubMenu1_Scan.iconButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            //    this.SubMenu1_Scan.iconButton2.Font = new System.Drawing.Font("微軟正黑體", 7.8F, System.Drawing.FontStyle.Bold);

            //    //TimerCallback savedataCallBack = (obj) => DataTimer_Elapsed(obj, includedTotalLenght, 'Z'); //  參數2：輸入要儲存的數據(Vector3D)  |  參數3：欲儲存的數據位置
            //    //AutoResetEvent autoEvent1 = new AutoResetEvent(false);   //  同步基元類型，用於多執行緒間同步
            //    //dataTimer = new System.Threading.Timer(savedataCallBack, autoEvent1, 100, 1000 / 600);

            //    dataToSave = new List<string>();

            //    pre_distanceRawdataBuf = 0;
            //    pre_RecordingTime = 0;

            //    isRecording = true;
            //    isInitialRecordingTime = false;
            //}
            //else
            //{
            //    // 物件(按鈕)狀態改變
            //    this.SubMenu1_Scan.iconButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            //    this.SubMenu1_Scan.iconButton2.Font = new System.Drawing.Font("微軟正黑體", 7.8F, System.Drawing.FontStyle.Bold);

            //    //dataTimer.Dispose();    //  關閉計時器

            //    if (dataToSave.Count > 0)
            //    {
            //        ContinuousSaveforAnalysis(dataToSave);
            //        dataToSave.Clear();  // 清空已儲存的數據
            //    }

            //    isRecording = false;
            //}
            //==============================
        }

        /// <summary>
        /// 【Scan子頁面 】-啟動/暫停感測器傳送數據的開關
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSensorCTR_Click(object sender, EventArgs e)
        {
            try
            {
                if (SubMenu1_Scan.SensorOperatingStatus != true)
                {
                    com.Write("SensorBtnON");
                    rtb_MainStatusAutoScroll("感測器開始傳送數據");
                }
                else
                {
                    com.Write("SensorBtnOFF");
                    rtb_MainStatusAutoScroll("感測器停止傳送數據");
                }
            }
            catch (Exception ex)
            { MessageBox.Show("命令傳送失敗： " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        /// <summary>
        /// 【Scan子頁面 】-重置感測器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iconBtn_RestartSensor_Click(object sender, EventArgs e)
        {
            try
            {
                if (com.IsOpen)
                {
                    com.Write("ResetSensor");
                    rtb_MainStatusAutoScroll("已重新啟動感測器");
                }
                else { rtb_MainStatusAutoScroll("SerialPort未連線成功！"); }
            }
            catch (Exception ex)
            { MessageBox.Show("命令傳送失敗： " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        /// <summary>
        /// 【Scan子頁面 】-增加 Lift_Detection_Thr 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iconBtn_liftDetecDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (com.IsOpen)
                {
                    com.Write("Lift_Detection_Thr_Down");
                    rtb_MainStatusAutoScroll("減少 Lift Detection ");
                }
                else { rtb_MainStatusAutoScroll("SerialPort未連線成功！"); }
            }
            catch (Exception ex)
            { MessageBox.Show("命令傳送失敗： " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        /// <summary>
        /// 【Scan子頁面 】-減少 Lift_Detection_Thr 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iconBtn_liftDetecUP_Click(object sender, EventArgs e)
        {
            try
            {
                if (com.IsOpen)
                {
                    com.Write("Lift_Detection_Thr_UP");
                    rtb_MainStatusAutoScroll("增加 Lift Detection");
                }
                else { rtb_MainStatusAutoScroll("SerialPort未連線成功！"); }
            }
            catch (Exception ex)
            { MessageBox.Show("命令傳送失敗： " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        /// <summary>
        /// 【Scan子頁面 】-減少 Frame_Period_Min_Bound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iconBtn_MinBoundDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (com.IsOpen)
                {
                    com.Write("MinBoundDown");
                    rtb_MainStatusAutoScroll("減少 Frame_Period_Min_Bound");
                }
                else { rtb_MainStatusAutoScroll("SerialPort未連線成功！"); }
            }
            catch (Exception ex)
            { MessageBox.Show("命令傳送失敗： " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        /// <summary>
        /// 【Scan子頁面 】-增加 Frame_Period_Min_Bound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iconBtn_MinBoundUP_Click(object sender, EventArgs e)
        {
            try
            {
                if (com.IsOpen)
                {
                    com.Write("MinBoundUP");
                    rtb_MainStatusAutoScroll("增加 Frame_Period_Min_Bound");
                }
                else { rtb_MainStatusAutoScroll("SerialPort未連線成功！"); }
            }
            catch (Exception ex)
            { MessageBox.Show("命令傳送失敗： " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        /// <summary>
        /// 【Scan子頁面 】-減少 Shutter_Max_Bound.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iconBtn_ShutterDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (com.IsOpen)
                {
                    com.Write("ShutterDown");
                    rtb_MainStatusAutoScroll("減少 Shutter_Max_Bound");
                }
                else { rtb_MainStatusAutoScroll("SerialPort未連線成功！"); }
            }
            catch (Exception ex)
            { MessageBox.Show("命令傳送失敗： " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        /// <summary>
        /// 【Scan子頁面 】-增加 Shutter_Max_Bound.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iconBtn_ShutterUP_Click(object sender, EventArgs e)
        {
            try
            {
                if (com.IsOpen)
                {
                    com.Write("ShutterUP");
                    rtb_MainStatusAutoScroll("增加 Shutter_Max_Bound");
                }
                else { rtb_MainStatusAutoScroll("SerialPort未連線成功！"); }
            }
            catch (Exception ex)
            { MessageBox.Show("命令傳送失敗： " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        /// <summary>
        /// 【Scan子頁面 】-控制 Laser 亮度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubMenu1_Scan_trackBar_CPIchanged_MouseUp(object sender, EventArgs e)
        {
            try
            {
                bool valueVisible = SubMenu1_Scan.label_lightness.Visible;
                if (valueVisible) { SubMenu1_Scan.label_lightness.Visible = false; }
                if (com.IsOpen)
                {
                    String value = SubMenu1_Scan.trackBar_LEDIntensity.Value.ToString();
                    com.Write("CPI" + value);
                    CPI = SubMenu1_Scan.trackBar_LEDIntensity.Value * 200;
                    rtb_MainStatusAutoScroll("CPI: " + CPI.ToString("F0"));
                }
                else { rtb_MainStatusAutoScroll("SerialPort未連線成功！"); }
            }
            catch (Exception ex)
            { MessageBox.Show("命令傳送失敗： " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        /// <summary>
        /// 【Scan子頁面 】-即時顯示LASER亮度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubMenu1_Scan_LASERIntensityChanged(object sender, EventArgs e)
        {
            try
            {
                SubMenu1_Scan.label_lightness.Visible = true;
                SubMenu1_Scan.label_lightness.Text = SubMenu1_Scan.trackBar_LEDIntensity.Value.ToString();
            }
            catch (Exception ex)
            { MessageBox.Show("命令傳送失敗： " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }


        #endregion  //  Scan子頁面

        #region 資料處理

        /// <summary>
        /// 【資料處理】 -數據重新歸零
        /// </summary>
        private void resetAllVariableValue()
        {
            prev_time_ = 0;
            buf_time = 0;
            deltaT_ = 0;

            gravity_b = new Vector3D(0, 0, 0);
            gravity_w = new Vector3D(0, 0, 0);
            gyro_b = new Vector3D(0, 0, 0);
            gyro_w = new Vector3D(0, 0, 0);
            acc_w = new Vector3D(0, 0, 0);
            acc_linear = new Vector3D(0, 0, 0);
            acc_b = new Vector3D(0, 0, 0);
            acc_Filtered_w = new Vector3D(0, 0, 0);
            linear_vel_w = new Vector3D(0, 0, 0);
            linear_Vel_Filterd = new Vector3D(0, 0, 0);
            Pos = new Vector3D(0, 0, 0);
            Pos_Filtered = new Vector3D(0, 0, 0);
            includedTotalLenght = new Vector3D(0, 0, 0);
            distance_transformed = new Vector3D(0, 0, 0);
            PointCloud = new Vector3D(0, 0, 0);
            TotalLenght_2D = 0;
            TotalLenghtofX_2D = 0;
            TotalLenghtofY_2D = 0;
            TotalLenghtofX_buf = 0;
            TotalLenghtofY_buf = 0;
            maxSpeed = 0.0;
            Deltatime = 0.0;
            NumofData = 0;

            acc_Noise = new Vector3D(0, 0, 0);

            prevVelocity = new Vector3D(0, 0, 0);
            prevPosition = new Vector3D(0, 0, 0);
            prevAccelerometerValue = new Vector3D(0, 0, 0);

            point_posi.Clear();

            SampleNum = 0;

            firstFrame_ = true;

            bool isScalarBarStatus;
            //  畫面更新
            if (io != null)
            {
                //  讀取顏色條狀態
                isScalarBarStatus = io.isScalarBarOpen;

                io.Dispose();
                io = new IOpcForm();
                OpenChildform(io);

                io.isScalarBarOpen = isScalarBarStatus;  //  回存狀態
            }
        }

        /// <summary>
        /// 【資料處理】 -依據四元數運算公式計算座標旋轉
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="quaternion"></param>
        /// <returns></returns>
        private Vector3D RotateVectorByQuaternion(Vector3D vector, Quaternion OreinQuaternion)
        {
            if (!IsNanData(vector))
            {
                //單位四元數
                double vectorLength = vector.Length;
                if (vectorLength == 0)
                {
                    return new Vector3D(0, 0, 0);
                }
                vector.Normalize();
                Quaternion vectorQuat;  //儲存純旋轉數據
                Quaternion buf_Orein = OreinQuaternion;
                //判斷數據數量
                if (vector.Z != 0) { vectorQuat = new Quaternion(vector.X, vector.Y, vector.Z, 0); }
                else { vectorQuat = new Quaternion(vector.X, vector.Y, 0, 0); }
                //共軛四元數
                Quaternion OreinQuaternionConj = buf_Orein;
                OreinQuaternionConj.Conjugate();
                //四元數運算
                Quaternion resultQuat = buf_Orein * vectorQuat * OreinQuaternionConj;
                return new Vector3D(resultQuat.X, resultQuat.Y, resultQuat.Z) * vectorLength;
                //return new Vector3D(Math.Round(resultQuat.X * vectorLength, 2), Math.Round(resultQuat.Y * vectorLength, 2), Math.Round(resultQuat.Z * vectorLength, 2));

            }
            else { return new Vector3D(0, 0, 0); }
        }
        private Vector3D RotateVectorByQuaternion_2(Vector3D vector, Quaternion OreinQuaternion)
        {
            if (!IsNanData(vector))
            {
                // 定義絕對座標中的加速度變量
                double absoluteAccelerometerX, absoluteAccelerometerY, absoluteAccelerometerZ;

                double q0 = OreinQuaternion.W;
                double q1 = OreinQuaternion.X;
                double q2 = OreinQuaternion.Y;
                double q3 = OreinQuaternion.Z;

                //  轉為座標轉換矩陣
                double qx2 = q1 * q1;
                double qy2 = q2 * q2;
                double qz2 = q3 * q3;
                double qxqy = q1 * q2;
                double qxqz = q1 * q3;
                double qyqz = q2 * q3;
                double qxqw = q1 * q0;
                double qyqw = q2 * q0;
                double qzqw = q3 * q0;

                absoluteAccelerometerX = 2 * (0.5 - qy2 - qz2) * vector.X + 2 * (qxqy - qzqw) * vector.Y + 2 * (qxqz + qyqw) * vector.Z;
                absoluteAccelerometerY = 2 * (qxqy + qzqw) * vector.X + 2 * (0.5 - qx2 - qz2) * vector.Y + 2 * (qyqz - qxqw) * vector.Z;
                absoluteAccelerometerZ = 2 * (qxqz - qyqw) * vector.X + 2 * (qyqz + qxqw) * vector.Y + 2 * (0.5 - qx2 - qy2) * vector.Z;

                return new Vector3D(absoluteAccelerometerX, absoluteAccelerometerY, absoluteAccelerometerZ);
            }
            else { return new Vector3D(0, 0, 0); }
        }
        private Vector3D RotateQuaternionByQuaternion(Quaternion Qua, Quaternion OreinQuaternion)
        {
            Quaternion quaternionConj = OreinQuaternion;
            quaternionConj.Conjugate();
            Quaternion resultQuat = OreinQuaternion * Qua * quaternionConj;
            return new Vector3D(resultQuat.X, resultQuat.Y, resultQuat.Z);
        }
        public static Matrix3D QuaternionToRotationMatrix(Quaternion quaternion)
        {
            Matrix3D matrix = new Matrix3D();

            matrix.M11 = 1 - 2 * (quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z);
            matrix.M12 = 2 * (quaternion.X * quaternion.Y - quaternion.W * quaternion.Z);
            matrix.M13 = 2 * (quaternion.X * quaternion.Z + quaternion.W * quaternion.Y);

            matrix.M21 = 2 * (quaternion.X * quaternion.Y + quaternion.W * quaternion.Z);
            matrix.M22 = 1 - 2 * (quaternion.X * quaternion.X + quaternion.Z * quaternion.Z);
            matrix.M23 = 2 * (quaternion.Y * quaternion.Z - quaternion.W * quaternion.X);

            matrix.M31 = 2 * (quaternion.X * quaternion.Z - quaternion.W * quaternion.Y);
            matrix.M32 = 2 * (quaternion.Y * quaternion.Z + quaternion.W * quaternion.X);
            matrix.M33 = 1 - 2 * (quaternion.X * quaternion.X + quaternion.Y * quaternion.Y);

            return matrix;
        }

        private void CalculatemaxSpeed(double deltaDistance, double deltaTime)
        {
            // 取得瞬時位移速度
            double speed;
            if (dt > 0.0) { speed = Math.Round(((deltaDistance / deltaTime) - 0.0019) / 1.15, 1); }
            else { speed = 0; }

            
            // 更新最高速度
            if (speed > maxSpeed)
            {
                maxSpeed = speed;
            }
        }

        /// <summary>
        /// 【資料處理】 -判斷數據有無NAN
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool IsNanData(Vector3D input)
        {
            bool isNan;
            if (double.IsNaN(input.X) || double.IsNaN(input.Y) || double.IsNaN(input.Z)) { isNan = true; }
            else { isNan = false; }
            return isNan;
        }

        /// <summary>
        /// 【資料處理】 -與 correctdata 相減獲得的總平均雜訊資料
        /// </summary>
        /// <param name="inputdata"></param>
        /// <param name="correctdata"></param>
        private Vector3D AvarageNoisyValue(Vector3D inputdata, Vector3D correctdata)
        {
            ++SampleNum;
            double[] Noise = new double[3];
            if (inputdata.Length != 0)
            {
                Noise[0] = ((SampleNum - 1) * acc_Noise.X + inputdata.X - correctdata.X) / SampleNum;
                Noise[1] = ((SampleNum - 1) * acc_Noise.Y + inputdata.Y - correctdata.Y) / SampleNum;
                Noise[2] = ((SampleNum - 1) * acc_Noise.Z + inputdata.Z - correctdata.Z) / SampleNum;
            }
            else { Noise[0] = Noise[1] = Noise[2] = 0; }

            return new Vector3D(Noise[0], Noise[1], Noise[2]);
        }

        /// <summary>
        /// 【資料處理】 -滑動平均取得動態偏差
        /// </summary>
        /// <param name="inputdata"></param>
        /// <param name="filter_N"></param>
        /// <returns></returns>
        private Vector3D MovingAvarageBiasValue(Vector3D inputdata)
        {
            double filter_SumX = 0;
            double filter_SumY = 0;
            double filter_SumZ = 0;
            buf_bias_x[filter_N] = inputdata.X;
            buf_bias_y[filter_N] = inputdata.Y;
            buf_bias_z[filter_N] = inputdata.Z;
            for (int i = 0; i < filter_N; i++)
            {
                buf_bias_x[i] = buf_bias_x[i + 1];
                buf_bias_y[i] = buf_bias_y[i + 1];
                buf_bias_z[i] = buf_bias_z[i + 1];
                filter_SumX += buf_bias_x[i];
                filter_SumY += buf_bias_y[i];
                filter_SumZ += buf_bias_z[i];
            }

            return new Vector3D(filter_SumX / filter_N, filter_SumY / filter_N, filter_SumZ / filter_N);
        }

        /// <summary>
        /// 【資料處理】 -四元數轉換為尤拉角
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public Vector3D ConvertToEulerAngles(Quaternion q)
        {
            Vector3D angle = new Vector3D(0, 0, 0);
            angle.X = Math.Atan2(2 * (q.Y * q.Z - q.W * q.X), 2 * q.W * q.W - 1 + 2 * q.Z * q.W);   // phi-Z
            angle.Y = -Math.Atan((2.0 * (q.X * q.W + q.W * q.Y)) / Math.Sqrt(1.0 - Math.Pow((2.0 * q.X * q.Z + 2.0 * q.W * q.Y), 2.0)));   // theta - Y
            angle.Z = Math.Atan2(2 * (q.X * q.Y - q.W * q.Z), 2 * q.W * q.W - 1 + 2 * q.X * q.X);   // psi-X
            return angle;
        }

        /// <summary>
        /// 【資料處理】 -按鈕點擊來觸發向量數據的儲存
        /// </summary>
        /// <param name="data"></param>
        private void SaveforAnalysis(Vector3D data)
        {
            string SavePath = "..\\..\\..\\AnalysisData/";
            // 取得當前按鈕的父控制項，即所在的 GroupBox
            string groupName = "位移距離";   // 取得 GroupBox 的名稱
            String completePath = SavePath + groupName + ".csv";

            // 檢查檔案是否存在，若不存在則創建
            if (!File.Exists(completePath))
            {
                using (StreamWriter writer = File.CreateText(completePath)) { }
            }

            String alldata = data.X.ToString() + "," + data.Y.ToString() + "," + data.Z.ToString();

            WriteToFile(alldata, completePath);
            AnalysisChart.Storagednum++;
        }

        /// <summary>
        /// 【資料處理】 -週期性的儲存數據
        /// </summary>
        /// <param name="data"></param>
        private void ContinuousSaveforAnalysis(List<string> data)
        {
            string SavePath = "..\\..\\..\\AnalysisData/";
            // 取得當前按鈕的父控制項，即所在的 GroupBox
            string groupName = "位移距離";   // 取得 GroupBox 的名稱
            String completePath = SavePath + groupName + ".csv";

            // 檢查檔案是否存在，若不存在則創建
            if (!File.Exists(completePath))
            {
                using (StreamWriter writer = File.CreateText(completePath)) { }
            }

            StringBuilder alldata = new StringBuilder();

            foreach (string line in data)
            {
                alldata.AppendLine(line);
            }

            WriteToFile(alldata.ToString(), completePath);
            AnalysisChart.Storagednum++;
        }

        /// <summary>
        /// 【資料處理】 -週期性的儲存單軸上的數據
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        /// <param name="Axis"></param>
        private void DataTimer_Elapsed(Vector3D data, char Axis, ulong currentTime)
        {
            string input = data.X.ToString() + "," + data.Y.ToString() + "," + data.Z.ToString(); // + "," + currentTime.ToString()

            //  取得 X 軸上的數據
            if (Axis == 'X')
            {
                if (!string.IsNullOrEmpty(input) && data.X != 0.0)
                {
                    double Displacement_diff = Math.Abs(data.X - pre_distanceRawdataBuf);   //  計算位移差

                    if (Displacement_diff <= 1)
                    {
                        dataToSave.Add(input);
                    }
                    pre_distanceRawdataBuf = data.X;
                }
            }
            //  取得 Y 軸上的數據
            if (Axis == 'Y')
            {
                if (!string.IsNullOrEmpty(input) && data.Y != 0.0)
                {
                    double Displacement_diff = Math.Abs(data.Y - pre_distanceRawdataBuf);   //  計算位移差

                    if (Displacement_diff <= 1)
                    {
                        dataToSave.Add(input);
                    }
                    pre_distanceRawdataBuf = data.Y;
                }
            }
            if (Axis == 'Z')
            {
                if (!string.IsNullOrEmpty(input) && data.Z != 0.0000)
                {
                    //  計算相對於第一刻的時間戳
                    double RelativeTime = 0;
                    if (!isInitialRecordingTime)
                    {
                        pre_RecordingTime = currentTime;
                        isInitialRecordingTime = true;
                    }
                    else
                    {
                       RelativeTime = (currentTime - pre_RecordingTime) / 1E06;
                    }

                    input = RelativeTime.ToString() + "," + input;
                    dataToSave.Add(input);  //  紀錄位移速度
                }
            }

        }

        /// <summary>
        /// 【資料處理】 -位移數據的一次回歸分析
        /// </summary>
        /// <param name="measurement"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        private double RegressionFunc(double measurement, char axis)
        {
            double dis;
            double _dis1 = measurement;

            //  無截距
            if (axis == 'X')
            {
                if (_dis1 > 0) { dis = (1.0 / 0.5833) * (_dis1); }
                else if (_dis1 < 0) { dis = (1.0 / 0.5761) * (_dis1); }
                else { dis = _dis1; }
            }
            else
            {
                if (_dis1 > 0) { dis = (1.0 / 0.5467) * (_dis1); }
                else if (_dis1 < 0) { dis = (1.0 / 0.5409) * (_dis1); }
                else { dis = _dis1; }
            }

            //  有截距
            //if (axis == 'X')
            //{
            //    if (_dis1 > 0) { dis = (1.0 / 0.6167) * (_dis1 + 0.0012); }
            //    else if (_dis1 < 0) { dis = (1.0 / 0.6087) * (_dis1 - 0.0011); }
            //    else { dis = _dis1; }
            //}
            //else
            //{
            //    if (_dis1 > 0) { dis = (1.0 / 0.5777) * (_dis1 + 0.0011); }
            //    else if (_dis1 < 0) { dis = (1.0 / 0.5759) * (_dis1 - 0.0012); }
            //    else { dis = _dis1; }
            //}

            return Math.Round(dis, 4);  //  回傳進位到小數點後第三位的數值
            //return dis;
        }

        /// <summary>
        /// 【資料處理】 -位移數據的二次迴歸分析
        /// </summary>
        /// <param name="measurement"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        private double RegressionFunc2(double measurement, char axis)
        {
            double dis;
            double _dis2 = measurement;

            if (axis == 'X')
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

        private void WriteToFile(string value, String filename)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(filename))
                {
                    writer.WriteLine(value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("儲存失敗： " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //public static  Quaternion operator *(Quaternion q1, Quaternion q2)
        //{
        //    double nw = q1.W * q2.W - q1.X * q2.X - q1.Y * q2.Y - q1.Z * q2.Z;
        //    double nx = q1.W * q2.X + q1.X * q2.W + q1.Y * q2.Z - q1.Z * q2.Y;
        //    double ny = q1.W * q2.Y + q1.Y * q2.W + q1.Z * q2.X - q1.X * q2.Z;
        //    double nz = q1.W * q2.Z + q1.Z * q2.W + q1.X * q2.Y - q1.Y * q2.X;
        //    return new Quaternion(nw, nx, ny, nz);
        //}


        //private Vector3D AvarageBiasValue(Vector3D inputdata)
        //{
        //    if (BiasSample < filter_N)
        //    {
        //        filter_SumX += inputdata.X;
        //        filter_SumY += inputdata.Y;
        //        filter_SumZ += inputdata.Z;

        //        BiasSample++;
        //        return new Vector3D(filter_SumX / filter_N, filter_SumY / filter_N, filter_SumZ / filter_N);
        //    }
        //    else 
        //    { 
        //        calBias = false;
        //        return new Vector3D(filter_SumX / filter_N, filter_SumY / filter_N, filter_SumZ / filter_N);
        //    }

        //}

        #endregion //資料處理

        #endregion

        #region Editor欄位的顯示與隱藏
        /// <summary>
        /// 視窗大小改變時觸發
        /// </summary>
        private void FrmSize()
        {
            //左半部頁面
            SplitterEditorVisua.Size = new Size(this.Size.Width, (this.Size.Height - panelTitleBar.Size.Height - menuStrip1.Size.Height - 15));

            ModifySubMenuStatus();

            //下半部頁面(PanelMode)
            int averagWidth = (panelSubmode.Width - (72 * 3) - 116) / 5;
            iconBtnSetting.Location = new Point(averagWidth, 0);
            iconBtnScan.Location = new Point(averagWidth * 2 + 72, 0);
            iconBtnCurFil.Location = new Point(averagWidth * 3 + 72 * 2, 0);
            iconBtnOutput.Location = new Point(averagWidth * 4 + 72 * 2 + 116, 0);

        }

        private void ModifySubMenuStatus()
        {
            switch (subMenu)
            {
                case subMenu1Status.SubMenu1_Setting:
                    if (!subMenu1_Setting.ShowStatus)
                    {
                        int temDistance = SplitterEditorVisua.Size.Width - SplitterEditorVisua.Panel2.Width - SplitterEditorVisua.SplitterWidth;
                        subMenu1_Setting.Location = new Point(0, 0);

                        if (!(temDistance < 350))
                        {
                            SplitterEditorVisua.SplitterDistance = temDistance;
                            rtb_MainStatus.Width = temDistance - subMenu1_Setting.btnMinimizwEditor.Width;
                            SplitterEditorVisua.SplitterDistance = 350;
                            rtb_MainStatus.Width = 329;
                            rtb_MainStatus.Visible = true;

                        }
                        else
                        {
                            SplitterEditorVisua.SplitterDistance = 350;
                            rtb_MainStatus.Width = 329;
                            rtb_MainStatus.Visible = true;
                        }
                    }
                    else
                    {
                        subMenu1_Setting.Location = new Point(-(subMenu1_Setting.Width - subMenu1_Setting.btnMinimizwEditor.Width), 0);
                        SplitterEditorVisua.SplitterDistance = subMenu1_Setting.btnMinimizwEditor.Width;
                        rtb_MainStatus.Visible = false;

                    }
                    break;
                case subMenu1Status.SubMenu1_Scan:
                    if (!SubMenu1_Scan.ShowStatus)
                    {
                        int temDistance = SplitterEditorVisua.Size.Width - SplitterEditorVisua.Panel2.Width - SplitterEditorVisua.SplitterWidth;
                        SubMenu1_Scan.Location = new Point(0, 0);

                        if (!(temDistance < 350))
                        {
                            SplitterEditorVisua.SplitterDistance = temDistance;
                            rtb_MainStatus.Width = temDistance - SubMenu1_Scan.btnMinimizwEditor.Width;
                            SplitterEditorVisua.SplitterDistance = 350;
                            rtb_MainStatus.Width = 329;
                            rtb_MainStatus.Visible = true;

                        }
                        else
                        {
                            SplitterEditorVisua.SplitterDistance = 350;
                            rtb_MainStatus.Width = 329;
                            rtb_MainStatus.Visible = true;
                        }
                    }
                    else
                    {
                        SubMenu1_Scan.Location = new Point(-(SubMenu1_Scan.Width - SubMenu1_Scan.btnMinimizwEditor.Width), 0);
                        SplitterEditorVisua.SplitterDistance = SubMenu1_Scan.btnMinimizwEditor.Width;
                        rtb_MainStatus.Visible = false;

                    }
                    break;
                case subMenu1Status.SubMenu1_CurFil:
                    if (!SubMenu1_CurFil.ShowStatus)
                    {
                        int temDistance = SplitterEditorVisua.Size.Width - SplitterEditorVisua.Panel2.Width - SplitterEditorVisua.SplitterWidth;
                        SubMenu1_CurFil.Location = new Point(0, 0);

                        if (!(temDistance < 350))
                        {
                            SplitterEditorVisua.SplitterDistance = temDistance;
                            rtb_MainStatus.Width = temDistance - SubMenu1_CurFil.btnMinimizwEditor.Width;
                            SplitterEditorVisua.SplitterDistance = 350;
                            rtb_MainStatus.Width = 329;
                            rtb_MainStatus.Visible = true;

                        }
                        else
                        {
                            SplitterEditorVisua.SplitterDistance = 350;
                            rtb_MainStatus.Width = 329;
                            rtb_MainStatus.Visible = true;
                        }
                    }
                    else
                    {
                        SubMenu1_CurFil.Location = new Point(-(SubMenu1_CurFil.Width - SubMenu1_CurFil.btnMinimizwEditor.Width), 0);
                        SplitterEditorVisua.SplitterDistance = SubMenu1_CurFil.btnMinimizwEditor.Width;
                        rtb_MainStatus.Visible = false;

                    }
                    break;
                case subMenu1Status.SubMenu1_Output:
                    if (!SubMenu1_Output.ShowStatus)
                    {
                        int temDistance = SplitterEditorVisua.Size.Width - SplitterEditorVisua.Panel2.Width - SplitterEditorVisua.SplitterWidth;
                        SubMenu1_Output.Location = new Point(0, 0);

                        if (!(temDistance < 350))
                        {
                            SplitterEditorVisua.SplitterDistance = temDistance;
                            rtb_MainStatus.Width = temDistance - SubMenu1_Output.btnMinimizwEditor.Width;
                            SplitterEditorVisua.SplitterDistance = 350;
                            rtb_MainStatus.Width = 329;
                            rtb_MainStatus.Visible = true;

                        }
                        else
                        {
                            SplitterEditorVisua.SplitterDistance = 350;
                            rtb_MainStatus.Width = 329;
                            rtb_MainStatus.Visible = true;
                        }
                    }
                    else
                    {
                        SubMenu1_Output.Location = new Point(-(SubMenu1_Output.Width - SubMenu1_Output.btnMinimizwEditor.Width), 0);
                        SplitterEditorVisua.SplitterDistance = SubMenu1_Output.btnMinimizwEditor.Width;
                        rtb_MainStatus.Visible = false;

                    }
                    break;
            }
        }

        /// <summary>
        /// 觸發subMenu1_Setting雙擊事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void subMenu11_DoubleClick(object sender, EventArgs e)
        {
            switch (subMenu)
            {
                case subMenu1Status.SubMenu1_Setting:
                    editorMinimized(subMenu1_Setting.btnMinimizwEditor, e);
                    break;
                case subMenu1Status.SubMenu1_Output:
                    editorMinimized(SubMenu1_Output.btnMinimizwEditor, e);
                    break;
                case subMenu1Status.SubMenu1_CurFil:
                    editorMinimized(SubMenu1_CurFil.btnMinimizwEditor, e);
                    break;
                case subMenu1Status.SubMenu1_Scan:
                    editorMinimized(SubMenu1_Scan.btnMinimizwEditor, e);
                    break;

            }
        }

        private void editorMinimized(object sender, EventArgs e)
        {
            switch (subMenu)
            {
                case subMenu1Status.SubMenu1_Setting:
                    if (subMenu1_Setting.ShowStatus)
                    {
                        subMenu1_Setting.ShowStatus = false;
                        FrmSize();
                    }
                    else
                    {
                        subMenu1_Setting.ShowStatus = true;
                        FrmSize();
                    }
                    break;
                case subMenu1Status.SubMenu1_Scan:
                    if (SubMenu1_Scan.ShowStatus)
                    {
                        SubMenu1_Scan.ShowStatus = false;
                        FrmSize();
                    }
                    else
                    {
                        SubMenu1_Scan.ShowStatus = true;
                        FrmSize();
                    }
                    break;
                case subMenu1Status.SubMenu1_CurFil:
                    if (SubMenu1_CurFil.ShowStatus)
                    {
                        SubMenu1_CurFil.ShowStatus = false;
                        FrmSize();
                    }
                    else
                    {
                        SubMenu1_CurFil.ShowStatus = true;
                        FrmSize();
                    }
                    break;
                case subMenu1Status.SubMenu1_Output:
                    if (SubMenu1_Output.ShowStatus)
                    {
                        SubMenu1_Output.ShowStatus = false;
                        FrmSize();
                    }
                    else
                    {
                        SubMenu1_Output.ShowStatus = true;
                        FrmSize();
                    }
                    break;
            }
        }
        #endregion

        #region ToolBar按鈕事件
        private void PCDFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = "";
            loadfile.loadPcd(ref url);          //取得檔案路徑
            io = new IOpcForm();                 //初始化PCL顯示
            OpenChildform(io);                   //使PCL畫面顯示在主頁面
            io.UpdateForm(url, "pcd");                  //更新顯示的點雲座標
            panelVirsualixtion.Refresh();        //自動更新畫面
        }

        private void OBJFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = "";
            loadfile.loadObj(ref url);           //取得檔案路徑
            io = new IOpcForm();                 //初始化PCL顯示
            OpenChildform(io);                   //使PCL畫面顯示在主頁面
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    io.UpdateForm(url, "obj");
                });
            }
            else { io.UpdateForm(url, "obj"); }
            panelVirsualixtion.Refresh();        //自動更新畫面
        }

        private void TxtFileStripMenuItem4_Click(object sender, EventArgs e)
        {
            string url = "";
            loadfile.loadTxt(ref url);           //取得檔案路徑
            io = new IOpcForm();                 //初始化PCL顯示
            OpenChildform(io);                   //使PCL畫面顯示在主頁面
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    io.UpdateForm(url, "txt");
                });
            }
            else { io.UpdateForm(url, "txt"); }
            panelVirsualixtion.Refresh();        //自動更新畫面
        }
        private void PLYFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = "";
            loadfile.loadPly(ref url);           //取得檔案路徑
            io = new IOpcForm();                 //初始化PCL顯示
            OpenChildform(io);                   //使PCL畫面顯示在主頁面
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    io.UpdateForm(url, "ply");
                });
            }
            else { io.UpdateForm(url, "ply"); }
            panelVirsualixtion.Refresh();        //自動更新畫面
        }

   

        #endregion

        #region 序列埠
        /// <summary>
        /// 加載序列埠視窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void serialClientToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if ((serial == null) || serial.IsDisposed)
            {
                if (com != null || com.IsOpen)
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new MethodInvoker(delegate
                        {
                            if (com.IsOpen)
                            {
                                com.Close(); // 在開啟序列埠視窗前確保串口已經關閉
                            }
                            serial = new Client(com, DB);
                            serial.SerialFormClosing += new EventHandler(Client_SerialFormClosing);
                            serial.Show();
                        }));
                    }
                    else
                    {
                        serial = new Client(com, DB);
                        serial.SerialFormClosing += new EventHandler(Client_SerialFormClosing);
                        serial.Show();
                    }
                }
                else 
                { 
                    rtb_MainStatusAutoScroll("NotConnected！");
                }
            }
            else
            {
                serial.BringToFront();
            }
        }

        /// <summary>
        /// 序列埠視窗關閉前執行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_SerialFormClosing(object sender, EventArgs e)
        {
            try
            {
                this.BringToFront();
                com.DataReceived -= serial.ComPort_DataReceived;
                serial.SerialFormClosing -= Client_SerialFormClosing;
                serial.Close();
                serial = null;  // 將串口視窗物件清空

            }
            catch (Exception ex)
            {
                MessageBox.Show("程式出現異常: " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion

        #region 狀態欄
        private void editorConnect_ComPortStatusChanged(object sender, EventArgs e)
        {
            rtb_MainStatusAutoScroll(this.subMenu1_Setting.editorConnect.ComPortStatus.ToString()); ;
        }

        private void rtb_MainStatusAutoScroll(string str)
        {
            //設定垂直捲軸
            rtb_MainStatus.ScrollBars = RichTextBoxScrollBars.Vertical;
            //改變舊狀態顏色(白)
            rtb_MainStatus.SelectionStart = 0;
            rtb_MainStatus.SelectionLength = rtb_MainStatus.TextLength;
            rtb_MainStatus.SelectionColor = Color.White;
            //改變新狀態顏色(紅)
            rtb_MainStatus.SelectionStart = rtb_MainStatus.TextLength;
            rtb_MainStatus.SelectionLength = 0;
            rtb_MainStatus.SelectionColor = Color.Red;
            rtb_MainStatus.AppendText(str + "\r\n");
            //移動光標至新的文字行
            rtb_MainStatus.SelectionStart = rtb_MainStatus.Text.Length;
            rtb_MainStatus.ScrollToCaret();
        }
        #endregion

        #region SubMode按鈕事件
        /// <summary>
        /// 觸發顯示txt檔內的點雲資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iconBtnScan_Click(object sender, EventArgs e)
        {
            if (subMenu != subMenu1Status.SubMenu1_Scan)
            {
                try
                {
                    switch (subMenu)
                    {
                        case subMenu1Status.SubMenu1_Setting:
                            this.subMenu1_Setting.Visible = false;
                            this.SubMenu1_Scan.Visible = true;
                            break;
                        case subMenu1Status.SubMenu1_CurFil:
                            this.SubMenu1_CurFil.Visible = false;
                            this.SubMenu1_Scan.Visible = true;
                            break;
                        case subMenu1Status.SubMenu1_Output:
                            this.SubMenu1_Output.Visible = false;
                            this.SubMenu1_Scan.Visible = true;
                            break;

                    }
                    subMenu = subMenu1Status.SubMenu1_Scan;
                }
                catch (Exception ex) { MessageBox.Show("子選單轉換失敗: " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

        }

        /// <summary>
        /// 顯示 Output 頁面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iconBtnOutput_Click(object sender, EventArgs e)
        {
            if (subMenu != subMenu1Status.SubMenu1_Output)
            {
                try
                {
                    switch (subMenu)
                    {
                        case subMenu1Status.SubMenu1_Setting:
                            this.subMenu1_Setting.Visible = false;
                            this.SubMenu1_Output.Visible = true;
                            break;
                        case subMenu1Status.SubMenu1_Scan:
                            this.SubMenu1_Scan.Visible = false;
                            this.SubMenu1_Output.Visible = true;
                            break;
                        case subMenu1Status.SubMenu1_CurFil:
                            this.SubMenu1_CurFil.Visible = false;
                            this.SubMenu1_Output.Visible = true;
                            break;

                    }
                    subMenu = subMenu1Status.SubMenu1_Output;
                }
                catch (Exception ex) { MessageBox.Show("子選單轉換失敗: " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                if (point_posi != null)
                {
                    SubMenu1_Output._pc = point_posi;
                }
                else { MessageBox.Show("點雲有誤: 未初始化", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            }
        }

        /// <summary>
        /// 顯示 Setting 頁面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iconBtnSetting_Click(object sender, EventArgs e)
        {
            if (subMenu != subMenu1Status.SubMenu1_Setting)
            {
                try
                {
                    switch (subMenu)
                    {
                        case subMenu1Status.SubMenu1_Scan:
                            this.SubMenu1_Scan.Visible = false;
                            subMenu1_Setting.Visible = true;
                            break;
                        case subMenu1Status.SubMenu1_CurFil:
                            this.SubMenu1_CurFil.Visible = false;
                            subMenu1_Setting.Visible = true;
                            break;
                        case subMenu1Status.SubMenu1_Output:
                            this.SubMenu1_Output.Visible = false;
                            this.subMenu1_Setting.Visible = true;
                            break;

                    }
                    subMenu = subMenu1Status.SubMenu1_Setting;
                }
                catch (Exception ex) { MessageBox.Show("子選單轉換失敗: " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            }
        }

        /// <summary>
        /// 顯示 Curve / Filter 頁面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iconBtnCurFil_Click(object sender, EventArgs e)
        {
            if (subMenu != subMenu1Status.SubMenu1_CurFil)
            {
                try
                {
                    switch (subMenu)
                    {
                        case subMenu1Status.SubMenu1_Setting:
                            subMenu1_Setting.Visible = false;
                            this.SubMenu1_CurFil.Visible = true;
                            break;
                        case subMenu1Status.SubMenu1_Scan:
                            SubMenu1_Scan.Visible = false;
                            this.SubMenu1_CurFil.Visible = true;
                            break;
                        case subMenu1Status.SubMenu1_Output:
                            this.SubMenu1_Output.Visible = false;
                            this.SubMenu1_CurFil.Visible = true;
                            break;

                    }
                    subMenu = subMenu1Status.SubMenu1_CurFil;
                }
                catch (Exception ex) { MessageBox.Show("子選單轉換失敗: " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            }
        }


        #endregion

        #region 分析表
        private void analysisChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AnalysisChart == null || AnalysisChart.IsDisposed)
            {
                if (!isAnalysisChartOpen)
                {
                    isAnalysisChartOpen = true;
                    AnalysisChart = new AnalysisChart();

                    // 更改此處的名稱，即可更新顯示名稱與項目
                    //ChangItemName("acc_linear", "X", "Y", "Z", "A1");
                    ChangItemName("總位移距離", "X", "Y", "瞬時速度", "A1");
                    ChangItemName("原始位移距離", "X", "Y", "Z", "A2");
                    ChangItemName("當下點雲座標", "X", "Y", "Z", "A3");
                    ChangItemName("角速度{B}  ", "X =", "Y =", "Z =", "A4");
                    ChangItemName("加速度{B}", "X =", "Y =", "Z =", "A5");
                    ChangItemName("四元數", "X", "Y", "Z", "A6");
                    AnalysisChart.Show();
                    AnalysisChart.FormClosed += new System.Windows.Forms.FormClosedEventHandler(AnalysisChart_FormClosed);
                }
            }
            else { AnalysisChart.BringToFront(); }
            
        }

        /// <summary>
        /// 分析表關閉後才執行的事件處理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnalysisChart_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                isAnalysisChartOpen = false;
                AnalysisChart.FormClosed -= AnalysisChart_FormClosed;
                AnalysisChart.Dispose();
            }
            catch(Exception ex) { MessageBox.Show("程式出現異常: " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        /// <summary>
        /// 輸出 data 至 groupBox 所屬的資料表內
        /// </summary>
        /// <param name="data"></param>
        /// <param name="groupBox"></param>
        private void PrintOnAnalysisCahrt(Vector3D data, String groupBox)
        {
            if (AnalysisChart != null && !AnalysisChart.IsDisposed)
            {
                if (isScanning)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        switch (groupBox)
                        {
                            case "A1":
                                AnalysisChart.textBox_A1_X.Text = data.X.ToString("F3") + " cm";
                                AnalysisChart.textBox_A1_Y.Text = data.Y.ToString("F3") + " cm";
                                AnalysisChart.textBox_A1_Z.Text = data.Z.ToString("F2") + " cm/s";
                                break;
                            case "A2":
                                AnalysisChart.textBox_A2_X.Text = data.X.ToString("F4");
                                AnalysisChart.textBox_A2_Y.Text = data.Y.ToString("F4");
                                AnalysisChart.textBox_A2_Z.Text = data.Z.ToString("F4");

                                break;
                            case "A3":
                                AnalysisChart.textBox_A3_X.Text = data.X.ToString("F4");
                                AnalysisChart.textBox_A3_Y.Text = data.Y.ToString("F4");
                                AnalysisChart.textBox_A3_Z.Text = data.Z.ToString("F4");

                                break;
                            case "A4":
                                AnalysisChart.textBox_A4_X.Text = data.X.ToString("F4");
                                AnalysisChart.textBox_A4_Y.Text = data.Y.ToString("F4");
                                AnalysisChart.textBox_A4_Z.Text = data.Z.ToString("F4");

                                break;
                            case "A5":
                                AnalysisChart.textBox_A5_X.Text = data.X.ToString("F4");
                                AnalysisChart.textBox_A5_Y.Text = data.Y.ToString("F4");
                                AnalysisChart.textBox_A5_Z.Text = data.Z.ToString("F4");

                                break;
                            case "A6":
                                AnalysisChart.textBox_A6_X.Text = data.X.ToString("F4");
                                AnalysisChart.textBox_A6_Y.Text = data.Y.ToString("F4");
                                AnalysisChart.textBox_A6_Z.Text = data.Z.ToString("F4");
                                AnalysisChart.textBox_A6_W.Visible = false;
                                AnalysisChart.lbl_A6_w.Visible = false;
                                
                                break;
                        }


                    });
                }
            }
            else
            {
                analysisChartToolStripMenuItem.PerformClick();
            }


        }
        private void PrintOnAnalysisCahrt(Quaternion data, String groupBox)
        {
            if (AnalysisChart != null && !AnalysisChart.IsDisposed)
            {
                if (isScanning)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        switch (groupBox)
                        {
                            case "A6":
                                AnalysisChart.textBox_A6_X.Text = data.X.ToString("F3");
                                AnalysisChart.textBox_A6_Y.Text = data.Y.ToString("F3");
                                AnalysisChart.textBox_A6_Z.Text = data.Z.ToString("F3");
                                AnalysisChart.textBox_A6_W.Text = data.W.ToString("F3");

                                break;
                        }


                    });
                }
            }
            else
            {
                analysisChartToolStripMenuItem.PerformClick();
            }


        }

        /// <summary>
        /// 修改 groupBox 內的標示名稱
        /// </summary>
        /// <param name="group"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="groupBox"></param>
        private void ChangItemName(String group, String x, String y, String z, String groupBox)
        {
            if (AnalysisChart != null && !AnalysisChart.IsDisposed)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    switch (groupBox)
                    {
                        case "A1":
                            AnalysisChart.groupBox_A1.Text = group;
                            AnalysisChart.lbl_A1_X.Text = x;
                            AnalysisChart.lbl_A1_Y.Text = y;
                            AnalysisChart.lbl_A1_Z.Text = z;
                            break;
                        case "A2":
                            AnalysisChart.groupBox_A2.Text = group;
                            AnalysisChart.lbl_A2_X.Text = x;
                            AnalysisChart.lbl_A2_Y.Text = y;
                            AnalysisChart.lbl_A2_Z.Text = z;
                            break;
                        case "A3":
                            AnalysisChart.groupBox_A3.Text = group;
                            AnalysisChart.lbl_A3_X.Text = x;
                            AnalysisChart.lbl_A3_Y.Text = y;
                            AnalysisChart.lbl_A3_Z.Text = z;
                            break;
                        case "A4":
                            AnalysisChart.groupBox_A4.Text = group;
                            AnalysisChart.lbl_A4_X.Text = x;
                            AnalysisChart.lbl_A4_Y.Text = y;
                            AnalysisChart.lbl_A4_Z.Text = z;
                            break;
                        case "A5":
                            AnalysisChart.groupBox_A5.Text = group;
                            AnalysisChart.lbl_A5_X.Text = x;
                            AnalysisChart.lbl_A5_Y.Text = y;
                            AnalysisChart.lbl_A5_Z.Text = z;
                            break;
                        case "A6":
                            AnalysisChart.groupBox_A6.Text = group;
                            AnalysisChart.lbl_A6_X.Text = x;
                            AnalysisChart.lbl_A6_Y.Text = y;
                            AnalysisChart.lbl_A6_Z.Text = z;
                            break;
                        default:
                            break;
                    }
                });
            }

        }

        /// <summary>
        /// 依據state(運動偵測狀態)的變化來改變 iconButton1 顏色
        /// </summary>
        /// <param name="state"></param>
        private void MotionStatusOnDotCircle(bool state)
        {
            if(AnalysisChart != null && !AnalysisChart.IsDisposed)
            {
                if (InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        if (state) { AnalysisChart.iconButton1.IconColor = System.Drawing.Color.Green; }
                        else { AnalysisChart.iconButton1.IconColor = System.Drawing.Color.Red; }
                    });
                }
                else
                {
                    if (state) { AnalysisChart.iconButton1.IconColor = System.Drawing.Color.Green; }
                    else { AnalysisChart.iconButton1.IconColor = System.Drawing.Color.Red; }
                }
               
            }
        }

        private void UpdateFPS(double deltime)
        {
            if (AnalysisChart != null && !AnalysisChart.IsDisposed)
            {
                if (isScanning)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        double ScanningFreq = 1 / deltime;
                        AnalysisChart.label_FPS.Text = ScanningFreq.ToString("F2");
                    });
                }
            }
            else
            {
                analysisChartToolStripMenuItem.PerformClick();
            }
        }


        private void UpdateDataNum(int Num)
        {
            if (AnalysisChart != null && !AnalysisChart.IsDisposed)
            {
                if (isScanning)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        AnalysisChart.lbl_NumberoofData.Text = Num.ToString();
                    });
                }
            }
            else
            {
                analysisChartToolStripMenuItem.PerformClick();
            }
        }
        #endregion

        #region 濾波

        /// <summary>
        /// 閾值濾波
        /// </summary>
        /// <param name="data"></param>
        /// <param name="thrvalue"></param>
        /// <returns></returns>
        private Vector3D DataThrFilter(Vector3D data, double Minthrvalue, double MaxthrValue)
        {
            if (!IsNanData(data))
            {
                if (!(data.X == (double)0) || !(data.Y == (double)0) || !(data.Z == (double)0))
                {
                    if (Math.Abs(data.X) < Minthrvalue || Math.Abs(data.X) > MaxthrValue) { data.X = (double)0.0; }
                    if (Math.Abs(data.Y) < Minthrvalue || Math.Abs(data.Y) > MaxthrValue) { data.Y = (double)0.0; }
                    if (Math.Abs(data.Z) < Minthrvalue || Math.Abs(data.Z) > MaxthrValue) { data.Z = (double)0.0; }
                }
                return data;
            }
            else { return new Vector3D(0, 0, 0); }
        }

        /// <summary>
        /// 低通濾波
        /// </summary>
        /// <param name="data">上一刻的數據</param>
        /// <param name="newData">此刻數據</param>
        /// <param name="filter_koef">低通係數</param>
        /// <param name="filter_trigger">閾值</param>
        /// <returns></returns>
        private double LowPassFilter(double data, double newData, double filter_koef, double filter_trigger)
        {
            if (Math.Abs(newData - data) < filter_trigger)
            {
                data = data + filter_koef * (newData - data);
            }
            else
            {
                data = newData;
            }
            return data;
        }
        /// <summary>
        /// 高通濾波器
        /// </summary>
        /// <param name="data"></param>
        /// <param name="newData"></param>
        /// <param name="filter_koef">濾波係數，越大截止頻率越高</param>
        /// <returns></returns>
        private double HighPassFilter(double data, double newData, double filter_koef)
        {
            data = newData - (data + filter_koef * (newData - data));
            return data;
        }

        private Quaternion HighPassFilterToQuaternion(Quaternion data, Quaternion newData, double filter_koef)
        {
            data.W = HighPassFilter(data.W, newData.W, filter_koef);
            data.X = HighPassFilter(data.X, newData.X, filter_koef);
            data.Y = HighPassFilter(data.Y, newData.Y, filter_koef);
            data.Z = HighPassFilter(data.Z, newData.Z, filter_koef);
            return data;
        }

        private Quaternion LowPassFilterToQuaternion(Quaternion data, Quaternion newData, double filter_koef, double filter_trigger)
        {
            data.W = LowPassFilter(data.W, newData.W, filter_koef, filter_trigger);
            data.X = LowPassFilter(data.X, newData.X, filter_koef, filter_trigger);
            data.Y = LowPassFilter(data.Y, newData.Y, filter_koef, filter_trigger);
            data.Z = LowPassFilter(data.Z, newData.Z, filter_koef, filter_trigger);
            return data;
        }
        #endregion

        #region 影像擷取

        private void frameCaptureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(frameCapture == null || frameCapture.IsDisposed)
            {
                if (com != null || com.IsOpen)
                {
                    if (InvokeRequired)
                    {
                        this.Invoke(new MethodInvoker(delegate
                        {
                            ChangeFrameCapMode();
                            frameCapture = new FrameCapture(com);
                            frameCapture.FrameCapFormClosing += new EventHandler(frameCapture_SerialFormClosing);
                            frameCapture.Show();
                        }));
                    }
                    else
                    {
                        ChangeFrameCapMode();
                        frameCapture = new FrameCapture(com);
                        frameCapture.FrameCapFormClosing += new EventHandler(frameCapture_SerialFormClosing);
                        frameCapture.Show();
                    }
                }
            }
            else { frameCapture.BringToFront(); }
            
        }

        private void ChangeFrameCapMode()
        {
            try
            {
                if (com.IsOpen)
                {
                    com.Write("FrameCaptureModeChange");
                    rtb_MainStatusAutoScroll("Enable Frame Capture Mode");
                }
                else { rtb_MainStatusAutoScroll("SerialPort未連線成功！"); }
            }
            catch (Exception ex)
            { MessageBox.Show("命令傳送失敗： " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void frameCapture_SerialFormClosing(object sender, EventArgs e)
        {
            try
            {
                this.BringToFront();
                frameCapture.FrameCapFormClosing -= new EventHandler(frameCapture_SerialFormClosing);
                frameCapture.Dispose();
                frameCapture = null;
                ChangeFrameCapMode();
                rtb_MainStatusAutoScroll("Disable Frame Capture Mode");

            }
            catch (Exception ex)
            {
                // 處理 IOException，例如記錄日誌或顯示錯誤訊息。
                MessageBox.Show("程式出現異常: " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion

        #region 點雲數據輸出

        /// <summary>
        /// 輸出 PCD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubMenu1_Output_PcdSaveClick(object sender, EventArgs e)
        {
            if (SubMenu1_Output.SaveStatus != true)
            {
                try
                {
                    SubMenu1_Output.SaveStatus = true;

                    string outputPath = "..\\OutputFile\\PCD";

                    if (!Directory.Exists(outputPath))
                    {
                        Directory.CreateDirectory(outputPath);
                    }
                    StreamWriter writer = new StreamWriter(outputPath);

                    for (int i = 0; i < point_posi.Size; i++)
                    {
                        writer.WriteLine($"{point_posi.GetX(i)} { point_posi.GetY(i)} {point_posi.GetZ(i)}");
                    }

                    MessageBox.Show("pcd檔儲存成功！", "通知", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //目前問題是，掃描後的點雲數據無法讓 point_posi 的 Height 等於 1 ，這會造成 savePcdFile 函式執行時的錯誤(point_posi.Height * point_posi.Width != point_posi.Size => 錯誤)
                    //point_posi.Height = 1;
                    //PclCSharp.Io.savePcdFile(outputPath, point_posi.PointCloudXYZPointer, 0);


                    SubMenu1_Output.SaveStatus = false;
                }
                catch (Exception ex)
                {
                    SubMenu1_Output.SaveStatus = false;
                    MessageBox.Show("儲存失敗: " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 輸出 TXT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubMenu1_Output_TxtSaveClick(object sender, EventArgs e)
        {
            if (SubMenu1_Output.SaveStatus != true)
            {
                try
                {
                    SubMenu1_Output.SaveStatus = true;  //  避免儲存過程的衝突

                    string outputPath = "..\\..\\..\\OutputFile\\TXT/";     //  儲存路徑

                    //如果沒有此資料夾，創建一個
                    if (!Directory.Exists(outputPath))
                    {
                        Directory.CreateDirectory(outputPath);
                    }

                    // 初始化 writer，創建即將寫入的檔案
                    using (StreamWriter writer = new StreamWriter(outputPath + DateTime.Now.ToString("MM-dd HH_mm_ss") + ".txt"))
                    {
                        // 寫入數據
                        for (int i = 0; i < point_posi.Size; i++)
                        {
                            writer.WriteLine($"{point_posi.GetX(i)} { point_posi.GetY(i)} {point_posi.GetZ(i)}");
                            //writer.WriteLine($"{xCoords[i]}");
                        }

                        MessageBox.Show("txt檔儲存成功！", "通知", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    SubMenu1_Output.SaveStatus = false;
                }
                catch (Exception ex)
                {
                    SubMenu1_Output.SaveStatus = false;
                    MessageBox.Show("儲存失敗: " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion


    }
}

