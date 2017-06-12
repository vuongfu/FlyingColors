using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TutorOnline.Web.Models
{
    public class ApprovePretutorViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> BirthDate { get; set; }
        public int Gender { get; set; }
        public string GenderStr { get; set; }
        public string SkypeID { get; set; }
        public string Cv { get; set; }
    }
}