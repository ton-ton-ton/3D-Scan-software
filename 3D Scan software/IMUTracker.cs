using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using MathNet.Numerics.LinearAlgebra;

namespace _3D_Scan_software
{

    struct Pose
    {
        public ulong timestamp;
        public Vector3D position;
        public Quaternion orientation;
        public Vector3D linear_vel;
        public Vector3D ang_vel;
    }

    public struct newPoint
    {
        public Vector3D pos;
        public Matrix3D orien;
        public Vector3D ang_vel;
        public Vector3D linear_vel;
    }

    public struct ImuFrame
    {
        public ulong timestamp;
        public Vector3D ang_vel;
        public Vector3D acc_vel;
    }

    class IMUTracker
    {
        newPoint point_;    //每一幀的點雲數據
        bool firstFrame_;
        List<ImuFrame> IMU_Buffer = new List<ImuFrame>();   //每一幀的原始數據

        Vector3D gravity_;
        double deltaT_;
        ulong prev_time_;
        List<Pose> vp_;
        public List<Tuple<ulong, Vector3D>> Pos;


        public IMUTracker()
        {
            //初始化暫存點雲
            point_.pos = new Vector3D(0, 0, 0);
            point_.orien = Matrix3D.Identity;
            point_.linear_vel = new Vector3D(0, 0, 0); 
            point_.ang_vel = new Vector3D(0, 0, 0); 
            firstFrame_ = true;

            //初始化儲存點雲數據集
            Pose p0;
            p0.timestamp = IMU_Buffer[0].timestamp;
            p0.position = new Vector3D(0, 0, 0);
            p0.orientation = new Quaternion(1, 0, 0, 0);
            p0.linear_vel = new Vector3D(0, 0, 0);
            p0.ang_vel = new Vector3D(0, 0, 0);
            vp_.Add(p0);
        }

        public void track(ulong time, Vector3D acc, Vector3D gyro)
        {
            //foreach(var msg in  IMU_Buffer)
            //{
                if (firstFrame_)
                {
                    prev_time_ = time;
                    deltaT_ = 0;
                    setGravity(acc);
                    firstFrame_ = false;
                }
                else
                {
                    deltaT_ = (time - prev_time_) * 1e-9;
                    prev_time_ = time;
                    //calOrien(gyro);
                    calPos(acc);
                    //updatePos(point_);
                }
            //}
        }

        private void setGravity(Vector3D msg)
        { 
            gravity_ = msg;
        }

        void calPos(Vector3D msg)
        {
            Vector3D acc_i = msg;
            Vector3D acc_w = point_.orien.Transform(acc_i);
            point_.linear_vel += deltaT_ * (acc_w - gravity_);
            point_.pos += deltaT_ * point_.linear_vel;
        }

        void calOrien(Vector3D msg)
        {
            point_.ang_vel = msg;
            Matrix3D B = new Matrix3D(); ; // 角速度 * 时间 = 角度（表示为反对称矩阵）
            B.M11 = 0; B.M12 = -msg.Z * deltaT_; B.M13 = msg.Y * deltaT_;
            B.M21 = msg.Z * deltaT_; B.M22 = 0; B.M23 = -msg.X * deltaT_;
            B.M31 = -msg.Y * deltaT_; B.M32 = msg.X * deltaT_; B.M33 = 0;

            double sigma = Math.Sqrt(Math.Pow(msg.X, 2) + Math.Pow(msg.Y, 2) + Math.Pow(msg.Z, 2)) * deltaT_;
            Matrix3D B2 = Matrix3D.Multiply(B, B);
            double sin_sig = (Math.Sin(sigma) / sigma); //垂直分量
            double cos_sig = ((1 - Math.Cos(sigma)) / Math.Pow(sigma, 2));  //水平分量
            Matrix3D sin = new Matrix3D(sin_sig, 0, 0, 0, 0, sin_sig, 0, 0, 0, 0, sin_sig, 0, 0, 0, 0, 1);
            Matrix3D cos = new Matrix3D(cos_sig, 0, 0, 0, 0, cos_sig, 0, 0, 0, 0, cos_sig, 0, 0, 0, 0, 1);

            Matrix3D result = new Matrix3D();
            result.Append(Matrix3D.Identity);
            result.Append(Matrix3D.Multiply(B, sin));
            result.Append(Matrix3D.Multiply(B2, cos));
            point_.orien *= result;

        }

        void updatePos(newPoint point)
        {
            Pose p = new Pose();

            p.timestamp = prev_time_;

            p.position = point.pos;
            
            //p.linear_vel = point.linear_vel;
            //p.ang_vel = point.ang_vel;

            //p.orientation.X = (point.orien.M32 - point.orien.M23) / 4;
            //p.orientation.Y = (point.orien.M13 - point.orien.M31) / 4;
            //p.orientation.Z = (point.orien.M21 - point.orien.M12) / 4;
            //p.orientation.W = Math.Sqrt(1 + point.orien.M11 + point.orien.M22 + point.orien.M33) / 2;

            vp_.Add(p);
            Pos.Add(new Tuple<ulong, Vector3D>(prev_time_, point.pos));
        }

    }
}
