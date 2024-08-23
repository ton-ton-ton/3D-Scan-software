using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PclCSharp;
using PointCloudSharp;
using Kitware.VTK;
using System.IO;
using System.Threading;
using System.Windows.Media.Media3D;


namespace IOPointCloud
{

    public partial class IOpcForm : Form
    {
        //創建VTK控件對象 "renderWindowControl1"，未被初始化與分配內存
        public RenderWindowControl renderWindowControl1;

        vtkCamera camera = vtkCamera.New();

        //輸入的點雲對象 "cloud"，同時分配內存空間用以儲存資料
        PointCloudXYZ cloud = new PointCloudXYZ();

        private vtkAxesActor axes;     //  左下的三軸可動座標
        private vtkOrientationMarkerWidget orienWidget;
        private vtkCubeAxesActor vtkCubeAxes;   //  三圍網格座標系
        private vtkRenderer Renderer;
        vtkScalarBarActor scalarBar = null;
        private vtkActor Actor;
        private vtkRenderWindow renderOnWindow;
        double[] minmax = new double[6] { 0, 1, 0, 1, 0, 1 };
        bool updateColorScale = true;

        public bool renderProcessingLock { get; set; }
        public bool isScalarBarOpen { get; set; }   //顏色調顯示狀態

        private bool firstInitializeRender { get; set; } = false;
        public float ballSize { get; set; } = 3;

        public IOpcForm()
        {
            //初始化畫面
            InitializeComponent();
            //將VTK控件(renderWindowControl)添加到Form內,且必須以panel作為裝載VTK的容器
            renderWindowControl1 = new RenderWindowControl();//控件(renderWindowControl1)初始化與分配內存
            renderWindowControl1.Dock = DockStyle.Fill;      //控件(renderWindowControl1)位置屬性為填滿
            panel_PC.Controls.Add(renderWindowControl1);     //在容器(panel_PC)內加入控件(renderWindowControl1)

            axes = vtkAxesActor.New();
            orienWidget = vtkOrientationMarkerWidget.New();
            vtkCubeAxes = vtkCubeAxesActor.New();

            //用於判斷在win上顯示點雲的renderWindowControl1物件成功初始化並添加至panel_PC上
            //if (panel_PC.Controls.Contains(renderWindowControl1))
            //{
            //    MessageBox.Show("renderWindowControl1 已經成功添加到 panel_PC 中");
            //}
        }

