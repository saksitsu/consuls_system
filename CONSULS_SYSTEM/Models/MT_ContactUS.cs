using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CONSULS_SYSTEM.Models
{
    public partial class MT_ContactUS
    {
        public int ID { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required]
        [StringLength(200),Display(Name = "Email_1")]
        public string Email_1 { get; set; }
        [StringLength(200), Display(Name = "Email_2")]
        public string Email_2 { get; set; }
        [StringLength(200), Display(Name = "Email_3")]
        public string Email_3 { get; set; }
        [Required]
        [StringLength(20)]
        [Display(Name = "Phone Number1")]
        public string Phone_Number1 { get; set; }
        [StringLength(20)]
        [Display(Name = "Phone Number2")]
        public string Phone_Number2 { get; set; }
        [StringLength(20)]
        [Display(Name = "Phone Number3")]
        public string Phone_Number3 { get; set; }
        [StringLength(500)]
        [Display(Name = "Google Map URL")]
        public string Google_Map_URL { get; set; }
        [Display(Name = "Active")]
        public bool? Active { get; set; }
        [StringLength(50)]
        [Display(Name = "Create by")]
        public string Createby { get; set; }
        [Column(TypeName = "datetime")]
        [Display(Name = "Create Date")]
        public DateTime? CreateDate { get; set; }
    }
}
