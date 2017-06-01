using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutorOnline.Core.Entity;

namespace TutorOnline.Web.Models
{
    public class CourseViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int CourseID { get; set; }

        [Required(ErrorMessage = "Course name is required!")]
        public string CourseName { get; set; }

        public string Title { get; set; }

        public int SubjectID { get; set; }

        public string SubjectName { get; set; }

        [Required(ErrorMessage = "Start date is required!")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required!")]
        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public string Purpose { get; set; }

        public string Requirement { get; set; }

        public string Photo { get; set; }

        [Required(ErrorMessage = "Price is required!")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price.")]
        public Decimal Price { get; set; }

        public IEnumerable<SelectListItem> ListSub { get; set; }
    }
}