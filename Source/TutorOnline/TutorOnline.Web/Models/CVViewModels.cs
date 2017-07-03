using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TutorOnline.Web.Models
{
    public class CVViewModels
    {
        public int CVId { get; set; }
        public string CVLink { get; set; }
        public bool isRead { get; set; }
        public bool isApproved { get; set; }
    }
}