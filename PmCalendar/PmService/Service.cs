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
                /*
                dataBase.HourFrequency[][i.level] += 1;
                dataBase.DateFrequency[][i.level] += 1;
                dataBase.CbwdFrequency[][i.level] += 1;
                dataBase.TempFrequency[][i.level] += 1;
                dataBase.PresFrequency[][i.level] += 1;
                dataBase.LwsFrequency[][i.level] += 1;
                dataBase.DewpFrequency[][i.level] += 1;
                */
            }
        }

        
    }
}
