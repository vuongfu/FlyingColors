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
        public int AuditId { get; set; }
        public string TableName { get; set; }
        public int ModifierId { get; set; }
        public int ModifierRole { get; set; }
        public System.DateTime ModifyDate { get; set; }
        public string Content { get; set; }
    
        public virtual Role Role { get; set; }
    }
}