        #region 畫面物件定義
        /// <summary>
        /// (文件加載)點雲顏色
        /// </summary>
        /// <param name="axis">沿著axis變化顏色</param>
        /// <param name="in_pc"></param>
        /// <returns></returns>
        vtkUnsignedCharArray setColorBaseAxis(char axis, PointCloudXYZ in_pc)
        {
            vtkUnsignedCharArray colors_rgb = vtkUnsignedCharArray.New();
            //点云的极值,第一第二个元素分别是x的最小最大值，yz依次类推
            double[] minmax = new double[6];
            in_pc.GetMinMaxXYZ(minmax);
            double z = minmax[5] - minmax[4];//z轴的差值
            double y = minmax[3] - minmax[2];//y轴的差值
            double x = minmax[1] - minmax[0];//x轴的差值
            double z_median = z / 2;
            double y_median = y / 2;
            double x_median = x / 2;
            colors_rgb.SetNumberOfComponents(3);//设置颜色的组分，因为是rgb，所以组分为3
            double r = 0, g = 0, b = 0;
            if (axis == 'x')
            {
                for (int i = 0; i < in_pc.Size; i++)
                {
                    //中间值为界，x值大于中间值的b组分为0，r组分逐渐变大
                    if ((in_pc.GetX(i) - minmax[0]) > x_median)
                    {
                        //x值要先归一化再乘以255，不然数值将会超出255

                        r = (255 * ((in_pc.GetX(i) - minmax[0] - x_median) / x_median)); ;
                        g = (255 * (1 - ((in_pc.GetX(i) - minmax[0] - x_median) / x_median)));
                        b = 0;
                        colors_rgb.InsertNextTuple3(r, g, b);
                    }
                    //中间值为界，x值小于中间值的r组分为0，b组分逐渐变大
                    else
                    {
                        //x值要先归一化再乘以255，不然数值将会超出255
                        r = 0;
                        g = (255 * ((in_pc.GetX(i) - minmax[0]) / x_median));
                        b = (255 * (1 - ((in_pc.GetX(i) - minmax[0]) / x_median))); ;
                        colors_rgb.InsertNextTuple3(r, g, b);
                    }
                }
            }
            else if (axis == 'y')
            {
                for (int i = 0; i < in_pc.Size; i++)
                {
                    //中间值为界，y值大于中间值的b组分为0，r组分逐渐变大
                    if ((in_pc.GetY(i) - minmax[2]) > y_median)
                    {
                        //y值要先归一化再乘以255，不然数值将会超出255
                        r = (255 * ((in_pc.GetY(i) - minmax[2] - y_median) / y_median)); ;
                        g = (255 * (1 - ((in_pc.GetY(i) - minmax[2] - y_median) / y_median)));
                        b = 0;
                        colors_rgb.InsertNextTuple3(r, g, b);
                    }
                    //中间值为界，y值小于中间值的r组分为0，b组分逐渐变大
                    else
                    {
                        r = 0;
                        g = (255 * ((in_pc.GetY(i) - minmax[2]) / y_median));
                        b = (255 * (1 - ((in_pc.GetY(i) - minmax[2]) / y_median))); ;
                        colors_rgb.InsertNextTuple3(r, g, b);
                    }
                }
            }
            else if (axis == 'z')
            {

                for (int i = 0; i < in_pc.Size; i++)
                {
                    //中间值为界，z值大于中间值的b组分为0，r组分逐渐变大
                    if ((in_pc.GetZ(i) - minmax[4]) > z_median)
                    {
                        //z值要先归一化再乘以255，不然数值将会超出255
                        r = (255 * ((in_pc.GetZ(i) - minmax[4] - z_median) / z_median)); ;
                        g = (255 * (1 - ((in_pc.GetZ(i) - minmax[4] - z_median) / z_median)));
                        b = 0;
                        colors_rgb.InsertNextTuple3(r, g, b);
                    }
                    //中间值为界，z值小于中间值的r组分为0，b组分逐渐变大
                    else
                    {
                        r = 0;
                        g = (255 * ((in_pc.GetZ(i) - minmax[4]) / z_median));
                        b = (255 * (1 - ((in_pc.GetZ(i) - minmax[4]) / z_median)));
                        colors_rgb.InsertNextTuple3(r, g, b);
                    }
                }
            }

            return colors_rgb;
        }
        #endregion

