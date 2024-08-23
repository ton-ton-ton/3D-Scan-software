
using PointCloudSharp;


namespace DefButton
{
    public class OutputFile
    {
        /// <summary>
        /// 輸出為PCD檔
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void savePcd(PointCloudXYZ cloud)
        {
            //第三个参数为0,代表以assii格式存储，参数为1，代表以二进制形式存储。二进制形式读取速度更快
            PclCSharp.Io.savePcdFile("..\\..\\..\\..\\ioDemo.pcd", cloud.PointCloudXYZPointer, 0);
        }

        /// <summary>
        /// 輸出為PlLY檔
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void savePly(PointCloudXYZ cloud)
        {
            //第三个参数为0,代表以assii格式存储，参数为1，代表以二进制形式存储。二进制形式读取速度更快
            PclCSharp.Io.savePlyFile("..\\..\\..\\..\\ioDemo.ply", cloud.PointCloudXYZPointer, 0);
        }

        
        
    }
}
