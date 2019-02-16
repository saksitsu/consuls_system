using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CONSULS_SYSTEM.Models
{
    public class Login
    {
        [Required]
        [Display(Name ="Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        //[Required(ErrorMessage = "This field is required")]
        [Display(Name = "E-Mail")]
        public string email { get; set; }

        /// <summary>
        /// For Vailidate with Sign in
        /// </summary>
        public string Validation { get; set; }
    }
}
