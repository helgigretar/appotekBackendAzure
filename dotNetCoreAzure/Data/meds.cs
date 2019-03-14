using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetCoreAzure.Data
{
    public class meds
    {
        public int id { get; set; }
        public String name { get; set; }
        public String active_ingredient { get; set; }
        public String pharmaceutical_form { get; set; }
        public String strength { get; set; }
        public String atc_code { get; set; }
        public String legal_status { get; set; }
        public String vnr { get; set; }
        public String other_info { get; set; }
        public String marketed { get; set; }
        public String ma_issued { get; set; }
    }
}
