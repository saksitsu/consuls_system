using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CONSULS_SYSTEM.Models
{
    public partial class MT_Country
    {
        [Key]
        [Display(Name = "Country ID")]
        public int Country_ID { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [StringLength(2)]
        [Display(Name = "Short Name")]
        public string Short_Name { get; set; }
        [Display(Name = "Active")]
        public bool? Active { get; set; }
        [StringLength(50)]
        [Display(Name = "Create by")]
        public string Createby { get; set; }
        [Column(TypeName = "datetime")]
        [Display(Name = "Create Date")]
        public DateTime? CreateDate { get; set; }
        [StringLength(200)]
        [Display(Name = "Image")]
        public string Img_Url { get; set; }
        [Display(Name = "Description")]
        public string Descriptions { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
