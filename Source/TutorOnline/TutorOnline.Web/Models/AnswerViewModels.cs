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
        public string Content { get; set; }

        [Required]
        public int QuestionId { get; set; }
        public string QuesContent { get; set; }
        public string QuesPhoto { get; set; }
        public string isCorrectStr { get; set; }
        public bool isCorrect { get; set; }
    }
}