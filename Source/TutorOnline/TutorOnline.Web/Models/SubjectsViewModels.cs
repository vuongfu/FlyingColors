using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TutorOnline.Web.Models
{
    public class SubjectsViewModels
    {
        public int SubjectId { get; set; }

        [Required]
        public string SubjectName { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string Purpose { get; set; }
        public string Requirement { get; set; }
        public string Photo { get; set; }
    }
}