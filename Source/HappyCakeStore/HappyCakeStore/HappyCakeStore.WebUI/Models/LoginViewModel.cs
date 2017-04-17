using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HappyCakeStore.WebUI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "User name is required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }
        [StringLength(50, MinimumLength = 6)]
        public string Forgotpass { get; set; }
    }
}