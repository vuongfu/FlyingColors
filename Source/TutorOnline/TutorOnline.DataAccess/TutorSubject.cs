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
    
    public partial class TutorSubject
    {
        public int TutorSubjectId { get; set; }
        public int SubjectId { get; set; }
        public int TutorId { get; set; }
        public int Status { get; set; }
        public string Experience { get; set; }
        public bool isActived { get; set; }
    
        public virtual Status Status1 { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Tutor Tutor { get; set; }
    }
}
