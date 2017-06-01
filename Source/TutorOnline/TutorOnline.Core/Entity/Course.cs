using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Tutor.Core.Entity
{
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string Title { get; set; }
        public int SubjectID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string Purpose { get; set; }
        public string Requirement { get; set; }
        public string Photo { get; set; }
        public Decimal Price { get; set; }

    }
}
