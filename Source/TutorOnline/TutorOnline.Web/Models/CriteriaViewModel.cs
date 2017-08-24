using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TutorOnline.DataAccess;
using System.ComponentModel.DataAnnotations;

namespace TutorOnline.Web.Models
{
    public class CriteriaViewModel
    {
        public int CriteriaId { get; set; }

        [Required (ErrorMessage = "Hãy nhập nội dung tiêu chí đánh giá.")]
        [DataType(DataType.MultilineText)]
        [StringLength(255, ErrorMessage = "{0} chứa tối đa {1} ký tự.")]
        public string CriteriaName { get; set; }
        public int LessonId { get; set; }
        public int RoleId { get; set; }
        public string LessonName { get; set; }
    }
}