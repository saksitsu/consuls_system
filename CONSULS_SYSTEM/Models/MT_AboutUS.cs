using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CONSULS_SYSTEM.Models
{
    public partial class MT_AboutUS
    {
        public int ID { get; set; }
        [StringLength(200)]
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Active")]
        public bool? Active { get; set; }
        [StringLength(50)]
        [Display(Name = "Create by")]
        public string Createby { get; set; }
        [Column(TypeName = "datetime")]
        [Display(Name = "Create Date")]
        public DateTime? CreateDate { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Title TH")]
        public string Title2 { get; set; }
        [Required]
        [Display(Name = "Description TH")]
        public string Description2 { get; set; }
    }
}
