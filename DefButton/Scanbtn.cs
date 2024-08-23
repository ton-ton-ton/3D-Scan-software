using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;



namespace DefButton
{
    public class Scanbtn
    {
        string remainingData = string.Empty;
        bool scanStatus;
        private object scanStatusLock = new object(); // 建立用於同步的專用物件

        public bool ScanStatus
        {
            get
            {
                lock (scanStatusLock)
                {
                    return scanStatus;
                }
            }
            set
            {
                lock (scanStatusLock)
                {
                    scanStatus = value;
                }
            }
        }

        /// <summary>
        /// 讀取序列埠中的資料流
        /// </summary>
        /// <param name="com"></param>
        /// <param name="dataflow"></param>
        public void StringDataReceived(SerialPort com, float[] dataflow, ref ulong time)
        {
            byte[] buffer = new byte[com.BytesToRead];
            com.Read(buffer, 0, buffer.Length);

            string inputStringData = Encoding.ASCII.GetString(buffer);
            buffer = null;

            inputStringData = remainingData + inputStringData;

            string[] lines = inputStringData.Split('\n');

            int lastIndex = lines.Length - 1;

            // 檢查最後一行是否為不完整的數據，如果是則存儲到 remainingData 中
            if (!string.IsNullOrEmpty(lines[lastIndex]))
            {
                remainingData = lines[lastIndex];
                lastIndex--;
            }
            else
            {
                remainingData = string.Empty;
            }

            for (int i = 0; i <= lastIndex; i++)
            {
                string line = lines[i];

                string[] splitData = line.Split(',');

                if (splitData.Length != dataflow.Length + 1)
                {
                    // 數據長度不符合預期，跳過此行
                    continue;
                }

                // 感測數據
                for (int j = 0; j < dataflow.Length; j++)
                {
                    if (float.TryParse(splitData[j], out float value))
                    {
                        dataflow[j] = value;
                    }
                    else
                    {
                        dataflow[j] = 0.0f; // 轉換失敗，給出默認值
                    }
                }

                // 時間
                if (ulong.TryParse(splitData[dataflow.Length], out ulong _time))
                {
                    time = _time;
                }
                else
                {
                    time = 0; // 轉換失敗，給出默認值
                }

                scanStatus = true;
            }

        }



        //將分離後的數據各別存入對應的List<>
            //accelData.x.Add(float.Parse(splitData[0]));
            //accelData.y.Add(float.Parse(splitData[1]));
            //accelData.z.Add(float.Parse(splitData[2]));

            //gyroData.x.Add(float.Parse(splitData[3]));
            //gyroData.y.Add(float.Parse(splitData[4]));
            //gyroData.z.Add(float.Parse(splitData[5]));

            //quaternionData.w.Add(float.Parse(splitData[6]));
            //quaternionData.x.Add(float.Parse(splitData[7]));
            //quaternionData.y.Add(float.Parse(splitData[8]));
            //quaternionData.z.Add(float.Parse(splitData[9]));

            //displacementData.x.Add(float.Parse(splitData[10]));
            //displacementData.y.Add(float.Parse(splitData[11]));

            //傳值給SerialClient 顯示

            //存入本地SQL
            //後處理











        
    }
}
