using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CONSULS_SYSTEM.Models
{
    public class MyAlert
    {
        [Display(Name = "Status")]
        public string Status { get; set; }
        [Display(Name = "Message")]
        public string Message { get; set; }
    }
}
