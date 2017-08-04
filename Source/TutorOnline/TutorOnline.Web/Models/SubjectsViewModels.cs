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

        [Required(ErrorMessage = "Hãy nhập tên khóa học.")]
        public string SubjectName { get; set; }
        [Required(ErrorMessage = "Hãy chọn môn học cho khóa học.")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.MultilineText)]
        public string Purpose { get; set; }

        [DataType(DataType.MultilineText)]
        public string Requirement { get; set; }
        public string Photo { get; set; }
    }
}