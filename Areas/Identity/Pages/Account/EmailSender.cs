using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace SportClubs1.Areas.Identity.Pages.Account
{


    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Ваша логіка надсилання електронного листа
            using (var message = new MailMessage())
            {
                message.To.Add(new MailAddress(email));
                message.Subject = subject;
                message.Body = htmlMessage;
                message.IsBodyHtml = true;

                using (var client = new SmtpClient("smtp.gmail.com"))
                {
                    client.Send(message);
                }
            }

            return Task.CompletedTask;
        }
    }

}
