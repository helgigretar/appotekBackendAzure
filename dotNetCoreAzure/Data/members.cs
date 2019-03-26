using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetCoreAzure.Data
{
    public class members
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string username { get; set; }
        [StringLength(100)]
        public string password { get; set; }
        public string repeatpassword { get; set; }
        public string name { get; set; }
        public String role { get; set; }

    }
}
