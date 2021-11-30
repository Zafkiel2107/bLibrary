using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Security;
using System.Threading.Tasks;

namespace bLibrary.App_Start
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            string from = ConfigurationManager.AppSettings["From"];
            SecureString password = CreateSecureString(ConfigurationManager.AppSettings["Password"]);
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(from, password),
                EnableSsl = true
            };

            MailMessage mailMessage = new MailMessage(from, message.Destination)
            {
                Subject = message.Subject,
                Body = message.Body,
                IsBodyHtml = true
            };
            return smtpClient.SendMailAsync(mailMessage);
        }
        private SecureString CreateSecureString(string password)
        {
            SecureString secureString = new SecureString();
            foreach(char s in password)
            {
                secureString.AppendChar(s);
            }
            return secureString;
        }
    }
}
