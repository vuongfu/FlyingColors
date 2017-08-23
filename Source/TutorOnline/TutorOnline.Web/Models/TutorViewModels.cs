using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TutorOnline.Common;

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

    public class StudentFeedbacks
    {
        public int StudentFeedbackId { get; set; }
        public int TutorId { get; set; }
        //public int StudentId { get; set; }
        //public int ScheduleId { get; set; }
        //public Nullable<int> LessonId { get; set; }
        //public System.DateTime FeedbackDate { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
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
        public Nullable<double> Salary { get; set; }
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
        public List<int> newTusubId { get; set; }

        public List<StudentFeedbacks> lstStuFb { get; set; }
    }

    public class BookedSlotByStudent
    {
        public string tableSlotId { get; set; }

        public int ScheduleId { get; set; }

        public string LessonName { get; set; }

        public string StudentName { get; set; }

        public int Status { get; set; }
    }

    public class DetailBookedSlotByStudent
    {
        public string FullName { get; set; }

        public string Gender { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Skype { get; set; }

        public string Category { get; set; }

        public string Subject { get; set; }

        public string Lesson { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int OrderSlot { get; set; }

        public string OrderTime { get; set; }

        public int ScheduleId { get; set; }

        public string Photo { get; set; }

        public int Status { get; set; }

        public string StatusName { get; set; }

        public List<int> CriteriaId { get; set; }

        public List<string> CriteriaContent { get; set; }

        public DateTime ScheduleDate { get; set; }

        public bool CanFeedback { get; set; }

        public string Comment { get; set; }
    }

    public class ViewFeedbackViewModel
    {
        public string CriteriaName { get; set; }

        public int criteriaId { get; set; }

        public int value { get; set; }

    }

    public class RegistTutorSubjectViewModel
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
    }

    public class DetailTutorViewModel
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

        [DisplayFormat(DataFormatString = "{0:0,0.00}")]
        public Nullable<double> Salary { get; set; }
        public decimal Balance { get; set; }
        public string Description { get; set; }
        public string BankId { get; set; }
        public string BankName { get; set; }
        public string BMemName { get; set; }
        public string isActived { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public List<TutorSubjectViewModels> tutorSub { get; set; }
    }

    public class EditTutorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = UserCommonString.RequiredMess)]
        [Display(Name = "Họ:")]
        public string LastName { get; set; }

        [Required(ErrorMessage = UserCommonString.RequiredMess)]
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
        public string Address { get; set; }

        [Display(Name = "Thành phố:")]
        public string City { get; set; }

        [Display(Name = "Quốc gia:")]
        public string Country { get; set; }

        [Display(Name = "Mã vùng:")]
        public string potalCode { get; set; }

        [Required(ErrorMessage = UserCommonString.RequiredMess)]
        [Display(Name = "Số điện thoại:")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Ảnh:")]
        public string Photo { get; set; }

        [Display(Name = "Skype:")]
        public string skypeId { get; set; }

        [Display(Name = "Mô tả:")]
        public string Description { get; set; }

        [Display(Name = "Số tài khoản:")]
        public string BankId { get; set; }

        [Display(Name = "Tên ngân hàng:")]
        public string BankName { get; set; }

        [Display(Name = "Người thụ hưởng:")]
        public string BMemName { get; set; }

    }
}