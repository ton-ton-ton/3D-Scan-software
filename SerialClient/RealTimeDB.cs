using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace SerialClient
{
    public class RealTimeDB
    {
        //Hide the dictionary, force people to use functions
        private Dictionary<String, List<Tuple<Single, Single>>> Data;       //Data 是一个字符串为键，列表为值的字典，用于存储单精度浮点型坐标点
        private int LockKey = -1;                   //LockKey 是一个整数变量，初始值为 -1，后面会用来实现线程同步。
        private Stopwatch sw = new Stopwatch();     //sw 是一个 Stopwatch 对象，用于计时。
                                                    //private Dictionary<String, List<Tuple<Single, Single>>> Buffer;    //Buffer 是一个与 Data 类似的字典，用于在数据采集过程中存储缓存数据。

        public SqlConnection SQLDB;        //SQLDB 是一个 SqlConnection 对象，用于与 SQL Server 数据库建立连接。

        public RealTimeDB()
        {
            Data = new Dictionary<String, List<Tuple<Single, Single>>>();     //建立物件，分配內存空間給予Data
            //Buffer = new Dictionary<String, List<Tuple<Single, Single>>>();
            sw.Start();

            //取得資料庫路徑與檔名
            string mdfName = "SerialDB.mdf"; //  資料庫檔案的名稱。
            string dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location); //  獲取目前執行的程式所在的目錄路徑。 
            string DBFilename = dir + "\\" + mdfName;      //  結合目錄路徑和資料庫檔案名稱得到的完整資料庫檔案路徑。
            string DBName = System.IO.Path.GetFileNameWithoutExtension(DBFilename);     //  獲取不包含副檔名的資料庫檔案名稱。

            //創建資料庫
            if (!System.IO.File.Exists(mdfName))
            {
                CreateDatabase(DBName, DBFilename);
            }
            else
            {
                System.IO.File.Delete(DBFilename);  //  刪除現有的資料庫
                CreateDatabase(DBName, DBFilename);
            }

            //開啟資料庫
            SQLDB = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\SerialDB.mdf;Integrated Security=True;MultipleActiveResultSets=true");
            SQLDB.Open();

            //刪除現有資料庫內的表格
            DataTable schema = SQLDB.GetSchema("Tables");
            foreach (DataRow row in schema.Rows)
            {
                SqlCommand SQLCMD = new SqlCommand("DELETE " + row, SQLDB);
                SQLCMD.ExecuteNonQuery();
            };

            //創建新的資料庫內的表格
            string createTable = "CREATE TABLE DBDATA(signal char(50), time FLOAT, value FLOAT );";
            SqlCommand SQLCMD2 = new SqlCommand(createTable, SQLDB);
            SQLCMD2.ExecuteNonQuery();

        }

        #region Lock
        public int requestLock()
        {
            if (LockKey == -1)
            {
                Random rnd = new Random();
                LockKey = rnd.Next(0, 1000);
                return LockKey;
            }
            else
            {
                return -1;
            }
        }

        //Return Lock
        public void returnLock(int Key)
        {
            if (Key == LockKey)
            {
                LockKey = -1;
            }
        }

        #endregion

        /// <summary>
        /// 清理緩存Data、清除SQL Database、清除Buffer、重啟計時
        /// </summary>
        public void Reset()
        {
            sw.Stop();

            Data.Clear();   //刪除內存
            //Buffer.Clear();

            //刪除表格內的資料
            string cmdStr = "TRUNCATE TABLE DBDATA; ";  //  無法被回滾（rollback），一旦執行，資料就會永久刪除
            //string cmdStr = "TRUNCATE TABLE DBDATA; ";  //  可以被回滾，因此可以在需要時進行還原
            SqlCommand SQLCMD = new SqlCommand(cmdStr, SQLDB);
            SQLCMD.ExecuteNonQuery();

            ////重啟Timer
            sw.Restart();
        }

        public void CloseLocalDB()
        {
            try
            {
                // 確認 SqlConnection 不為 null 且是打開狀態
                if (SQLDB != null && SQLDB.State == System.Data.ConnectionState.Open)
                {
                    // 關閉資料庫連接
                    SQLDB.Close();
                    Data.Clear();   //刪除內存
                }
            }
            catch (Exception ex)
            {
                // 處理可能的例外狀況
                Console.WriteLine("關閉本地DB時發生錯誤：" + ex.Message);
            }
            //finally
            //{
            //    // 釋放 SqlConnection 物件資源
            //    SQLDB.Dispose();
            //    SQLDB = null;
            //}
        }
    

        /// <summary>
    /// 向SQL請求查詢 -> 判斷資料列有無資料可讀 -> 讀取並return )
    /// </summary>
    /// <returns>可讀取的資料列</returns>
        public List<String> Signals()
        {
            // return Data.Keys.ToList();
            List<String> retSignals = new List<String>();

            string cmdStr = "SELECT DISTINCT signal FROM DBDATA;";
            SqlDataReader reader;
            SqlCommand SQLCMD = new SqlCommand(cmdStr, SQLDB);
            reader = SQLCMD.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    String signal = reader.GetString(0);
                    retSignals.Add(signal);
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            reader.Close();

            return retSignals;
        }

        /// <summary>
        /// 創建Database資料集
        /// </summary>
        /// <param name="dbName">資料集的名稱</param>
        /// <param name="dbFileName">資料集的路徑</param>
        /// <returns></returns>
        public static bool CreateDatabase(string dbName, string dbFileName)
        {
            try
            {
                string connectionString = String.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True");
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();

                    String ldfName = System.IO.Path.GetDirectoryName(dbFileName) + "\\" + System.IO.Path.GetFileNameWithoutExtension(dbFileName) + "_log.ldf";
                    if (System.IO.File.Exists(ldfName))
                    {
                        System.IO.File.Delete(ldfName);
                    }
                    DetachDatabase(dbName);
                    cmd.CommandText = String.Format("CREATE DATABASE {0} ON (NAME = '{0}', FILENAME = '{1}')", dbName, dbFileName);
                    cmd.ExecuteNonQuery();
                }

                if (File.Exists(dbFileName)) return true;
                else return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 分離資料庫，使資料庫脫離SQL的操作範圍，確保資料庫執行重建時的獨立性
        /// </summary>
        /// <param name="dbName">資料集名稱</param>
        /// <returns></returns>
        public static bool DetachDatabase(string dbName)
        {
            try
            {
                string connectionString = String.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True");
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = String.Format("exec sp_detach_db '{0}'", dbName);
                    cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        #region 存CSV
        public bool storeCSV(string outFileName, Single startTime, Single endTime, Single DataRate)
        {

            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(outFileName))
                {

                    // return Data.Keys.ToList();
                    Dictionary<String, List<Tuple<Single, Single>>> csvData = new Dictionary<String, List<Tuple<Single, Single>>>();
                    List<String> SignalNames = Signals();

                    //Write CSV Header
                    file.WriteLine("time," + String.Join(",", SignalNames));

                    //Get all the Data
                    Single minTime = Single.MaxValue;
                    foreach (String sig in SignalNames)
                    {
                        List<Tuple<Single, Single>> Data = Read(sig, startTime, endTime);
                        if (Data.Count > 0)
                        {
                            minTime = Math.Min(minTime, Data[0].Item1);
                            csvData.Add(sig, Data);
                        }
                    }

                    //Write Data Line by Line
                    for (Single time = 0; time <= (endTime - startTime); time = time + DataRate)
                    {
                        String LineData = time.ToString() + ",";
                        bool allFound = true;
                        foreach (String sig in SignalNames)
                        {
                            List<Tuple<Single, Single>> Data = csvData[sig];

                            //Find the data point which lines up with the time, if needed, interpolate
                            bool found = false;
                            int lasti = 0;
                            for (int i = 0; i < Data.Count - 1; i++)
                            {
                                lasti = i;
                                if (Data[i + 1].Item1 >= time + minTime)
                                {
                                    Single interpValue = Data[i].Item2 + ((time + minTime) - Data[i].Item1) * (Data[i + 1].Item2 - Data[i].Item2) / (Data[i + 1].Item1 - Data[i].Item1);
                                    found = true;
                                    LineData = LineData + interpValue.ToString() + ",";
                                    break;
                                }
                                if (i == Data.Count - 1)
                                {
                                    //End of Data set.
                                    found = true;
                                    break;
                                }
                            }

                            if (!found)
                            {
                                allFound = false;
                            }

                        }

                        if (allFound)
                        {
                            file.WriteLine(LineData);
                        }
                    }
                    file.Close();
                }
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion

        #region 讀取
        public List<Tuple<Single, Single>> Read(String ParamName)
        {

            string cmdStr = "SELECT time,value FROM DBDATA WHERE signal='" + ParamName + "'";
            SqlDataReader reader;
            SqlCommand SQLCMD = new SqlCommand(cmdStr, SQLDB);
            reader = SQLCMD.ExecuteReader();

            List<Tuple<Single, Single>> retData = new List<Tuple<Single, Single>>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Single time = Convert.ToSingle(reader.GetDouble(0));
                    Single value = Convert.ToSingle(reader.GetDouble(1));
                    retData.Add(new Tuple<Single, Single>(time, value));
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            reader.Close();

            return retData;
        }

        //Read Data and Specify a time range
        public List<Tuple<Single, Single>> Read(String ParamName, Single StartTime, Single EndTime)
        {

            string cmdStr = "SELECT time,value FROM DBDATA WHERE signal='" + ParamName + "' AND time>=" + StartTime.ToString() + " AND time<=" + EndTime.ToString();
            SqlDataReader reader;
            SqlCommand SQLCMD = new SqlCommand(cmdStr, SQLDB);
            reader = SQLCMD.ExecuteReader();

            List<Tuple<Single, Single>> retData = new List<Tuple<Single, Single>>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Single time = Convert.ToSingle(reader.GetDouble(0));
                    Single value = Convert.ToSingle(reader.GetDouble(1));
                    retData.Add(new Tuple<Single, Single>(time, value));
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            reader.Close();

            return retData;
        }
        #endregion

        #region 寫入
        public void Write(String ParamName, Single TimeSec, Single ParamData)
        {
            //Write Data to DB
            string cmdStr = "INSERT INTO DBDATA (signal, time, value) VALUES ('" + ParamName + "'," + TimeSec.ToString() + "," + ParamData.ToString() + ")";
            SqlCommand SQLCMD = new SqlCommand(cmdStr, SQLDB);
            SQLCMD.ExecuteNonQuery();
        }

        public void Write(String ParamName, List<Tuple<Single, Single>> ParamData)
        {
            if (ParamData != null && ParamData.Count > 0)
            {
                //List<Tuple<Single, Single>> dataCopy = new List<Tuple<Single, Single>>(ParamData); // 創建副本
                Tuple<Single, Single>[] dataCopy = new Tuple<Single, Single>[ParamData.Count];
                if (dataCopy.Length != ParamData.Count) return;
                ParamData.CopyTo(dataCopy, 0);

                foreach (Tuple<Single, Single> Data in dataCopy)
                {
                    if (Data != null)
                        Write(ParamName, Data.Item1, Data.Item2);
                }
                    
            }
        }
        #endregion

        #region 時間
        public Double GetTimeMs()
        {
            return sw.ElapsedMilliseconds;
        }
        public Double GetTimeSec()
        {
            return sw.ElapsedMilliseconds / 1000.0;
        }
        #endregion








    }
}
