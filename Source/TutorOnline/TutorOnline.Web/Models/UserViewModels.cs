using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TutorOnline.Web.Models
{
    public class CreateUserViewModels
    {
        [Required]
        public int RoleID { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }

        public Nullable<System.DateTime> BirthDate { get; set; }

        public int Gender { get; set; }

        public string Address { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string SkypeID { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string BankID { get; set; }

        public Nullable<decimal> Salary { get; set; }

        public Nullable<decimal> Wallet { get; set; }

        public byte[] Photo { get; set; }

        public string Description { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")] 
        public string ConfirmPassword { get; set; }

        public string BankName { get; set; }

        public string BMemName { get; set; }
    }
}