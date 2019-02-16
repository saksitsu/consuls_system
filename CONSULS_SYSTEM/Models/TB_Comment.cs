using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CONSULS_SYSTEM.Models
{
    public partial class TB_Comment
    {
        public int ID { get; set; }
        [Display(Name = "Board ID")]
        public int? Board_ID { get; set; }
        [StringLength(1000)]
        [Display(Name = "Comment")]
        public string Comment { get; set; }
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
    }
}
