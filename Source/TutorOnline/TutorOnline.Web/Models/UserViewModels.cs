﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TutorOnline.DataAccess;

namespace TutorOnline.Web.Models
{
    public class IndexUserViewModel
    {
        
        public int Id { get; set; }

        public int RoleID { get; set; }

        [Display(Name = "Chức vụ:")]
        public string RoleName { get; set; }

        [Display(Name = "Họ:")]
        public string LastName { get; set; }

        [Display(Name = "Tên:")]
        public string FirstName { get; set; }

        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Display(Name = "Tên đăng nhập:")]
        public string Username { get; set; }

        [Display(Name = "Giới tính:")]
        public int Gender { get; set; }

        [Display(Name = "Số điện thoại:")]
        public string PhoneNumber { get; set; }
    }



    public class DetailBackEndUserViewModels
    {
        public int Id { get; set; }
        public int RoleID { get; set; }

        [Display(Name = "Chức vụ:")]
        public string RoleName { get; set; }

        [Display(Name = "Họ:")]
        public string LastName { get; set; }

        [Display(Name = "Tên:")]
        public string FirstName { get; set; }

        [Display(Name = "Ngày sinh:")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> BirthDate { get; set; }

        [Display(Name = "Giới tính:")]
        public int Gender { get; set; }

        [Display(Name = "Địa chỉ:")]
        public string Address { get; set; }

        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Display(Name = "Thành phố:")]
        public string City { get; set; }

        [Display(Name = "Quốc gia:")]
        public string Country { get; set; }

        [Display(Name = "Số điện thoại:")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Ảnh:")]
        public byte[] Photo { get; set; }

        [Display(Name = "Mô tả:")]
        public string Description { get; set; }

        [Display(Name = "Tên đăng nhập:")]
        public string Username { get; set; }

        public DetailBackEndUserViewModels()
        {

        }

        public DetailBackEndUserViewModels(BackendUser data)
        {
            this.Address = data.Address;
            this.BirthDate = data.BirthDate;
            this.City = data.City;
            this.Country = data.Country;
            this.Description = data.Description;
            this.Email = data.Email;
            this.FirstName = data.FirstName;
            this.Gender = data.Gender;
            this.Id = data.BackendUserId;
            this.LastName = data.LastName;
            this.PhoneNumber = data.PhoneNumber;
            this.Photo = data.Photo;          
            this.RoleID = data.RoleId;
            this.RoleName = data.Role.RoleName;           
            this.Username = data.UserName;          
        }

    }

    public class DetailParentUserViewModels
    {
        public int Id { get; set; }
        public int RoleID { get; set; }

        [Display(Name = "Chức vụ:")]
        public string RoleName { get; set; }

        [Display(Name = "Họ:")]
        public string LastName { get; set; }

        [Display(Name = "Tên:")]
        public string FirstName { get; set; }

        [Display(Name = "Ngày sinh:")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> BirthDate { get; set; }

        [Display(Name = "Giới tính:")]
        public int Gender { get; set; }

        [Display(Name = "Địa chỉ:")]
        public string Address { get; set; }

        [Display(Name = "Skype:")]
        public string SkypeID { get; set; }

        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Display(Name = "Mã vùng:")]
        public string PostalCode { get; set; }

        [Display(Name = "Thành phố:")]
        public string City { get; set; }

        [Display(Name = "Quốc gia:")]
        public string Country { get; set; }

        [Display(Name = "Số điện thoại:")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Ảnh:")]
        public byte[] Photo { get; set; }

        [Display(Name = "Mô tả:")]
        public string Description { get; set; }

        [Display(Name = "Tên đăng nhập:")]
        public string Username { get; set; }

        [Display(Name = "Số dư:")]
        [DisplayFormat(DataFormatString = "{0:0,0.00}")]
        public Nullable<decimal> Wallet { get; set; }

        public DetailParentUserViewModels()
        {

        }

        public DetailParentUserViewModels(Parent data)
        {
            this.Address = data.Address;
            this.BirthDate = data.BirthDate;
            this.City = data.City;
            this.Country = data.Country;
            this.Description = data.Description;
            this.Email = data.Email;
            this.FirstName = data.FirstName;
            this.Gender = data.Gender;
            this.Id = data.ParentId;
            this.LastName = data.LastName;
            this.PhoneNumber = data.PhoneNumber;
            this.Photo = data.Photo;
            this.PostalCode = data.PostalCode;
            this.RoleID = data.RoleId;
            this.RoleName = data.Role.RoleName;
            this.SkypeID = data.SkypeId;
            this.Username = data.UserName;
            this.Wallet = data.Balance;
        }
    }

    public class DetailTutorUserViewModels
    {
        public int Id { get; set; }
        public int RoleID { get; set; }

        [Display(Name = "Chức vụ:")]
        public string RoleName { get; set; }

        [Display(Name = "Họ:")]
        public string LastName { get; set; }

        [Display(Name = "Tên:")]
        public string FirstName { get; set; }

        [Display(Name = "Ngày sinh:")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> BirthDate { get; set; }

        [Display(Name = "Giới tính:")]
        public int Gender { get; set; }

        [Display(Name = "Địa chỉ:")]
        public string Address { get; set; }

        [Display(Name = "Skype:")]
        public string SkypeID { get; set; }

        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Display(Name = "Mã vùng:")]
        public string PostalCode { get; set; }

        [Display(Name = "Thành phố:")]
        public string City { get; set; }

        [Display(Name = "Quốc gia:")]
        public string Country { get; set; }

        [Display(Name = "Số điện thoại:")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Ảnh:")]
        public byte[] Photo { get; set; }

        [Display(Name = "Mô tả:")]
        public string Description { get; set; }

        [Display(Name = "Tên đăng nhập:")]
        public string Username { get; set; }

        [Display(Name = "Số dư:")]
        [DisplayFormat(DataFormatString = "{0:0,0.00}")]
        public Nullable<decimal> Wallet { get; set; }

        [Display(Name = "Số tài khoản:")]
        public string BankID { get; set; }

        [Display(Name = "Lương:")]
        [DisplayFormat(DataFormatString = "{0:0,0.00}")]
        public Nullable<decimal> Salary { get; set; }

        [Display(Name = "Tên ngân hàng:")]
        public string BankName { get; set; }

        [Display(Name = "Người thụ hưởng:")]
        public string BMemName { get; set; }
        public DetailTutorUserViewModels()
        {

        }

        public DetailTutorUserViewModels(Tutor data)
        {
            this.Address = data.Address;
            this.BirthDate = data.BirthDate;
            this.City = data.City;
            this.Country = data.Country;
            this.Description = data.Description;
            this.Email = data.Email;
            this.FirstName = data.FirstName;
            this.Gender = data.Gender;
            this.Id = data.TutorId;
            this.LastName = data.LastName;
            this.PhoneNumber = data.PhoneNumber;
            this.Photo = data.Photo;
            this.PostalCode = data.PostalCode;
            this.RoleID = data.RoleId;
            this.RoleName = data.Role.RoleName;
            this.SkypeID = data.SkypeId;
            this.Username = data.UserName;
            this.Wallet = data.Balance;
            this.Salary = data.Salary;
            this.BankID = data.BankId;
            this.BankName = data.BankName;
            this.BMemName = data.BMemName;
        }
    }


    public class DetailStudentUserViewModels
    {
        public int Id { get; set; }
        public int RoleID { get; set; }

        public int? ParentId { get; set; }

        [Display(Name = "Chức vụ:")]
        public string RoleName { get; set; }

        [Display(Name = "Họ:")]
        public string LastName { get; set; }

        [Display(Name = "Tên:")]
        public string FirstName { get; set; }

        [Display(Name = "Phụ huynh:")]
        public string ParentName { get; set; }

        [Display(Name = "Ngày sinh:")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> BirthDate { get; set; }

        [Display(Name = "Giới tính:")]
        public int Gender { get; set; }

        [Display(Name = "Địa chỉ:")]
        public string Address { get; set; }

        [Display(Name = "Skype:")]
        public string SkypeID { get; set; }

        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Display(Name = "Mã vùng:")]
        public string PostalCode { get; set; }

        [Display(Name = "Thành phố:")]
        public string City { get; set; }

        [Display(Name = "Quốc gia:")]
        public string Country { get; set; }

        [Display(Name = "Số điện thoại:")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Ảnh:")]
        public byte[] Photo { get; set; }

        [Display(Name = "Mô tả:")]
        public string Description { get; set; }

        [Display(Name = "Tên đăng nhập:")]
        public string Username { get; set; }

        [Display(Name = "Số dư:")]
        [DisplayFormat(DataFormatString = "{0:0,0.00}")]
        public Nullable<decimal> Wallet { get; set; }

        public DetailStudentUserViewModels()
        {

        }

        public DetailStudentUserViewModels(Student data)
        {
            this.Address = data.Address;
            this.BirthDate = data.BirthDate;
            this.City = data.City;
            this.Country = data.Country;
            this.Description = data.Description;
            this.Email = data.Email;
            this.FirstName = data.FirstName;
            this.Gender = data.Gender;
            this.Id = data.StudentId;
            this.LastName = data.LastName;
            this.PhoneNumber = data.PhoneNumber;
            this.Photo = data.Photo;
            this.PostalCode = data.PostalCode;
            this.RoleID = data.RoleId;
            this.RoleName = data.Role.RoleName;
            this.SkypeID = data.SkypeId;
            this.Username = data.UserName;
            this.Wallet = data.Balance;
            this.ParentId = data.ParentId;
            this.ParentName = data.Parent.LastName +" " + data.Parent.LastName;
        }
    }


    public class CreateUserViewModels
    {
        [Required]
        [Display(Name = "Chức vụ:")]
        public int RoleID { get; set; }

        [Required]
        [Display(Name = "Họ:")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Tên:")]
        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày sinh:")]
        public Nullable<System.DateTime> BirthDate { get; set; }

        [Display(Name = "Giới tính:")]
        public int Gender { get; set; }

        [Display(Name = "Địa chỉ:")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Skype:")]
        public string SkypeID { get; set; }

        [Display(Name = "Thành phố:")]
        public string City { get; set; }

        [Display(Name = "Mã vùng:")]
        public string PostalCode { get; set; }

        [Display(Name = "Quốc gia:")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Số điện thoại:")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Số tài khoản:")]
        public string BankID { get; set; }

        [Display(Name = "Mức lương:")]
        public Nullable<decimal> Salary { get; set; }

        [Display(Name = "Số dư:")]
        public Nullable<decimal> Wallet { get; set; }

        [Display(Name = "Ảnh:")]
        public byte[] Photo { get; set; }

        [Display(Name = "Miêu tả:")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Tên đăng nhập:")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")] 
        public string ConfirmPassword { get; set; }

        [Display(Name = "Tên ngân hàng:")]
        public string BankName { get; set; }

        [Display(Name = "Người thụ hưởng:")]
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