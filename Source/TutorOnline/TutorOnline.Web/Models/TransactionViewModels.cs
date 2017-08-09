using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TutorOnline.DataAccess;

namespace TutorOnline.Web.Models
{
    public class TransactionListViewModels
    {
        [Required(ErrorMessage = "")]
        [Display(Name = "Mã giao dịch:")]
        public int TransactionId { get; set; }
        [Display(Name = "Nội dung:")]
        public string Content { get; set; }
        [Display(Name = "Số tiền:")]
        public int Amount { get; set; }

        [Display(Name = "ngày giờ giao dịch:")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public System.DateTime TranDate { get; set; }
        [Display(Name = "Số tài khoản:")]
        public int UserID { get; set; }
        [Display(Name = "Tên tài khoản:")]
        public String UserName { get; set; }
        [Display(Name = "Tên người dùng:")]
        public String Name { get; set; }
        [Display(Name = "Loại tài khoản:")]
        public int UserType { get; set; }
        [Display(Name = "Loại tài khoản:")]
        public String UserTypeName { get; set; }
    }
    public class TransactionViewModels
    {
        [Display(Name = "Mã giao dịch:")]
        public int TransactionId { get; set; }
        [Required(ErrorMessage = "Xin hãy điền nội dung giao dịch")]
        [Display(Name = "Nội dung:")]
        public string Content { get; set; }
        [Required(ErrorMessage = "Xin hãy điền số tiền giao dịch")]
        [Display(Name = "Số tiền:")]
        public int Amount { get; set; }
        [Display(Name = "ngày giờ giao dịch:")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public System.DateTime TranDate { get; set; }
        [Display(Name = "Số tài khoản:")]
        public int UserID { get; set; }
        [Display(Name = "Tên tài khoản:")]
        public String UserName { get; set; }
        [Display(Name = "Tên người dùng:")]
        public String Name { get; set; }
        [Display(Name = "Loại tài khoản:")]
        public int UserType { get; set; }
        [Display(Name = "Loại tài khoản:")]
        public String UserTypeName { get; set; }
        public TransUserViewModel User { get; set; }
        public int Balance { get; set; }
    }

}