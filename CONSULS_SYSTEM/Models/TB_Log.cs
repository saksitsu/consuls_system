using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CONSULS_SYSTEM.Models
{
    public partial class TB_Log
    {
        public int ID { get; set; }
        [StringLength(50)]
        [Display(Name = "Controller")]
        public string Controller { get; set; }
        [StringLength(50)]
        [Display(Name = "Function Name")]
        public string Function_Name { get; set; }
        [Display(Name = "Message")]
        public string Message { get; set; }
        [StringLength(50)]
        [Display(Name = "Mac Address")]
        public string Mac_Address { get; set; }
        [StringLength(50)]
        [Display(Name = "IP Address")]
        public string IP_Address { get; set; }
        [StringLength(50)]
        [Display(Name = "Role Name")]
        public string Role_Name { get; set; }
        [Display(Name = "Level ID")]
        public int? Level_ID { get; set; }
        [StringLength(50)]
        [Display(Name = "Create by")]
        public string Createby { get; set; }
        [Column(TypeName = "datetime")]
        [Display(Name = "Create Date")]
        public DateTime? CreateDate { get; set; }
    }
}
