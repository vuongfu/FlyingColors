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
        public string Content { get; set; }

        [Required]
        public int QuestionId { get; set; }
        public bool isCorrect { get; set; }
    }
}