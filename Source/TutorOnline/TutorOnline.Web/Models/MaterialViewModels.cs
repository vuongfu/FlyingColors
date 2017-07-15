using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TutorOnline.Web.Models
{
    public class MaterialViewModels
    {
        public int MaterialId { get; set; }

        [Required]
        public string MaterialUrl { get; set; }
        public int MaterialTypeId { get; set; }
        public string MaterialTypeName { get; set; }
        public string Description { get; set; }

        [Required]
        public int? LessonId { get; set; }

        [Required]
        public int? SubjectId { get; set; }
    }
}