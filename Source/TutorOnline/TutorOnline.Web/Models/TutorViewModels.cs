using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class TutorSubjectViewModels
    {
        public int TutorSubjectId { get; set; }
        public string subjectName { get; set; }
        public string experiences { get; set; }
    }
    public class TutorInfoViewModels
    {
        public int TutorId { get; set; }
        public string FullName { get; set; }
        public string Photo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
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

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime RegisterDate { get; set; }
        public List<string> cateTeaching { get; set; }
        public List<TutorSubjectViewModels> tutorSub { get; set; }
        public List<TutorSubjectViewModels> newTutorSub { get; set; }
        public int [] newTusubId { get; set; }
    }
}