using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace UserControlEditor
{
    public partial class SubMenu1_CurFil : UserControl
    {
        PanelControl panelControl = new PanelControl();


        //創建VTK控件renderWindowControl1
        //private Kitware.VTK.RenderWindowControl renderWindowControl1;
        //private Kitware.VTK.RenderWindowControl renderWindowControl2;

        //點雲路徑
        string url;

        //PointCloudXYZ cloud = new PointCloudXYZ();      //濾波前
        //PointCloudXYZ cloud_res = new PointCloudXYZ();  //濾波後

        //vtkCamera camera = vtkCamera.New();

        //private vtkAxesActor axes;     //  左下的三軸可動座標
        //private vtkOrientationMarkerWidget orienWidget;
        //private vtkCubeAxesActor vtkCubeAxes;   //  三圍網格座標系
        //private vtkRenderer Renderer;
        //private vtkActor Actor;
        //private vtkRenderWindow renderOnWindow;
        double[] minmax = new double[6] { 0, 1, 0, 1, 0, 1 };

        Panel panel2;

        //創建委任事件
        public event EventHandler MinimezedClick;

        //屬性
        public bool ShowStatus { get; set; }

        public SubMenu1_CurFil()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 頁面隱藏與顯示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected  void BtnMinimizwEditor_Click(object sender, EventArgs e)
        {
            //如委任方法有被繼承，則進入條件式內執行
            if (MinimezedClick != null)
            {
                MinimezedClick?.Invoke(this,e);  //判斷當前是否在UI執行緒上，如果不是就用invoke，避免跨執行緒異常
            }
              
        }

        /// <summary>
        /// 子頁面1 隱藏與顯示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iconButton1_Click(object sender, EventArgs e)
        {
            panelControl.ShowSubMenuPanel(panelSubconnect);
        }

        /// <summary>
        /// 子頁面2 隱藏與顯示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iconButton2_Click(object sender, EventArgs e)
        {
            panelControl.ShowSubMenuPanel(panelSubCalibration);
        }



        //private vtkRenderer RenderPointCloud(PointCloudXYZ in_pc, vtkRenderWindow RenWindow)
        //{
        //    vtkRenderer out_render = vtkRenderer.New();

        //    //宣告用於顯示的點雲(points)
        //    vtkPoints points = vtkPoints.New();
        //    //把點雲指針中的點雲資料依序存入points
        //    for (int i = 0; i < in_pc.Size; i++)
        //    {
        //        points.InsertNextPoint(in_pc.GetX(i), in_pc.GetY(i), in_pc.GetZ(i));

        //    }
        //    //==創建上述點雲的屬性數據，此處為顏色==
        //    vtkUnsignedCharArray colors_rgb = setColorBaseAxis('z', in_pc);

        //    //====創建並設置儲存點雲的對象====
        //    vtkPolyData polydata = vtkPolyData.New();
        //    polydata.SetPoints(points); //將points數據傳進polydata
        //    polydata.GetPointData().SetScalars(colors_rgb); //將點雲屬性的顏色資料傳進polydata中

        //    //===創建並設置點雲擴展過濾器===
        //    vtkVertexGlyphFilter glyphFilter = vtkVertexGlyphFilter.New();
        //    glyphFilter.SetInputConnection(polydata.GetProducerPort());

        //    // ===新增可印製點雲對象====
        //    vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
        //    mapper.SetColorModeToDefault(); //使用vtkPolyData中指定的點和面的顏色
        //    mapper.SetScalarVisibility(2);  //顏色由polydata和vtkActor共同決定
        //    mapper.SetInputConnection(glyphFilter.GetOutputPort()); // 連接管道

        //    //==設定颜色刻度表==
        //    vtkScalarBarActor scalarBar = vtkScalarBarActor.New();
        //    scalarBar.SetLookupTable(mapper.GetLookupTable());
        //    scalarBar.SetTitle("Point Cloud");
        //    scalarBar.SetHeight(0.7);
        //    scalarBar.SetWidth(0.1);
        //    scalarBar.SetNumberOfLabels(10);
        //    scalarBar.GetLabelTextProperty().SetFontSize(4);

        //    //====創建並設置可視化對象====
        //    vtkActor actor = vtkActor.New();
        //    actor.SetMapper(mapper); // vtkMapper 將資料轉換為可以在 VTK 中顯示的形式
        //    actor.GetProperty().SetPointSize(3);


        //    //====座標軸=====
        //    orienWidget.SetOrientationMarker(axes);
        //    orienWidget.SetViewport(0.0, 0.0, 0.25, 0.25);
        //    orienWidget.SetInteractor(RenWindow.GetInteractor());
        //    orienWidget.SetEnabled(1);
        //    orienWidget.InteractiveOff();

        //    //====設定三維網格座標=====
        //    in_pc.GetMinMaxXYZ(minmax);
        //    vtkCubeAxes.SetBounds(minmax[0], minmax[1], minmax[2], minmax[3], minmax[4], minmax[5]);
        //    vtkCubeAxes.XAxisLabelVisibilityOn();
        //    vtkCubeAxes.YAxisLabelVisibilityOn();
        //    vtkCubeAxes.ZAxisLabelVisibilityOn();
        //    //vtkCubeAxes.SetXTitle("X");
        //    //vtkCubeAxes.SetYTitle("Y");
        //    //vtkCubeAxes.SetZTitle("Z");
        //    vtkCubeAxes.SetXTitle("");
        //    vtkCubeAxes.SetYTitle("");
        //    vtkCubeAxes.SetZTitle("");
        //    //vtkCubeAxes.SetXUnits("mm");
        //    //vtkCubeAxes.SetYUnits("mm");
        //    //vtkCubeAxes.SetZUnits("mm");
        //    vtkCubeAxes.SetTickLocationToOutside();
        //    vtkCubeAxes.SetFlyMode(1);
        //    vtkCubeAxes.SetLabelScaling(true, 0, 0, 0);
        //    vtkCubeAxes.SetFlyMode(0);


        //    //====創建並設置渲染器====
        //    out_render.AddActor(actor);
        //    out_render.AddActor(scalarBar);     //  添加颜色刻度表
        //    out_render.AddActor(vtkCubeAxes);   //  添加三維網格座標
        //    out_render.SetViewport(1.0, 1.0, 0.0, 0.0); // 設定Viewport窗口
        //    out_render.SetActiveCamera(camera);
        //    // 打开渐变色背景开关
        //    out_render.GradientBackgroundOn();
        //    out_render.SetBackground(0.27, 0.27, 0.3);
        //    out_render.SetBackground2(0, 0, 0);
        //    // out_render.SetBackground(0.07, 0.07, 0.1);    //單色背景
        //    return out_render;
        //}

        /// <summary>
        /// 統計濾波
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iconBtn_StatisticalOutlierRemovalFilter_Click(object sender, EventArgs e)
        {
            //  staFilter(濾波前點雲座標, 近邻数, )
           // PclCSharp.Filter.staFilter(cloud.PointCloudXYZPointer, 40, 1f, cloud_res.PointCloudXYZPointer);
            //vtkRenderWindow renWin2 = renderWindowControl2.RenderWindow;
            //vtkRenderer renderer2 = RenderPointCloud(cloud_res, renWin2);
            // 将“角色Actor”添加到“渲染器Renderer”并渲染
            //renWin2.AddRenderer(renderer2);
            //刷新panel，这样就不需要点击一下屏幕才会显示点云
            panel2.Refresh();
        }
        /// <summary>
        /// 半徑濾波
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iconbtn_UniformDownSampleFilter_Click(object sender, EventArgs e)
        {
            //PclCSharp.Filter.radiusFilter(cloud.PointCloudXYZPointer, 0.08, 40, cloud_res.PointCloudXYZPointer);
            //vtkRenderWindow renWin2 = renderWindowControl2.RenderWindow;

            //vtkRenderer renderer2 = RenderPointCloud(cloud_res, renWin2);
            //// 将“角色Actor”添加到“渲染器Renderer”并渲染
            //renWin2.AddRenderer(renderer2);
            //刷新panel，这样就不需要点击一下屏幕才会显示点云
            panel2.Refresh();
        }
        /// <summary>
        /// 體素降取樣濾波
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iconbtn_VoxelDownSampleFilter_Click(object sender, EventArgs e)
        {
            //PclCSharp.Filter.voxelDownSample(cloud.PointCloudXYZPointer, 0.08, cloud_res.PointCloudXYZPointer);
            //vtkRenderWindow renWin2 = renderWindowControl2.RenderWindow;

            //vtkRenderer renderer2 = RenderPointCloud(cloud_res, renWin2);
            //// 将“角色Actor”添加到“渲染器Renderer”并渲染
            //renWin2.AddRenderer(renderer2);
            //刷新panel，这样就不需要点击一下屏幕才会显示点云
            panel2.Refresh();
        }
        /// <summary>
        /// 近似體素降取樣濾波
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iconBtn_approximateVoxelDownSampleFilter_Click(object sender, EventArgs e)
        {
            //PclCSharp.Filter.approximateVoxelDownSample(cloud.PointCloudXYZPointer, 0.08, cloud_res.PointCloudXYZPointer);
            //vtkRenderWindow renWin2 = renderWindowControl2.RenderWindow;

            //vtkRenderer renderer2 = RenderPointCloud(cloud_res, renWin2);
            //// 将“角色Actor”添加到“渲染器Renderer”并渲染
            //renWin2.AddRenderer(renderer2);
            //刷新panel，这样就不需要点击一下屏幕才会显示点云
            panel2.Refresh();
        }
        /// <summary>
        /// 直通濾波
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iconBtn_PassThroughFilter_Click(object sender, EventArgs e)
        {
            //Filter.passThroughFilter(cloud.PointCloudXYZPointer, "x", 0.4f, 1.0f, 0, cloud_res.PointCloudXYZPointer);
            //vtkRenderWindow renWin2 = renderWindowControl2.RenderWindow;

            //vtkRenderer renderer2 = RenderPointCloud(cloud_res, renWin2);
            //// 将“角色Actor”添加到“渲染器Renderer”并渲染
            //renWin2.AddRenderer(renderer2);
            //刷新panel，这样就不需要点击一下屏幕才会显示点云
            panel2.Refresh();
        }

        private void iconBtn_SigmaFilter_Click(object sender, EventArgs e)
        {

        }

        //vtkUnsignedCharArray setColorBaseAxis(char axis, PointCloudXYZ in_pc)
        //{
        //    vtkUnsignedCharArray colors_rgb = vtkUnsignedCharArray.New();
        //    //点云的极值,第一第二个元素分别是x的最小最大值，yz依次类推
        //    double[] minmax = new double[6];
        //    in_pc.GetMinMaxXYZ(minmax);
        //    double z = minmax[5] - minmax[4];//z轴的差值
        //    double y = minmax[3] - minmax[2];//y轴的差值
        //    double x = minmax[1] - minmax[0];//x轴的差值
        //    double z_median = z / 2;
        //    double y_median = y / 2;
        //    double x_median = x / 2;
        //    colors_rgb.SetNumberOfComponents(3);//设置颜色的组分，因为是rgb，所以组分为3
        //    double r = 0, g = 0, b = 0;
        //    if (axis == 'x')
        //    {
        //        for (int i = 0; i < in_pc.Size; i++)
        //        {
        //            //中间值为界，x值大于中间值的b组分为0，r组分逐渐变大
        //            if ((in_pc.GetX(i) - minmax[0]) > x_median)
        //            {
        //                //x值要先归一化再乘以255，不然数值将会超出255

        //                r = (255 * ((in_pc.GetX(i) - minmax[0] - x_median) / x_median)); ;
        //                g = (255 * (1 - ((in_pc.GetX(i) - minmax[0] - x_median) / x_median)));
        //                b = 0;
        //                colors_rgb.InsertNextTuple3(r, g, b);
        //            }
        //            //中间值为界，x值小于中间值的r组分为0，b组分逐渐变大
        //            else
        //            {
        //                //x值要先归一化再乘以255，不然数值将会超出255
        //                r = 0;
        //                g = (255 * ((in_pc.GetX(i) - minmax[0]) / x_median));
        //                b = (255 * (1 - ((in_pc.GetX(i) - minmax[0]) / x_median))); ;
        //                colors_rgb.InsertNextTuple3(r, g, b);
        //            }
        //        }
        //    }
        //    else if (axis == 'y')
        //    {
        //        for (int i = 0; i < in_pc.Size; i++)
        //        {
        //            //中间值为界，y值大于中间值的b组分为0，r组分逐渐变大
        //            if ((in_pc.GetY(i) - minmax[2]) > y_median)
        //            {
        //                //y值要先归一化再乘以255，不然数值将会超出255
        //                r = (255 * ((in_pc.GetY(i) - minmax[2] - y_median) / y_median)); ;
        //                g = (255 * (1 - ((in_pc.GetY(i) - minmax[2] - y_median) / y_median)));
        //                b = 0;
        //                colors_rgb.InsertNextTuple3(r, g, b);
        //            }
        //            //中间值为界，y值小于中间值的r组分为0，b组分逐渐变大
        //            else
        //            {
        //                r = 0;
        //                g = (255 * ((in_pc.GetY(i) - minmax[2]) / y_median));
        //                b = (255 * (1 - ((in_pc.GetY(i) - minmax[2]) / y_median))); ;
        //                colors_rgb.InsertNextTuple3(r, g, b);
        //            }
        //        }
        //    }
        //    else if (axis == 'z')
        //    {

        //        for (int i = 0; i < in_pc.Size; i++)
        //        {
        //            //中间值为界，z值大于中间值的b组分为0，r组分逐渐变大
        //            if ((in_pc.GetZ(i) - minmax[4]) > z_median)
        //            {
        //                //z值要先归一化再乘以255，不然数值将会超出255
        //                r = (255 * ((in_pc.GetZ(i) - minmax[4] - z_median) / z_median)); ;
        //                g = (255 * (1 - ((in_pc.GetZ(i) - minmax[4] - z_median) / z_median)));
        //                b = 0;
        //                colors_rgb.InsertNextTuple3(r, g, b);
        //            }
        //            //中间值为界，z值小于中间值的r组分为0，b组分逐渐变大
        //            else
        //            {
        //                r = 0;
        //                g = (255 * ((in_pc.GetZ(i) - minmax[4]) / z_median));
        //                b = (255 * (1 - ((in_pc.GetZ(i) - minmax[4]) / z_median)));
        //                colors_rgb.InsertNextTuple3(r, g, b);
        //            }
        //        }
        //    }

        //    return colors_rgb;
        //}
    }
}
