﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PmService
{
    public class PmData
    {
        public PmData()
        {
            Pid = initNumber;
            Dewp = initNumber;
            Lws = initNumber;
            Pres = initNumber;
            Temp = initNumber;
            Cbwd = initNumber;
            Pdate = new DateTime();
            Hour = initNumber;
            Pm = initNumber;
            level = initNumber;
        }
        /// <summary>
        /// 数据的ID
        /// </summary>
        public long Pid { get; set; }

        /// <summary>
        /// 露点，空气中水气含量达到饱和的气温（â„ƒ）
        /// </summary>
        public int Dewp { get; set; }
        public int DewpLevel
        {
            get
            {
                if (Dewp < -12.4)
                {
                    return 0;
                }
                else if (Dewp < -3.8)
                {
                    return 1;
                }
                else if (Dewp < 8.3)
                {
                    return 2;
                }
                else if (Dewp < 17.9)
                {
                    return 3;
                }
                else
                {
                    return 4;
                }
            }
        }

        /// <summary>
        /// 累积风速，观测时间点对应的累积风速（m/s）
        /// </summary>
        public double Lws { get; set; }

        public int LwsLevel
        {
            get
            {
                if (Lws < 5)   // 静风
                {
                    return 0;
                }
                else if (Lws < 50)   // 小风
                {
                    return 1;
                }
                else if (Lws < 100)   //中风
                {
                    return 2;
                }
                else   //大风
                {
                    return 3;
                }
            }
        }

        /// <summary>
        /// 压强，观测时间点对应的压强（hPa）
        /// </summary>
        public double Pres { get; set; }

        public int PresLevel
        {
            get
            {
                if (Pres < 1005.712)
                {
                    return 0;
                }
                else if (Pres < 1012.864)
                {
                    return 1;
                }
                else if (Pres < 1019.991)
                {
                    return 2;
                }
                else if (Pres < 1026.143)
                {
                    return 3;
                }
                else
                {
                    return 4;
                }
            }
        }

        /// <summary>
        /// /温度，观测时间点对应的温度（â„ƒ）
        /// </summary>
        public double Temp { get; set; }

        public int TempLevel
        {
            get
            {
                if (Temp < -0.0727)
                {
                    return 0;
                }
                else if (Temp < 8.3721)
                {
                    return 1;
                }
                else if (Temp < 18.455)
                {
                    return 2;
                }
                else if (Temp < 24.7545)
                {
                    return 3;
                }
                else
                {
                    return 4;
                }
            }
        }

        /// <summary>
        /// 风向，1是东北风，2是西北风，3是东南风，4是静风
        /// </summary>
        public int Cbwd { get; set; }

        public int CbwdLevel
        {
            get
            {
                return Cbwd - 1;
            }
        }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Pdate { get; set; }

        public int DateLevel
        {
            get
            {
                return Pdate.DayOfYear-1;
            }
        }

        /// <summary>
        /// 小时
        /// </summary>
        public int Hour { get; set; }

        public int HourLevel
        {
            get
            {
                return Hour/6;
            }
        }

        /// <summary>
        /// Pm2.5数值
        /// </summary>
        public int Pm { get; set; }

        /// <summary>
        /// PM2.5评级
        /// </summary>
        public int level { get; set; }

        /// <summary>
        /// 返回数据的信息的字符串
        /// </summary>
        /// <returns>数据信息的字符串</returns>
        public override String ToString()
        {
            String str = "pid:"+Pid + " pm:" + Pm + " level:" + level + " Hour" + HourLevel;
            return str;
        }

        private static int initNumber = -114514;

    }
}
