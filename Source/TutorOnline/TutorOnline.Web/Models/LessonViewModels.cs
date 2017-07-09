using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TutorOnline.Web.Models
{
    public class LessonViewModels
    {
        public int LessonId { get; set; }
        [Required]
        public string LessonName { get; set; }
        [Required]
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string Content { get; set; }
    }
}