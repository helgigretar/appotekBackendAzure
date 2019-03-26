using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetCoreAzure.Pages.myObjects
{
    public class medicine
    {
        private object p;

        public int id { get; set; }
        public String name { get; set; }
        public String active_ingredient { get; set; }
        public String pharmaceutical_form { get; set; }
        public String strength { get; set; }
        public String atc_code { get; set; }
        public String vnr { get; set; }
        public String other_info { get; set; }
        public String marketed { get; set; }
        public String ma_issued { get; set; }
        public String legal_status { get; set; }
        public medicine(int Id, String Name, String Active_ingredient, String Pharmaceutical_form, 
                        String Strength, String Atc_code,String Vnr, String Other_info, 
                        String Marketed, String Ma_issued,String Legal_status)

        {
            id = Id;
            name = Name;
            active_ingredient = Active_ingredient;
            pharmaceutical_form = Pharmaceutical_form;
            strength = Strength;
            atc_code = Atc_code;
            vnr = Vnr;
            other_info = Other_info;
            marketed = Marketed;
            ma_issued = Ma_issued;
            legal_status = Legal_status;
        }
    }
}
