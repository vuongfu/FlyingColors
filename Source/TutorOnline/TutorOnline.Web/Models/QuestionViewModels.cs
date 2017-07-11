using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TutorOnline.Web.Models
{
    public class QuestionViewModels
    {
        public int QuestionId { get; set; }
        public string Photo { get; set; }

        [Required(ErrorMessage ="Hãy nhập nội dung câu hỏi.")]
        public string Content { get; set; }
        public int LessonId { get; set; }
        public int SubjectId { get; set; }
    }
}