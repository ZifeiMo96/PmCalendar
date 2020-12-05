using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmService
{
    public class Service
    {
        /// <summary>
        /// 存储训练数据
        /// </summary>
        private PmDataBase dataBase;
        /// <summary>
        /// 初始化函数
        /// </summary>
        public Service()
        {
            dataBase = new PmDataBase();
        }



        public Service(String path)
        {
            dataBase = new PmDataBase();
            LoadPmData(path);
            CalculateLevelFrequency();
            CalculateItemFrequency();
        }
        /// <summary>
        /// 读取训练数据
        /// </summary>
        /// <param name="path">训练数据csv文件的路径</param>
        /// <returns>读取是否成功</returns>
        public bool LoadPmData(String path)
        {
            using (var fs = File.OpenRead(path))
            {
                using (var reader = new StreamReader(fs))
                {
                    var line = reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        line = reader.ReadLine();
                        var values = line.Split(',');
                        PmData data = new PmData();
                        data.Pid = long.Parse(values[0]);
                        data.Dewp = int.Parse(values[1]);
                        data.Lws = double.Parse(values[2]);
                        data.Pres = double.Parse(values[3]);
                        data.Temp = double.Parse(values[4]);
                        data.Cbwd = int.Parse(values[5]);
                        var date = values[6].Split('/');
                        data.Pdate = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]));
                        data.Hour = int.Parse(values[7]);
                        data.Pm = int.Parse(values[8]);
                        data.level = int.Parse(values[9]);
                        dataBase.AddData(data);
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 打印所有数据
        /// </summary>
        public void PrintData()
        {
            foreach (PmData i in dataBase.DataList)
            {
                Console.WriteLine(i.ToString());
            }
        }

        public void PrintLevelFrequency()
        {
            foreach (double i in dataBase.LevelFrequency)
            {
                Console.WriteLine(i);
            }
        }
        public bool LoadTestPmData(String path)
        {
            using (var fs = File.OpenRead(path))
            {
                using (var reader = new StreamReader(fs))
                {
                    var line = reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        line = reader.ReadLine();
                        var values = line.Split(',');
                        PmData data = new PmData();
                        data.Pid = long.Parse(values[0]);
                        data.Dewp = int.Parse(values[1]);
                        data.Lws = double.Parse(values[2]);
                        data.Pres = double.Parse(values[3]);
                        data.Temp = double.Parse(values[4]);
                        data.Cbwd = int.Parse(values[5]);
                        var date = values[6].Split('/');
                        data.Pdate = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]));
                        data.Hour = int.Parse(values[7]);
                        data.Pm = int.Parse(values[8]);
                        data.level = int.Parse(values[9]);
                        dataBase.AddTestData(data);
                    }
                }
            }
            return true;
        }

        public void TestDataLevel()
        {
            double[,] box = new double[4,5];
            int count = dataBase.DataList.Count;
            foreach (PmData i in dataBase.DataList)
            {
                box[0,i.DewpLevel]+= 1;
                box[1, i.LwsLevel] += 1;
                box[2, i.PresLevel] += 1;
                box[3, i.TempLevel] += 1;
            }
            for (int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    box[i, j] /= count;
                    Console.Write(box[i, j] + "    ");
                }
                Console.WriteLine();
            }

        }

        public void GetDataFeatures()
        {
            double dewp = 0;
            double lws = 0;
            double pres = 0;
            double temp = 0;
            double cbwd = 0;
            int count = dataBase.DataList.Count;
            foreach (PmData i in dataBase.DataList)
            {
                dewp += i.Dewp;
                lws += i.Lws;
                pres += i.Pres;
                temp += i.Temp;
                cbwd += i.Cbwd;
            }
            dewp /= count;
            lws /= count;
            pres /= count;
            temp /= count;
            cbwd /= count;
            Console.WriteLine("dewp平均值："+dewp);
            Console.WriteLine("lws平均值：" + lws);
            Console.WriteLine("pres平均值：" + pres);
            Console.WriteLine("temp平均值：" + temp);
            Console.WriteLine("cbwd平均值：" + cbwd);
            double dewp2 = 0;
            double lws2 = 0;
            double pres2 = 0;
            double temp2 = 0;
            double cbwd2 = 0;
            foreach (PmData i in dataBase.DataList)
            {
                dewp2 += (i.Dewp-dewp)* (i.Dewp - dewp)/ count;
                lws2 += (i.Lws-lws)* (i.Lws - lws)/count;
                pres2 += (i.Pres-pres)* (i.Pres - pres)/count;
                temp2 += (i.Temp-temp)* (i.Temp - temp)/count;
                cbwd2 += (i.Cbwd-cbwd)* (i.Cbwd - cbwd)/count;
            }
            Console.WriteLine("dewp方差：" + dewp2);
            Console.WriteLine("lws方差：" + lws2);
            Console.WriteLine("pres方差：" + pres2);
            Console.WriteLine("temp方差：" + temp2);
            Console.WriteLine("cbwd方差：" + cbwd2);
        }

        public void CalculateLevelFrequency()
        {
            foreach (PmData i in dataBase.DataList)
            {
                dataBase.LevelFrequency[i.level] += 1;
            }
            for(int i = 0; i < 6; i++)
            {
                dataBase.LevelFrequency[i] /= dataBase.DataList.Count;
            }
        }

        public void CalculateItemFrequency()
        {
            foreach(PmData i in dataBase.DataList)
            {
                dataBase.HourFrequency[i.HourLevel,i.level] += 1;
                dataBase.DateFrequency[i.DateLevel,i.level] += 1;
                dataBase.CbwdFrequency[i.CbwdLevel,i.level] += 1;
                dataBase.TempFrequency[i.TempLevel,i.level] += 1;
                dataBase.PresFrequency[i.TempLevel,i.level] += 1;
                dataBase.LwsFrequency[i.LwsLevel,i.level] += 1;
                dataBase.DewpFrequency[i.DewpLevel,i.level] += 1; 
            }
            CalculateFrequency(dataBase.HourFrequency, 4);
            CalculateFrequency(dataBase.DateFrequency, 366);
            CalculateFrequency(dataBase.CbwdFrequency, 4);
            CalculateFrequency(dataBase.TempFrequency, 5);
            CalculateFrequency(dataBase.PresFrequency, 5);
            CalculateFrequency(dataBase.LwsFrequency, 4);
            CalculateFrequency(dataBase.DewpFrequency, 5);
        }

        public void TestItemFrequency()
        {
            PrintItemFrequency(dataBase.HourFrequency, 4);
            PrintItemFrequency(dataBase.DateFrequency, 366);
            PrintItemFrequency(dataBase.CbwdFrequency, 4);
            PrintItemFrequency(dataBase.TempFrequency, 5);
            PrintItemFrequency(dataBase.PresFrequency, 5);
            PrintItemFrequency(dataBase.LwsFrequency, 4);
            PrintItemFrequency(dataBase.DewpFrequency, 5);
        }

        public void TestItemLevel()
        {
            Console.WriteLine(dataBase.GetItemFrequency(dataBase.HourFrequency, 3));
        }

        private void PrintItemFrequency(double[,] box, int a)
        {
            for (int i = 0; i < a; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Console.Write(box[i, j] + "  ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("=====================================================");
        }

        private void CalculateFrequency(double[,]box,int a)
        {
            int count = dataBase.DataList.Count;
            for (int i = 0; i < a; i++)
            {
                for(int j = 0; j < 6; j++)
                {
                    box[i, j] /= (count * dataBase.LevelFrequency[j]);
                }
            }
        }

        public int GetPmLevel(PmData data)
        {
            double[] levelFrequency = new double[6];
            for(int i = 0; i < 6; i++)
            {
                levelFrequency[i] = 1;
                if (data.Dewp > -10000)
                {
                    levelFrequency[i]*= dataBase.DewpFrequency[data.DewpLevel, i] / dataBase.GetItemFrequency(dataBase.DewpFrequency, data.DewpLevel);
                }
                if (data.Lws > -10000)
                {
                    levelFrequency[i] *= dataBase.LwsFrequency[data.LwsLevel, i] / dataBase.GetItemFrequency(dataBase.LwsFrequency, data.LwsLevel);
                }
                if (data.Pres > -10000)
                {
                    levelFrequency[i] *= dataBase.PresFrequency[data.PresLevel, i] / dataBase.GetItemFrequency(dataBase.PresFrequency, data.PresLevel);
                }
                if (data.Temp > -10000)
                {
                    levelFrequency[i] *= dataBase.TempFrequency[data.TempLevel, i] / dataBase.GetItemFrequency(dataBase.TempFrequency, data.TempLevel);
                }
                if (data.Cbwd > -10000)
                {
                    levelFrequency[i] *= dataBase.CbwdFrequency[data.CbwdLevel, i] / dataBase.GetItemFrequency(dataBase.CbwdFrequency, data.CbwdLevel);
                }
                if (data.Hour > -10000)
                {
                    levelFrequency[i] *= dataBase.HourFrequency[data.HourLevel, i] / dataBase.GetItemFrequency(dataBase.HourFrequency, data.HourLevel);
                }
                levelFrequency[i] *= dataBase.LevelFrequency[i];
            }
            return FrequencyMax(levelFrequency);
        }

        public int GetPmLevelByDate(PmData pmdata)
        {
            double[] levelFrequency = new double[6];
            double n = dataBase.GetItemFrequency(dataBase.DateFrequency, pmdata.DateLevel); ;
            for(int i = 0; i < 6; i++)
            {
                levelFrequency[i] = dataBase.DateFrequency[pmdata.DateLevel, i] * dataBase.LevelFrequency[i] / n;

            }
            return FrequencyMax(levelFrequency);
        }

        public int GetPmLevelByDate(int date)
        {
            double[] levelFrequency = new double[6];
            double n = dataBase.GetItemFrequency(dataBase.DateFrequency, date); ;
            for (int i = 0; i < 6; i++)
            {
                levelFrequency[i] = dataBase.DateFrequency[date, i] * dataBase.LevelFrequency[i] / n;
            }
            return FrequencyMax(levelFrequency);
        }

        public void TestGetPmLevelByDate()
        {
            Console.WriteLine(GetPmLevelByDate(dataBase.DataList[0]));
           
            /*
            for (int i = 0; i < 365; i++)
            {
                Console.WriteLine(GetPmLevelByDate(i));
            }
            */

        }

        public void TestGetPmLevel()
        {
            PmData data = dataBase.DataList[0];
            Console.WriteLine(GetPmLevel(data));
            data.Cbwd = -114514;
            Console.WriteLine(GetPmLevel(data));
        }

        public void TestData()
        {
            PmData data = new PmData();
            if (data.Pid == null)
            {
                Console.WriteLine("1");
            }
            else
            {
                Console.WriteLine(data.Pid);
            }
        }

        public int FrequencyMax(double[] box)
        {
            double prequency = box[0];
            int n = 0;
            for(int i = 0;i<box.Length;i++)
            {
                if (box[i] > prequency)
                {
                    prequency = box[i];
                    n = i;
                }
            }
            return n;
        }

        public void TestRight()
        {
            double count = 0;
            double right = 0;
            foreach(PmData i in dataBase.TestList)
            {
                count++;
                if (System.Math.Abs(GetPmLevel(i)-i.level)<=1)
                {
                    right++;
                }
            }
            Console.WriteLine(right / count);
        }
    }
}
