﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TutorOnline.Web.Models
{
    public class DetailUserViewModels
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> BirthDate { get; set; }

        public int Gender { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string SkypeID { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public string PhoneNumber { get; set; }

        public string BankID { get; set; }

        [DisplayFormat(DataFormatString = "{0:0,0.00}")]
        public Nullable<decimal> Salary { get; set; }

        [DisplayFormat(DataFormatString = "{0:0,0.00}")]
        public Nullable<decimal> Wallet { get; set; }

        public byte[] Photo { get; set; }

        public string Description { get; set; }

        public string Username { get; set; }

        public string BankName { get; set; }

        public string BMemName { get; set; }
    }

    public class CreateUserViewModels
    {
        [Required]
        public int RoleID { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [DataType(DataType.Date)]
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

    public class ChangePasswordViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Họ và tên:")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu cũ:")]
        public string Password { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} phải dài ít nhất {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu mới:")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = " Xác nhận mật khẩu:")]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu mới và mật khẩu xác nhận không khớp")]
        public string ConfirmPassword { get; set; }

    }
}