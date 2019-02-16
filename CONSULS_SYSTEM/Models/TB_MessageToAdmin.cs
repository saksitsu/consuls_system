using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CONSULS_SYSTEM.Models
{
    public partial class TB_MessageToAdmin
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [StringLength(200),Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [StringLength(1000),Required]
        [Display(Name = "Message")]
        public string Message { get; set; }
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
        [StringLength(20)]
        [Display(Name = "User_ID")]
        public string User_ID { get; set; }
        [StringLength(20)]
        [Display(Name = "Topic")]
        public string Topic { get; set; }
        [StringLength(200)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [StringLength(200)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [StringLength(15)]
        [Display(Name = "Phone Number")]
        public string Phone_Number { get; set; }
        //[StringLength(200)]
        //public string Img_Url { get; set; }

        [Display(Name = "Country ID")]
        public int Country_ID { get; set; }

        [ForeignKey("Type")]
        public MT_Level level { get; set; }
    }
}
