using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TutorOnline.DataAccess;
using System.ComponentModel.DataAnnotations;

namespace TutorOnline.Web.Models
{
    public class QuestionViewModels
    {
        public int QuestionId { get; set; }
        public string Photo { get; set; }

        [Required(ErrorMessage ="Hãy nhập nội dung câu hỏi.")]
        public string Content { get; set; }
        public Nullable<int> LessonId { get; set; }
        public string lessonName { get; set; }
        public Nullable<int> SubjectId { get; set; }
    }
    public class QuestionTestViewModels
    {
        public int QuestionId { get; set; }
        public string Photo { get; set; }

        [Required(ErrorMessage = "Hãy nhập nội dung câu hỏi.")]
        public string Content { get; set; }
        public Nullable<int> LessonId { get; set; }
        public Nullable<int> SubjectId { get; set; }
        public List<Answer> ListAnswer { get; set; }
    }
}