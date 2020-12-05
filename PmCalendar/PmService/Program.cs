using System;
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
            String path = "../../../train.csv";
            Service service = new Service(path);
            service.LoadTestPmData(path);
            service.TestRight();

        }
    }
}
