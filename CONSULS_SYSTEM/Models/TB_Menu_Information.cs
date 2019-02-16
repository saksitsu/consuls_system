using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CONSULS_SYSTEM.Models
{
    public partial class TB_Menu_Information
    {
        //[Key]
        //public int ID { get; set; }
        [Key]
        [StringLength(10)]
        [Display(Name = "Menu ID")]
        public string Menu_ID { get; set; }
        [StringLength(200)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [StringLength(200)]
        [Display(Name = "Action")]
        public string Action { get; set; }
        [StringLength(200)]
        [Display(Name = "Controller")]
        public string Controller { get; set; }
        [StringLength(50)]
        [Display(Name = "Sorting")]
        public string Sorting { get; set; }
        [StringLength(10)]
        [Display(Name = "Child Of Menu")]
        public string ChildOfMenu { get; set; }
        [Display(Name = "Active")]
        public bool? Active { get; set; }
        [StringLength(50)]
        [Display(Name = "Create by")]
        public string Createby { get; set; }
        [Column(TypeName = "datetime")]
        [Display(Name = "Create Date")]
        public DateTime? CreateDate { get; set; }
        [StringLength(50)]
        [Display(Name = "Icon")]
        public string Icon { get; set; }
    }
}
