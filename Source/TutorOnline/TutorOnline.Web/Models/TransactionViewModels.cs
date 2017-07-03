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
        [Display(Name = "Mã giao dịch:")]
        public int TransactionId { get; set; }
        [Display(Name = "Nội dung:")]
        public string Content { get; set; }
        [Display(Name = "Số tiền:")]
        public decimal Amount { get; set; }
        [Display(Name = "ngày giờ giao dịch:")]
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
        [Display(Name = "Nội dung:")]
        public string Content { get; set; }
        [Display(Name = "Số tiền:")]
        public decimal Amount { get; set; }
        [Display(Name = "ngày giờ giao dịch:")]
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
        public Nullable<decimal> Balance { get; set; }
    }

}