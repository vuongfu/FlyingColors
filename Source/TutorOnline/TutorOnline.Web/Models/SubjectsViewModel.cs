﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TutorOnline.Web.Models
{
    public class SubjectsViewModel
    {
        public int Id { get; set; }

        [Required]
        public string SubjectName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public double Duration { get; set; }
        public string Purpose { get; set; }
        public string Requirement { get; set; }
        public byte[] Photo { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}