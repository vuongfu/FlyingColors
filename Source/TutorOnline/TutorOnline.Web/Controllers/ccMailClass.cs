using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace TutorOnline.Web.Controllers
{
    public static class ccMailClass
    {
        public static bool sendMail_UseGmail(string strFrom, string strPass, string strTo, string strSubject, string strBody)
        {
            MailMessage ms = new MailMessage(strFrom, strTo, strSubject, strBody);

            ms.BodyEncoding = System.Text.Encoding.UTF8;
            ms.SubjectEncoding = System.Text.Encoding.UTF8;
            ms.IsBodyHtml = true;

#pragma warning disable CS0618 // 'MailMessage.ReplyTo' is obsolete: 'ReplyTo is obsoleted for this type.  Please use ReplyToList instead which can accept multiple addresses. http://go.microsoft.com/fwlink/?linkid=14202'
            ms.ReplyTo = new MailAddress(strFrom);
#pragma warning restore CS0618 // 'MailMessage.ReplyTo' is obsolete: 'ReplyTo is obsoleted for this type.  Please use ReplyToList instead which can accept multiple addresses. http://go.microsoft.com/fwlink/?linkid=14202'
            ms.Sender = new MailAddress(strFrom);

            SmtpClient SmtpMail = new SmtpClient("smtp.gmail.com", 587);
            SmtpMail.Credentials = new NetworkCredential(strFrom, strPass);
            SmtpMail.EnableSsl = true;

            try
            {
                SmtpMail.Send(ms);
                return true;
            }
            catch
            {
                return false;
            }


        }
    }
}