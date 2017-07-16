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

        [Required(ErrorMessage = "Hãy nhập tên bài học.")]
        public string LessonName { get; set; }

        [Required(ErrorMessage = "Hãy chọn khóa học cho bài học.")]
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string Content { get; set; }
    }
}