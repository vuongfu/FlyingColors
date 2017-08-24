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
        [StringLength(500, ErrorMessage = "{0} chứa tối đa {1} ký tự.")]
        public string LessonName { get; set; }

        [Required(ErrorMessage = "Hãy chọn khóa học cho bài học.")]
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        [Required(ErrorMessage = "Hãy nhập nội dung cho bài học.")]
        [DataType(DataType.MultilineText)]
        [StringLength(1000, ErrorMessage = "{0} chứa tối đa {1} ký tự.")]
        public string Content { get; set; }
        public int Order { get; set; }
    }
}