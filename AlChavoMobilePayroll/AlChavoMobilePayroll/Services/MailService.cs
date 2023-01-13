using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace AlChavoMobilePayroll.Services
{
    public class MailService
    {

        public void SendEmail(string Subject, string Body, string Recipient)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("mail.smtp2go.com");

            mail.From = new MailAddress("noreply@alchavo.com");
            mail.To.Add(Recipient);
            mail.Subject = Subject;
            mail.Body = Body;

            SmtpServer.Port = 2525;
            SmtpServer.Host = "mail.smtp2go.com";
            SmtpServer.EnableSsl = true;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("noreply@alchavo.com", "Alchavo1");

            SmtpServer.Send(mail);

        }
    }
}
