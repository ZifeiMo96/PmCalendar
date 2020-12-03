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
        public List<PmData> DataList { get; set; }
        /// <summary>
        /// 初始化函数
        /// </summary>
        public Service()
        {
            DataList = new List<PmData>();
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
                        DataList.Add(data);
                    }
                    foreach(PmData i in DataList)
                    {
                        Console.WriteLine(i.ToString());
                    }
                }
            }
            return true;
        }
    }
}
