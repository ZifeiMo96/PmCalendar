﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmService
{
    class Program
    {
        static void Main(string[] args)
        {
            Service service = new Service();

            service.LoadPmData("F:\\school\\商务智能\\PM2.5\\北京PM2.5浓度回归数据\\train.csv");

        }
    }
}