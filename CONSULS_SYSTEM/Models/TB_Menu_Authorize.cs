using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CONSULS_SYSTEM.Models
{
    public partial class TB_Menu_Authorize
    {
        [Key]
        public int ID { get; set; }
        [StringLength(10)]
        [Display(Name = "Menu ID")]
        public string Menu_ID { get; set; }
        [Display(Name = "Level ID")]
        public int Level_ID { get; set; }
        [StringLength(1)]
        [Display(Name = "Allow")]
        public string Allow { get; set; }
        [StringLength(50)]
        [Display(Name = "Create by")]
        public string Createby { get; set; }
        [Column(TypeName = "datetime")]
        [Display(Name = "Create Date")]
        public DateTime? CreateDate { get; set; }

        [ForeignKey("Menu_ID")]
        public TB_Menu_Information menu_info { get; set; }
        [ForeignKey("Level_ID")]
        public MT_Level level { get; set; }
    }
}
