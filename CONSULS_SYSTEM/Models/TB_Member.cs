using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CONSULS_SYSTEM.Models
{
    public partial class TB_Member
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [StringLength(10),Display(Name = "User_ID"),Required]
        public string User_ID { get; set; }
        [StringLength(200), Display(Name = "Password")]//, Required(ErrorMessage = "This field is required")
        public string Password { get; set; }
        [StringLength(200), Display(Name = "Name")]
        public string Name { get; set; }
        [StringLength(200), Display(Name = "Sub_Name")]
        public string Sub_Name { get; set; }
        [Display(Name = "Country ID")]
        public int Country_ID { get; set; }
        [StringLength(200)]
        [Display(Name = "Position")]
        public string Position { get; set; }
        [StringLength(200)]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [StringLength(200)]
        [Display(Name = "Education")]
        public string Education { get; set; }
        [Display(Name = "Level ID")]
        public int Level_ID { get; set; }
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
        [Display(Name = "Email")]
        public string Email { get; set; }
        [StringLength(20)]
        [Display(Name = "Phone Number")]
        public string Phone_Number { get; set; }
        [StringLength(1000)]
        [Display(Name = "Line Token")]
        public string Line_Token { get; set; }
        [StringLength(200)]
        [Display(Name = "Image")]
        public string Img_Url { get; set; }

        [ForeignKey("Level_ID")]
        public MT_Level level { get; set; }
        [ForeignKey("Country_ID")]
        public MT_Country country { get; set; }
    }
}
