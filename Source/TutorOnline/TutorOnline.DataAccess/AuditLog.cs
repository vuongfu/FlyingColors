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
    
    public partial class AuditLog
    {
        public int Id { get; set; }
        public int DocumentID { get; set; }
        public int ModifierID { get; set; }
        public System.DateTime ModifyDate { get; set; }
        public string Content { get; set; }
    
        public virtual Document Document { get; set; }
        public virtual User User { get; set; }
    }
}