        #region 渲染畫面
        /// <summary>
        /// (文件加載)點雲渲染，點雲彩色，有色階圖
        /// </summary>
        /// <param name="in_pc"></param>
        /// <returns></returns>
        private vtkRenderer RenderPointCloud(PointCloudXYZ in_pc)
        {
            // 創建 vtkRenderer
            vtkRenderer renderer = vtkRenderer.New();
            renderer.GradientBackgroundOn();
            renderer.SetBackground(0.27, 0.27, 0.3);
            renderer.SetBackground2(0, 0, 0);

            // 創建 vtkActor 並設置點雲資料
            vtkActor actor = vtkActor.New();
            actor.GetProperty().SetPointSize(ballSize);

            // 將點雲數據插入 vtkPoints
            vtkPoints points = vtkPoints.New();
            for (int i = 0; i < in_pc.Size; i++)
            {
                points.InsertNextPoint(in_pc.GetX(i), in_pc.GetY(i), in_pc.GetZ(i));
            }

            // 依 Z 軸方向改變顏色
            vtkUnsignedCharArray colors_rgb = setColorBaseAxis('z', in_pc);

            // 創建並設置儲存點雲的對象
            vtkPolyData polyData = vtkPolyData.New();
            polyData.SetPoints(points);
            polyData.GetPointData().SetScalars(colors_rgb);

            // 創建點雲擴展過濾器
            vtkVertexGlyphFilter glyphFilter = vtkVertexGlyphFilter.New();
            glyphFilter.SetInputConnection(polyData.GetProducerPort()); // 連接管道

            // 創建並設置可視化對象
            vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
            mapper.SetColorModeToDefault(); // 使用 vtkPolyData 中指定的點和面的顏色
            mapper.SetScalarVisibility(1);  // 顏色由 polydata 和 vtkActor 共同決定
            mapper.SetInputConnection(glyphFilter.GetOutputPort()); // 連接管道

            actor.SetMapper(mapper); // vtkMapper 將資料轉換為可以在 VTK 中顯示的形式

            // 設定三維網格座標
            //in_pc.GetMinMaxXYZ(minmax);
            //vtkCubeAxesActor vtkCubeAxes = vtkCubeAxesActor.New();
            //vtkCubeAxes.SetBounds(minmax[0], minmax[1], minmax[2], minmax[3], minmax[4], minmax[5]);
            //vtkCubeAxes.XAxisLabelVisibilityOn();
            //vtkCubeAxes.YAxisLabelVisibilityOn();
            //vtkCubeAxes.ZAxisLabelVisibilityOn();
            //vtkCubeAxes.SetXTitle("");
            //vtkCubeAxes.SetYTitle("");
            //vtkCubeAxes.SetZTitle("");
            //vtkCubeAxes.SetCamera(renderer.GetActiveCamera());

            // 添加演員到渲染器
            renderer.AddActor(actor);
            //renderer.AddActor(vtkCubeAxes);

            // 設置顏色刻度條
            vtkScalarBarActor scalarBar = vtkScalarBarActor.New();
            scalarBar.SetLookupTable(mapper.GetLookupTable());
            scalarBar.SetTitle("Point Cloud");
            scalarBar.SetHeight(0.7);
            scalarBar.SetWidth(0.1);
            scalarBar.SetNumberOfLabels(10);
            scalarBar.GetLabelTextProperty().SetFontSize(4);
            renderer.AddActor(scalarBar);

            return renderer;
        }

        #endregion

        #region 更新畫面
        /// <summary>
        /// 載入文件並更新畫面
        /// </summary>
        /// <param name="url">PCD檔案路徑</param>
        public void UpdateForm(string url, string fileFormat)
        {
            renderProcessingLock = true;

            // 清空 cloud 中的點雲，如果不清空的話，打開新文件時會點數疊加
            cloud.Clear();

            // 載入 PCD 文件，並將點雲資料儲存至 cloud 對象的 PointCloudXYZPointer 指針中
            Io.loadPcdFile(url, cloud.PointCloudXYZPointer);
            switch (fileFormat.ToLower())
            {
                case "pcd":
                    Io.loadPcdFile(url, cloud.PointCloudXYZPointer);
                    break;
                case "txt":
                    Io.loadTxtFile(url, cloud.PointCloudXYZPointer);
                    break;
                case "obj":
                    Io.loadObjFile(url, cloud.PointCloudXYZPointer);
                    break;
                case "ply":
                    Io.loadPlyFile(url, cloud.PointCloudXYZPointer);
                    break;
                default:
                    throw new ArgumentException("不支援 *." + fileFormat + " 的檔案格式");
            }

            // 初始化 renderOnWindow
            renderOnWindow = renderWindowControl1.RenderWindow;
            vtkOutputWindow.GlobalWarningDisplayOff();
            renderOnWindow.RemoveAllObservers();  // 移除所有關於錯誤的訊息

            // 渲染點雲並更新畫面
            vtkRenderer renderer = RenderPointCloud(cloud);

            // 移除舊的渲染器
            renderWindowControl1.RenderWindow.GetRenderers().RemoveAllItems();

            // 添加渲染器到渲染視窗
            renderWindowControl1.RenderWindow.AddRenderer(renderer);

            // 設置並啟動 Orientation Marker Widget
            orienWidget.SetOrientationMarker(axes);
            orienWidget.SetViewport(0.0, 0.0, 0.28, 0.28);
            orienWidget.SetInteractor(renderOnWindow.GetInteractor());
            orienWidget.SetEnabled(1);
            orienWidget.InteractiveOff();

            // 設定並渲染畫面
            renderer.SetActiveCamera(camera);
            renderer.ResetCamera();
            renderWindowControl1.RenderWindow.Render();

            // 刷新 panel，這樣就不需點擊視窗刷新顯示資料了
            if (this.InvokeRequired) { Invoke((MethodInvoker)delegate { panel_PC.Refresh(); }); }
            else { panel_PC.Refresh(); }

            renderProcessingLock = false;
        }

