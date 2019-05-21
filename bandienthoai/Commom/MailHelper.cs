using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Commom
{
   public class MailHelper
    {
        public void SenMail(string toEmailAddresss, string subject, string content)
        {
            var fromEmailAddresss = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            var fromEmailDisplayName = ConfigurationManager.AppSettings["fromEmailDisplayName"].ToString();
            var fromEmailPassword = ConfigurationManager.AppSettings["fromEmailPassword"].ToString();
            var FromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();
            
            bool enableSsl=bool.Parse(ConfigurationManager.AppSettings["EnableSSL"].ToString());
            string body = content;
            MailMessage message = new MailMessage(new MailAddress(fromEmailAddresss, fromEmailDisplayName), new MailAddress(toEmailAddresss));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;
            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(fromEmailAddresss, fromEmailPassword);
            client.Host = smtpHost;
            client.EnableSsl = enableSsl;
            client.Port = !string.IsNullOrEmpty(smtpPort)?Convert.ToInt32(smtpPort):0;
            client.Send(message);

        }
    }
}
