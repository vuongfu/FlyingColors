using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace TutorOnline.Web.Models
{
    public class AnswerViewModels
    {
        public int AnswerId { get; set; }

        [Required (ErrorMessage = "Hãy nhập nội dung câu trả lời.")]
        [StringLength(500, ErrorMessage = "{0} chứa tối đa {1} ký tự.")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Required]
        public int QuestionId { get; set; }
        [DataType(DataType.MultilineText)]
        public string QuesContent { get; set; }
        public string QuesPhoto { get; set; }
        public string isCorrectStr { get; set; }
        public bool isCorrect { get; set; }
    }

    public partial class StudentTestAnswerViewModels
    {
        public List<int> ListAnswer { get; set; }
        public int LessonId { get; set; }
    }
}