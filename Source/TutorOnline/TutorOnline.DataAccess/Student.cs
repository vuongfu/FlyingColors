//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TutorOnline.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            this.Schedules = new HashSet<Schedule>();
            this.StudentFeedbacks = new HashSet<StudentFeedback>();
            this.StudentSubjects = new HashSet<StudentSubject>();
            this.TutorFeedbacks = new HashSet<TutorFeedback>();
        }
    
        public int StudentId { get; set; }
        public int RoleId { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string SkypeId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public bool isActived { get; set; }
        public Nullable<System.DateTime> RegisterDate { get; set; }
    
        public virtual Parent Parent { get; set; }
        public virtual Role Role { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Schedule> Schedules { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentFeedback> StudentFeedbacks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TutorFeedback> TutorFeedbacks { get; set; }
    }
}
