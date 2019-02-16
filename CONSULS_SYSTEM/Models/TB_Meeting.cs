using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CONSULS_SYSTEM.Models
{
    public partial class TB_Meeting
    {
        public int ID { get; set; }
        [StringLength(10)]
        [Display(Name = "Meeting ID")]
        public string Meeting_ID { get; set; }
        [StringLength(200)]
        [Display(Name = "Meeting Title")]
        public string Meeting_Title { get; set; }
        [Column(TypeName = "datetime")]
        [Display(Name = "Meeting Date")]
        public DateTime? Meeting_Date { get; set; }
        [Display(Name = "Active")]
        public bool? Active { get; set; }
        [StringLength(50)]
        [Display(Name = "Create by")]
        public string Createby { get; set; }
        [Column(TypeName = "datetime")]
        [Display(Name = "Create Date")]
        public DateTime? CreateDate { get; set; }
    }
}
