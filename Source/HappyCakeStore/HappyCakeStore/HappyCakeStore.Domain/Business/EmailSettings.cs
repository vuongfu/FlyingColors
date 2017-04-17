using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyCakeStore.Domain.Business
{
    public class EmailSettings
    {
        public string MailToAddress = "vuongfu95@gmail.com";
        public string MailFromAddress = "vuongfu95@gmail.com";
        public bool UseSsl = true;
        public string Username = "vuongfu95@gmail.com";
        public string Password = "vuongtv12345";
        public string ServerName = "smtp.gmail.com";
        public int ServerPort = 587;
    }
}
