using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CONSULS_SYSTEM.Models
{
    public partial class TB_Events
    {
        public int ID { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [StringLength(1000)]
        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }
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
        [StringLength(1)]
        [Display(Name = "Status")]
        public string Status { get; set; }
        [Display(Name = "Type")]
        public int Type { get; set; }
        [Display(Name = "Country ID")]
        public int Country_ID { get; set; }
        [Display(Name = "Events Date")]
        public DateTime? EventsDate { get; set; }

        [ForeignKey("Type")]
        public MT_Level level { get; set; }
        [ForeignKey("Country_ID")]
        public MT_Country country { get; set; }
    }
}
