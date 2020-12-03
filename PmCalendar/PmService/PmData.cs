using System;
using System.Collections.Generic;
using System.Text;

namespace PmService
{
    public class PmData
    {

        /// <summary>
        /// 数据的ID
        /// </summary>
        public long Pid { get; set; }

        /// <summary>
        /// 露点，空气中水气含量达到饱和的气温（â„ƒ）
        /// </summary>
        public int Dewp { get; set; }

        /// <summary>
        /// 累积风速，观测时间点对应的累积风速（m/s）
        /// </summary>
        public double Lws { get; set; }

        /// <summary>
        /// 压强，观测时间点对应的压强（hPa）
        /// </summary>
        public double Pres { get; set; }

        /// <summary>
        /// /温度，观测时间点对应的温度（â„ƒ）
        /// </summary>
        public double Temp { get; set; }

        /// <summary>
        /// 风向，1是东北风，2是西北风，3是东南风，4是静风
        /// </summary>
        public int Cbwd { get; set; }  

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Pdate { get; set; }

        /// <summary>
        /// 小时
        /// </summary>
        public int Hour { get; set; } 

        /// <summary>
        /// Pm2.5数值
        /// </summary>
        public int Pm { get; set; }

        /// <summary>
        /// PM2.5评级
        /// </summary>
        public int level { get; set; }
        
        public override String ToString()
        {
            String str = "pid:"+Pid + " pm:" + Pm + " level:" + level;
            return str;
        }

    }
}
