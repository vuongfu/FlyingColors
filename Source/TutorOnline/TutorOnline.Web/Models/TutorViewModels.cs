using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TutorOnline.Web.Models
{
    public class TutorBookSlotViewModel
    {
        public int TutorId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderSlot { get; set; }
    }
}