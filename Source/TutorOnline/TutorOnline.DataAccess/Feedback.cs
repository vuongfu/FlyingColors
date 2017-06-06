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
    
    public partial class Feedback
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Feedback()
        {
            this.FeedbackDetails = new HashSet<FeedbackDetail>();
        }
    
        public int Id { get; set; }
        public Nullable<int> AssessorID { get; set; }
        public Nullable<int> JudgedID { get; set; }
        public Nullable<int> SlotID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeedbackDetail> FeedbackDetails { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual Slot Slot { get; set; }
    }
}