        /// <summary>
        /// 載入掃描結果並更新畫面
        /// </summary>
        /// <param name="in_pc">點雲數據</param>
        public void UpdateForm(PointCloudXYZ in_pc)
        {
            renderProcessingLock = true;

            // 初始化 renderOnWindow
            renderOnWindow = renderWindowControl1.RenderWindow;
            vtkOutputWindow.GlobalWarningDisplayOff();
            renderOnWindow.RemoveAllObservers();  // 移除所有關於錯誤的訊息

            //加載點雲至VTK並在window顯示畫面上
            vtkRenderer renderer = RenderPointCloud(in_pc);   //點雲資料轉為VTK庫中的資料格式，接著渲染資料並附值給renderer，使其為定義過的渲染器

            // 移除舊的渲染器
            renderWindowControl1.RenderWindow.GetRenderers().RemoveAllItems();

            // 添加渲染器到渲染視窗
            renderWindowControl1.RenderWindow.AddRenderer(renderer);

            // 設置並啟動 Orientation Marker Widget
            orienWidget.SetOrientationMarker(axes);
            orienWidget.SetViewport(0.0, 0.0, 0.28, 0.28);
            orienWidget.SetInteractor(renderOnWindow.GetInteractor());
            orienWidget.SetEnabled(1);
            orienWidget.InteractiveOff();

            // 設定並渲染畫面
            renderer.SetActiveCamera(camera);
            renderer.ResetCamera();
            renderWindowControl1.RenderWindow.Render();

            // 刷新 panel，這樣就不需點擊視窗刷新顯示資料了
            if (this.InvokeRequired) { Invoke((MethodInvoker)delegate { panel_PC.Refresh(); }); }
            else { panel_PC.Refresh(); }

            renderProcessingLock = false;
        }

        /// <summary>
        /// 動態顯示txt檔內的點雲資料
        /// </summary>
        /// <param name="txt">檔名</param>
        /// <param name="cancellationToken">讀取是否取消顯示</param>
        public void SimplePointsReader(string txt, CancellationToken cancellationToken)
        {
            string raletivePath = "C:/Users/kenny/Desktop/testdata/";
            string filePath = raletivePath + txt;

            int readInterval = 33; // 每33毫秒讀取一次
            int readCount = 50; // 每次讀取10筆資料

            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream && !cancellationToken.IsCancellationRequested)//全部讀完或收到取消請求時停止處理
                {
                    for (int i = 0; i < readCount; i++)
                    {
                        string line = sr.ReadLine();
                        if (line == null)
                        {
                            break;
                        }
                        // 分割這行資料
                        string[] data = line.Split(' ');

                        // 如果資料不是3筆，忽略這行資料
                        if (data.Length != 3)
                        {
                            continue;
                        }

                        // 將資料存儲到cloud中，進行後續處理
                        cloud.Push(double.Parse(data[1]), double.Parse(data[2]), double.Parse(data[0]));

                    }

                    vtkPoints points = vtkPoints.New();

                    //把點雲指針中的點雲資料依序存入points
                    for (int i = 0; i < cloud.Size; i++)
                    {
                        points.InsertNextPoint(cloud.GetX(i), cloud.GetY(i), cloud.GetZ(i));

                    }

                    //創建儲存點雲的對象
                    vtkPolyData polyData = vtkPolyData.New();
                    polyData.SetPoints(points);

                    //創建過濾器，使點雲擴大進而幫助顯示
                    vtkVertexGlyphFilter glyphFilter = vtkVertexGlyphFilter.New();
                    glyphFilter.SetInputConnection(polyData.GetProducerPort());

                    // Visualize
                    if (InvokeRequired)
                    {
                        vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
                        mapper.SetInputConnection(glyphFilter.GetOutputPort());
                        vtkActor actor = vtkActor.New();
                        actor.SetMapper(mapper);
                        actor.GetProperty().SetPointSize(3);
                        vtkRenderWindow RenderWindow = renderWindowControl1.RenderWindow;
                        vtkOutputWindow.GlobalWarningDisplayOff();
                        if (RenderWindow != null)
                        {
                            vtkRenderer renderer = RenderWindow.GetRenderers().GetFirstRenderer();
                            renderer.SetBackground(0.2, 0.3, 0.4);
                            renderer.SetViewport(0.5, 0, 1.0, 1.0);
                            renderer.SetActiveCamera(camera);
                            renderer.AddActor(actor);
                            renderer.ResetCamera();
                            Invoke((MethodInvoker)delegate { panel_PC.Refresh(); });

                        }
                    }
                    else return;

                    // 检查取消请求
                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }

