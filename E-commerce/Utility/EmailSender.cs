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
                EnableSsl = true, // to encrypt data
                UseDefaultCredentials = false, // to not use default credentials
                Credentials = new NetworkCredential("ahmeeeed02375@gmail.com\r\n", "Asdfghjkl2&248") // put your email and password here to send email to users
            };
            return client.SendMailAsync(
                new MailMessage(from: "your.email@live.com", // change it to your email
                to: email,// user email
                subject,// subject
                htmlMessage // body of the email
                )
                {
                    IsBodyHtml = true // to support html in the body
                });



        }
    }

}

