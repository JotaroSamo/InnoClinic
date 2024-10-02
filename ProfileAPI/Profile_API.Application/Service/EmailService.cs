using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Profile_API.Domain.Abstract.IService;
using Profile_API.Domain.Models;

namespace Profile_API.Application.Service
{
    public class EmailService : IEmailService
    {
        private const string SENDER_EMAIL = "smttest9@mail.ru"; // Ваш email
        private const string PASSWORD = "AbTPE2e14aV1ynPiE09m"; // Пароль от email
        private const string SMTP_SERVER = "smtp.mail.ru";
        private const int SMTP_PORT = 587;

        public async Task SendCredentialsToEmail(string email)
        {
            MailAddress from = new MailAddress(SENDER_EMAIL, "InnoClinic");
            MailAddress to = new MailAddress(email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "InnoClinic account confirmation";
            m.IsBodyHtml = true;
            m.Body = $"<h3>Hello, dear user!</h3>" +
                    $"<p>Congratulations! Now you have an account.<br>" +
                    $"Your credentials:</p>" +
                    $"<h3>email: {email}<br/>"+
                    $"Thanks!";

            using (SmtpClient smtp = new SmtpClient(SMTP_SERVER, SMTP_PORT))
            {
                smtp.Credentials = new NetworkCredential(SENDER_EMAIL, PASSWORD);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(m);
            }
        }

        public async Task SendConfirmationLink(string email, Guid accountId)
        {
            MailAddress from = new MailAddress(SENDER_EMAIL, "InnoClinic");
            MailAddress to = new MailAddress(email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "InnoClinic account confirmation";
            m.IsBodyHtml = true;
            m.Body = $"<h3>Hello, dear user!</h3>" +
                    $"<p>Congratulations! Now you have an account.<br>" +
                    $"To confirm your email follow this: " +
                    $"<a target=\"_self\" href=\"https://localhost:7246/api/account/verify-email?userId={accountId}\">link</a></p>" +
                    $"Thanks!";
            using (SmtpClient smtp = new SmtpClient(SMTP_SERVER, SMTP_PORT))
            {
                smtp.Credentials = new NetworkCredential(SENDER_EMAIL, PASSWORD);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(m);
            }
        }
    }
}
