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
    
    public partial class StudentFeedback
    {
        public int StudentFeedbackId { get; set; }
        public int TutorId { get; set; }
        public int StudentId { get; set; }
        public Nullable<int> LessonId { get; set; }
        public System.DateTime FeedbackDate { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
    
        public virtual Lesson Lesson { get; set; }
        public virtual Student Student { get; set; }
        public virtual Tutor Tutor { get; set; }
    }
}
