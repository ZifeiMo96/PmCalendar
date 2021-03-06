﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmService
{
    class PmDataBase
    {
        public List<PmData> DataList { get; set; }

        public List<PmData> TestList { get; set; }

        public double[] LevelFrequency { get; set; }

        public double[,] HourFrequency { get; set; }

        public double[,] DateFrequency { get; set; }

        public double[,] CbwdFrequency { get; set; }

        public double[,] TempFrequency { get; set; }

        public double[,] PresFrequency { get; set; }

        public double[,] LwsFrequency { get; set; }

        public double[,] DewpFrequency { get; set; }

        /// <summary>
        /// 得出某一个属性某一等级的概率
        /// </summary>
        /// <param name="box">对应属性的矩阵</param>
        /// <param name="level">对应属性的评级</param>
        /// <returns></returns>
        public double GetItemFrequency(double[,] box,int level)
        {
            double frequency = 0;
            for(int i = 0; i < 5; i++)
            {
                frequency += box[level,i] * LevelFrequency[i];
            }
            return frequency;
        }

        public PmDataBase()
        {
            DataList = new List<PmData>();
            TestList = new List<PmData>();
            LevelFrequency = new double[6];
            HourFrequency = new double[4, 6];
            DateFrequency = new double[366, 6];
            CbwdFrequency = new double[4, 6];
            TempFrequency = new double[5, 6];
            PresFrequency = new double[5, 6];
            LwsFrequency = new double[4, 6];
            DewpFrequency = new double[6, 6];
        }

        public void AddData(PmData data)
        {
            DataList.Add(data);
        }

        public void AddTestData(PmData data)
        {
            TestList.Add(data);
        }
    }
}
