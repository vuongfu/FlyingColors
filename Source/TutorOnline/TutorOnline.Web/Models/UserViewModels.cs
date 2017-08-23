using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TutorOnline.DataAccess;
using TutorOnline.Common;

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

    public class DetailUserViewModels
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
        public string Photo { get; set; }

        [Display(Name = "Mô tả:")]
        public string Description { get; set; }

        [Display(Name = "Tên đăng nhập:")]
        public string Username { get; set; }


        [Display(Name = "Skype:")]
        public string SkypeID { get; set; }


        [Display(Name = "Mã vùng:")]
        public string PostalCode { get; set; }

        [Display(Name = "Số dư:")]
        [DisplayFormat(DataFormatString = "{0:0,0.00}")]
        public Nullable<decimal> Balance { get; set; }

        [Display(Name = "Số tài khoản:")]
        public string BankID { get; set; }

        [Display(Name = "Lương:")]
        [DisplayFormat(DataFormatString = "{0:0,0.00}")]
        public Nullable<decimal> Salary { get; set; }

        [Display(Name = "Tên ngân hàng:")]
        public string BankName { get; set; }

        [Display(Name = "Người thụ hưởng:")]
        public string BMemName { get; set; }

        [Display(Name = "Phụ huynh:")]
        public string ParentName { get; set; }

        public int? ParentId { get; set; }

        public DetailUserViewModels()
        {

        }

        public DetailUserViewModels(BackendUser data)
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

        public DetailUserViewModels(Parent data)
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
            this.Balance = data.Balance;
        }

        public DetailUserViewModels(Tutor data)
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
            this.Balance = data.Balance;
            this.Salary = data.Salary;
            this.BankID = data.BankId;
            this.BankName = data.BankName;
            this.BMemName = data.BMemName;
        }

        public DetailUserViewModels(Student data)
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
            this.Balance = data.Balance;
            this.ParentId = data.ParentId;
            this.ParentName = data.Parent.LastName + " " + data.Parent.FirstName;
        }
    }

    public class DetailBackEndUserViewModels
    {
        public int Id { get; set; }
        public int RoleID { get; set; }

        [Display(Name = "Chức vụ:")]
        public string RoleName { get; set; }

        [Required(ErrorMessage = UserCommonString.RequiredMess)]
        [StringLength(50, ErrorMessage = "{0} chứa tối đa {1} ký tự.")]
        [Display(Name = "Họ:")]
        public string LastName { get; set; }

        [Required(ErrorMessage = UserCommonString.RequiredMess)]
        [StringLength(50, ErrorMessage = "{0} chứa tối đa {1} ký tự.")]
        [Display(Name = "Tên:")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = UserCommonString.RequiredMess)]
        [Display(Name = "Ngày sinh:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public Nullable<System.DateTime> BirthDate { get; set; }

        [Display(Name = "Giới tính:")]
        public int Gender { get; set; }

        [Display(Name = "Địa chỉ:")]
        [StringLength(300, ErrorMessage = "{0} chứa tối đa {1} ký tự.")]
        public string Address { get; set; }

        [Display(Name = "Email:")]
        [StringLength(50, ErrorMessage = "{0} chứa tối đa {1} ký tự.")]
        public string Email { get; set; }

        [Display(Name = "Thành phố:")]
        public string City { get; set; }

        [Display(Name = "Quốc gia:")]
        public string Country { get; set; }

        [Required(ErrorMessage = UserCommonString.RequiredMess)]
        [StringLength(50, ErrorMessage = "{0} chứa tối đa {1} ký tự.")]
        [Display(Name = "Số điện thoại:")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Ảnh:")]
        public string Photo { get; set; }

        [Display(Name = "Mô tả:")]
        [StringLength(200, ErrorMessage = "{0} chứa tối đa {1} ký tự.")]
        public string Description { get; set; }

        [Display(Name = "Tên đăng nhập:")]
        [StringLength(50, ErrorMessage = "{0} chứa tối đa {1} ký tự.")]
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
        public string Photo { get; set; }

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
        public string Photo { get; set; }

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
        public string Photo { get; set; }

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
            this.ParentName = data.Parent.LastName + " " + data.Parent.LastName;
        }
    }


    public class CreateUserViewModels
    {
        [Required(ErrorMessage = UserCommonString.RequiredMess)]
        [Display(Name = "Chức vụ:")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = UserCommonString.RequiredMess)]
        [StringLength(50, ErrorMessage = "{0} chứa tối đa {1} ký tự.")]
        [Display(Name = "Họ:")]
        public string LastName { get; set; }

        [Required(ErrorMessage = UserCommonString.RequiredMess)]
        [StringLength(50, ErrorMessage = "{0} chứa tối đa {1} ký tự.")]
        [Display(Name = "Tên:")]
        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày sinh:")]
        public Nullable<System.DateTime> BirthDate { get; set; }

        [Display(Name = "Giới tính:")]
        public int Gender { get; set; }

        [Display(Name = "Địa chỉ:")]
        [StringLength(300, ErrorMessage = "{0} chứa tối đa {1} ký tự.")]
        public string Address { get; set; }

        [Required(ErrorMessage = UserCommonString.RequiredMess)]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        [StringLength(50, ErrorMessage = "{0} chứa tối đa {1} ký tự.")]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Display(Name = "Thành phố:")]
        public string City { get; set; }

        [Display(Name = "Mã vùng:")]
        public string PostalCode { get; set; }

        [Display(Name = "Quốc gia:")]
        public string Country { get; set; }

        [Required(ErrorMessage = UserCommonString.RequiredMess)]
        [Display(Name = "Số điện thoại:")]
        [StringLength(24, ErrorMessage = "{0} chứa tối đa {1} ký tự.")]
        public string PhoneNumber { get; set; }


        [Display(Name = "Ảnh:")]
        public string Photo { get; set; }

        [Display(Name = "Miêu tả:")]
        [StringLength(200, ErrorMessage = "{0} chứa tối đa {1} ký tự.")]
        public string Description { get; set; }

        [Required(ErrorMessage = UserCommonString.RequiredMess)]
        [Display(Name = "Tên đăng nhập:")]
        public string Username { get; set; }

        [Required(ErrorMessage = UserCommonString.RequiredMess)]
        [StringLength(100, ErrorMessage = "{0} phải có ít nhất {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu và Xác nhận mật khẩu không trùng nhau.")]
        public string ConfirmPassword { get; set; }
    }

    public class CreateFrontEndUserViewModels
    {
        [Required(ErrorMessage = UserCommonString.RequiredMess)]
        [Display(Name = "Chức vụ:")]
        public int? RoleId { get; set; }

        public string RoleName { get; set; }

        [Required(ErrorMessage = UserCommonString.RequiredMess)]
        [StringLength(50, ErrorMessage = "{0} chứa tối đa {1} ký tự.")]
        [Display(Name = "Họ:")]
        public string LastName { get; set; }

        public int? ParentId { get; set; }

        [Required(ErrorMessage = UserCommonString.RequiredMess)]
        [StringLength(50, ErrorMessage = "{0} chứa tối đa {1} ký tự.")]
        [Display(Name = "Tên:")]
        public string FirstName { get; set; }


        [Display(Name = "Ngày sinh:")]
        public Nullable<System.DateTime> BirthDate { get; set; }

        [Required(ErrorMessage = UserCommonString.RequiredMess)]
        [Display(Name = "Giới tính:")]
        public int Gender { get; set; }

        [Display(Name = "Địa chỉ:")]
        [StringLength(300, ErrorMessage = "{0} chứa tối đa {1} ký tự.")]
        public string Address { get; set; }

        [Required(ErrorMessage = UserCommonString.RequiredMess)]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(50, ErrorMessage = "{0} chứa tối đa {1} ký tự.")]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Display(Name = "Thành phố:")]
        public string City { get; set; }

        [Display(Name = "Mã vùng:")]
        [StringLength(50, ErrorMessage = "{0} chứa tối đa {1} ký tự.")]
        public string PostalCode { get; set; }

        [Display(Name = "Quốc gia:")]
        public string Country { get; set; }

        [Required(ErrorMessage = UserCommonString.RequiredMess)]
        [StringLength(24, ErrorMessage = "{0} chứa tối đa {1} ký tự.")]
        [Display(Name = "Số điện thoại:")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = UserCommonString.RequiredMess)]
        [StringLength(100, ErrorMessage = "{0} chứa tối đa {1} ký tự.")]
        [Display(Name = "Skype")]
        public string SkypeId { get; set; }

        [Display(Name = "Ảnh:")]
        public string Photo { get; set; }

        [Display(Name = "Miêu tả:")]
        [StringLength(200, ErrorMessage = "{0} chứa tối đa {1} ký tự.")]
        public string Description { get; set; }

        [Required(ErrorMessage = UserCommonString.RequiredMess)]
        [Display(Name = "Tên đăng nhập:")]
        [StringLength(100, ErrorMessage = "{0} chứa tối đa {1} ký tự.")]
        public string Username { get; set; }

        [Required(ErrorMessage = UserCommonString.RequiredMess)]
        [StringLength(100, ErrorMessage = "{0} phải có ít nhất {2} kí tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu và Xác nhận mật khẩu không khớp.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Số tài khoản:")]
        [StringLength(50, ErrorMessage = "{0} chứa tối đa {1} ký tự.")]
        public string BankID { get; set; }

        [Display(Name = "Tên ngân hàng:")]
        [StringLength(200, ErrorMessage = "{0} chứa tối đa {1} ký tự.")]
        public string BankName { get; set; }

        [Display(Name = "Người thụ hưởng:")]
        [StringLength(200, ErrorMessage = "{0} chứa tối đa {1} ký tự.")]
        public string BMemName { get; set; }

        List<TutorSubjectRegisterViewModel> listSubject { get; set; }

    }

    public class TutorSubjectRegisterViewModel
    {
        public int TutorSubjectId { get; set; }
        public string Experience { get; set; }
    }

    public class ChangePasswordViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Họ và tên:")]
        public string Name { get; set; }

        [Required(ErrorMessage = UserCommonString.RequiredMess)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu cũ:")]
        public string Password { get; set; }

        [Required(ErrorMessage = UserCommonString.RequiredMess)]
        [StringLength(100, ErrorMessage = "{0} phải dài ít nhất {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu mới:")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = " Xác nhận mật khẩu:")]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu mới và mật khẩu xác nhận không khớp")]
        public string ConfirmPassword { get; set; }

        public int UserRole { get; set; }

        public string Rolename { get; set; }
    }
    public class TransUserViewModel
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

        [Display(Name = "Số dư:")]
        public int Balance { get; set; }
    }
}