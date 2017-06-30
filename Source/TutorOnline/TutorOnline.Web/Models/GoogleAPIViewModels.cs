using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TutorOnline.Web.Models
{
    public class GoogleProfile
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public ImageProfile Image { get; set; }
        public List<Email> Emails { get; set; }
        public string Gender { get; set; }
        public string ObjectType { get; set; }
    }
    public class Email
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }
    public class ImageProfile
    {
        public string Url { get; set; }
    }
}