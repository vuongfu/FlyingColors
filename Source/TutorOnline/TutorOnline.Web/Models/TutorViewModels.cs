using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TutorOnline.Web.Models
{
    public class TutorBookSlotViewModel
    {
        public int TutorId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderSlot { get; set; }
    }

    public class TutorInfoViewModels
    {
        public int TutorId { get; set; }
        public string FullName { get; set; }
        public string Photo { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string SkypeId { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public Nullable<decimal> Salary { get; set; }
        public string Description { get; set; }
        public string BankId { get; set; }
        public string BankName { get; set; }
        public string BMemName { get; set; }
        public string isActived { get; set; }
        public System.DateTime RegisterDate { get; set; }
    }
}