                    Thread.Sleep(readInterval);
                }


            }
        }

        /// <summary>
        /// 實時顯示點雲數據
        /// </summary>
        /// <param name="in_pc"></param>
        public void ScanePointsReader(PointCloudXYZ in_pc, Quaternion orienQuar)
        {
            if (!firstInitializeRender)
            {
                //  renderOnWindow初始化
                renderOnWindow = renderWindowControl1.RenderWindow;
                vtkOutputWindow.GlobalWarningDisplayOff();
                renderOnWindow.RemoveAllObservers();    //  移除所有關於錯誤的訊息

                //  畫面背景設定
                Renderer = renderOnWindow.GetRenderers().GetFirstRenderer();
                //Renderer.SetBackground(0.2, 0.3, 0.4);
                Renderer.GradientBackgroundOn();
                Renderer.SetBackground(0.27, 0.27, 0.3);
                Renderer.SetBackground2(0, 0, 0);

                //  點雲初始化
                Actor = vtkActor.New();
                Actor.GetProperty().SetPointSize(ballSize);

                //設定坐標軸
                orienWidget.SetOrientationMarker(axes);
                orienWidget.SetViewport(0.0, 0.0, 0.25, 0.25);
                orienWidget.SetInteractor(renderOnWindow.GetInteractor());
                orienWidget.SetEnabled(1);
                orienWidget.InteractiveOff();

                // 設定三維網格座標
                vtkCubeAxes.SetCamera(Renderer.GetActiveCamera());
                //  網格設定
                vtkCubeAxes.XAxisLabelVisibilityOn();
                vtkCubeAxes.YAxisLabelVisibilityOn();
                vtkCubeAxes.ZAxisLabelVisibilityOn();
                vtkCubeAxes.SetXTitle("");
                vtkCubeAxes.SetYTitle("");
                vtkCubeAxes.SetZTitle("");
                //vtkCubeAxes.SetXTitle("X");
                //vtkCubeAxes.SetYTitle("Y");
                //vtkCubeAxes.SetZTitle("Z");
                //vtkCubeAxes.SetXUnits("mm");
                //vtkCubeAxes.SetYUnits("mm");
                //vtkCubeAxes.SetZUnits("mm");
                //  軸設定
                vtkCubeAxes.SetTickLocationToOutside();
                vtkCubeAxes.SetFlyMode(1);
                vtkCubeAxes.SetLabelScaling(true, 0, 0, 0);
                vtkCubeAxes.SetFlyMode(0);

                //  設定顏色刻度條
                scalarBar = vtkScalarBarActor.New();
                scalarBar.SetTitle("Point Cloud");
                scalarBar.SetHeight(0.7);
                scalarBar.SetWidth(0.1);
                scalarBar.SetNumberOfLabels(10);
                scalarBar.GetLabelTextProperty().SetFontSize(2);



                // 添加渲染器和演員到渲染視窗
                Renderer.SetActiveCamera(camera);
                Renderer.ResetCamera();
                renderOnWindow.AddRenderer(Renderer);
                renderOnWindow.Render();

                firstInitializeRender = true;
            }
            else
            {
                try
                {
                    //釋放內存空間
                    Renderer.RemoveAllViewProps(); //  釋放渲染器內的所有Actors
                    //renderOnWindow.RemoveRenderer(Renderer);

                    renderProcessingLock = true;
                    vtkPoints points = vtkPoints.New();


                    //把點雲指針中的點雲資料依序存入points
                    for (int i = 0; i < in_pc.Size; i++)
                    {
                        points.InsertNextPoint(in_pc.GetX(i), in_pc.GetY(i), in_pc.GetZ(i));
                    }

                    //依 Z 軸方向改變顏色
                    vtkUnsignedCharArray colors_rgb = setColorBaseAxis('z', in_pc);

                    //設定坐標軸界線
                    in_pc.GetMinMaxXYZ(minmax);
                    vtkCubeAxes.SetBounds(minmax[0], minmax[1], minmax[2], minmax[3], minmax[4], minmax[5]);

                    //創建儲存點雲的對象
                    vtkPolyData polyData = vtkPolyData.New();
                    polyData.SetPoints(points);
                    polyData.GetPointData().SetScalars(colors_rgb);

                    //創建過濾器
                    vtkVertexGlyphFilter glyphFilter = vtkVertexGlyphFilter.New();
                    glyphFilter.SetInputConnection(polyData.GetProducerPort()); // 連接管道

                    Actor.SetMapper(null); // 移除舊的映射器

                    //創建可將點雲印製到渲染器上的對象
                    vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
                    mapper.SetColorModeToDefault(); //使用vtkPolyData中指定的點和面的顏色
                    mapper.SetScalarVisibility(2);  //顏色由polydata和vtkActor共同決定
                    mapper.SetInputConnection(glyphFilter.GetOutputPort()); // 連接管道

                    // 設定三維網格座標的視角
                    vtkCubeAxes.SetCamera(Renderer.GetActiveCamera());

                    //  開啟顏色刻度條
                    if (isScalarBarOpen)
                    {
                        if (updateColorScale)
                        {
                            scalarBar.SetLookupTable(mapper.GetLookupTable());
                            updateColorScale = false;
                        }
                        else
                        {
                            //創建渲染器
                            Renderer.AddActor(scalarBar);   //添加颜色刻度表物件
                        }
                    }

                    //創建可視化對象
                    Actor.SetMapper(mapper);    // vtkMapper 將資料轉換為可以在 VTK 中顯示的形式

                    // 加入物件並更新渲染視窗(renderOnWindow)
                    Renderer.AddActor(vtkCubeAxes);
                    Renderer.AddActor(Actor);
                    Renderer.ResetCamera();
                    renderOnWindow.AddRenderer(Renderer);
                    renderOnWindow.Render();    //  將渲染窗口顯示在Windows上

                    // Visualize
                    if (this.InvokeRequired) { Invoke((MethodInvoker)delegate { panel_PC.Refresh(); }); }
                    else { panel_PC.Refresh(); }

                    // 釋放內存空間
                    points.Dispose();
                    colors_rgb.Dispose();
                    polyData.Dispose();
                    glyphFilter.Dispose();
                    mapper.Dispose();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"掃描畫面的例外情況：+ {e}");
                }

            }
        }
        #endregion

        /// <summary>
        /// 刷新點雲數據，重置掃描
        /// </summary>
        public void ResetPC()
        {
            this.cloud.Clear();
            renderProcessingLock = false;
            isScalarBarOpen = false;
            updateColorScale = true;
        }



    }

}


