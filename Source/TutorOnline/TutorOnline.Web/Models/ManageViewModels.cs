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
        public Nullable<int> RoleID { get; set; }
        public Nullable<int> ParentID { get; set; }
        public string FullName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string RoleName { get; set; }
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
        public Nullable<decimal> Salary { get; set; }
        public Nullable<decimal> Wallet { get; set; }
        public byte[] Photo { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string BankName { get; set; }
        public string BMemName { get; set; }
    }
}