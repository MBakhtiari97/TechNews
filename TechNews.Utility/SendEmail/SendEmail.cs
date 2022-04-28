using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using Microsoft.Extensions.Configuration;

namespace TechNews.Utility
{
    public class SendEmail
    {
        public static void Send(string To,string Subject,string Body)
        {

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("onlineshoptestt123@gmail.com", "VenusDigital");
            mail.To.Add(To);
            mail.Subject = Subject;
            mail.Body = Body;
            mail.IsBodyHtml = true;

            //System.Net.Mail.Attachment attachment;
            // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            // mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("onlineshoptestt123@gmail.com", "********");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        }
    }
}