using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;

namespace TutorOnline.Web.Models
{
    public class PersonalViewModel
    {
        public int Id { get; set; }
        public int RoleID { get; set; }
        public string FullName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string RoleName { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> BirthDate { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string SkypeID { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] Photo { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "OldPassword")]
        [Compare("Password", ErrorMessage = "The old password is incorrect.")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class CategoriesViewModel
    {
        public int Id { get; set; }

        [Required]
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
    public class SubjectViewModel
    {
        public int Id { get; set; }

        [Required]
        public string SubjectName { get; set; }
        public int CategoryID { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string Purpose { get; set; }
        public string Requirement { get; set; }
        public byte[] Photo { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}