using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Profile_API.Domain.Abstract.IService;

namespace Profile_API.Application.Service
{
    public class EmailService : IEmailService
    {
        public async Task SendVerificationEmail(string recipientEmail, string verificationLink)
        {
            string smtpServer = "smtp.gmail.com";
            int smtpPort = 587;

            // Убедитесь, что здесь используется ваш email как отправитель
            var message = new MailMessage
            {
                From = new MailAddress("smpttest9@gmail.com"), // Ваш email
                Subject = "Email Verification",
                Body = $"Please verify your email by clicking on the following link: {verificationLink}",
                IsBodyHtml = true
            };

            // Добавьте адрес получателя
            message.To.Add(recipientEmail);

            using (var client = new SmtpClient(smtpServer, smtpPort))
            {
                client.Credentials = new NetworkCredential("smpttest9@gmail.com", "qwerty12_");
                client.EnableSsl = true;

                try
                {
                    await client.SendMailAsync(message);
                }
                catch (SmtpException ex)
                {
                    // Обработка ошибок SMTP
                    Console.WriteLine($"SMTP Exception: {ex.Message}");
                    throw;
                }
                catch (Exception ex)
                {
                    // Обработка других ошибок
                    Console.WriteLine($"Exception: {ex.Message}");
                    throw;
                }
            }
        }
    }
}
