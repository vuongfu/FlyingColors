using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TutorOnline.Web.Models
{
    public class CategoriesViewModels
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Hãy nhập tên môn học.")]
        [StringLength(100, ErrorMessage = "{0} chứa tối đa {1} ký tự.")]
        public string CategoryName { get; set; }
        [DataType(DataType.MultilineText)]
        [StringLength(255, ErrorMessage = "{0} chứa tối đa {1} ký tự.")]
        public string Description { get; set; }
    }
}