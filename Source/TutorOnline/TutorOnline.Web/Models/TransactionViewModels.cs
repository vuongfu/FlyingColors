using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TutorOnline.DataAccess;

namespace TutorOnline.Web.Models
{
    public class TransactionViewModels
    {
        public int Id { get; set; }
        public Nullable<int> UserID { get; set; }
        public string Content { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime TranDate { get; set; }

        public virtual User User { get; set; }
    }
}