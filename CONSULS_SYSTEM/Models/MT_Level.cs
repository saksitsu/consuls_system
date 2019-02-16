using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CONSULS_SYSTEM.Models
{
    public partial class MT_Level
    {
        [Key]
        [Display(Name = "Level ID")]
        public int Level_ID { get; set; }
        [StringLength(200)]
        [Display(Name = "Level ID")]
        public string Name { get; set; }
        [Display(Name = "Active")]
        public bool? Active { get; set; }
    }
}
