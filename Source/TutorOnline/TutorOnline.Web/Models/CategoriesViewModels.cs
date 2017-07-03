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

        [Required]
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}