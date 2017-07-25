using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TutorOnline.DataAccess;
namespace TutorOnline.Web.Models
{
    public class StudentSubjectViewModels
    {
        public int StudentSubjectId { get; set; }
        public int SubjectId { get; set; }
        public int StudentId { get; set; }
        public int Status { get; set; }

        public virtual Status Status1 { get; set; }
        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }
    }
    public class SubjectDetailViewModels
    {
        public virtual Student Student { get; set; }
        public int SubjectId { get; set; }
        [Required]
        public string SubjectName { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string Purpose { get; set; }
        public string Requirement { get; set; }
        public string Photo { get; set; }
        public List<Lesson> ListLesson { get; set; }
        public int StudiedLesson { get; set; }
    }
}