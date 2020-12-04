using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmService
{
    class PmDataBase
    {
        public List<PmData> DataList { get; set; }

        public PmDataBase()
        {
            DataList = new List<PmData>();
        }

        public void AddData(PmData data)
        {
            DataList.Add(data);
        }
    }
}
