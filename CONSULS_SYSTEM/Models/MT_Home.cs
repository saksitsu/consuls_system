using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CONSULS_SYSTEM.Models
{
    public partial class MT_Home
    {
        public int ID { get; set; }
        [StringLength(200)]
        [Display(Name = "Header")]
        public string Header { get; set; }
        [Display(Name = "Message")]
        public string Message { get; set; }
        [StringLength(50)]
        [Display(Name = "Credit")]
        public string Credit { get; set; }
        [StringLength(200)]
        [Display(Name = "Footer")]
        public string Footer { get; set; }
        [StringLength(200)]
        [Display(Name = "Image")]
        public string Img_Url { get; set; }
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
