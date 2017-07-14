using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TutorOnline.DataAccess;
namespace TutorOnline.Web.Models
{
    public class StudentSubjectViewModels
    {
        public int StudentSubjectId { get; set; }
        public int SubjectId { get; set; }
        public int StudentId { get; set; }
        public int Status { get; set; }

        public virtual Status Status1 { get; set; }
        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }
    }
}