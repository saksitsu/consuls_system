using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CONSULS_SYSTEM.Models
{
    public partial class TB_Webboard
    {
        public int ID { get; set; }
        [StringLength(200)]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [StringLength(1000)]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [StringLength(1000)]
        [Display(Name = "Other")]
        public string Other { get; set; }
        [StringLength(200)]
        [Display(Name = "Image")]
        public string Img_Url { get; set; }
        [Display(Name = "Image Gallery ID")]
        public int? Img_Gallary_ID { get; set; }
        [Display(Name = "Active")]
        public bool? Active { get; set; }
        [StringLength(50)]
        [Display(Name = "Create by")]
        public string Createby { get; set; }
        [Column(TypeName = "datetime")]
        [Display(Name = "Create Date")]
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// Ignore field for show to view
        /// </summary>
        public string image_user { get; set; }
        public string name_user { get; set; }

        //[ForeignKey("User_ID")]
        //public TB_Member member { get; set; }
    }
}
