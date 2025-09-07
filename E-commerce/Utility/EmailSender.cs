using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace E_commerce.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // send email to users
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("ahmeeeed02375@gmail.com\r\n", "Asdfghjkl2&248")
            };
            // put your email and password here to send email
            return client.SendMailAsync(
                new MailMessage(from: "your.email@live.com",
                to: email,
                subject,
                htmlMessage
                )
                {
                    IsBodyHtml = true
                });



        }
    }

}

