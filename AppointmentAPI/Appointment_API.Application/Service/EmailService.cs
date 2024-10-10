﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Appointment_API.Domain.Model;
using CSharpFunctionalExtensions;


namespace Profile_API.Application.Service
{
    public class EmailService : IEmailService
    {
        private const string SENDER_EMAIL = "smttest9@mail.ru"; // Ваш email
        private const string PASSWORD = "AbTPE2e14aV1ynPiE09m"; // Пароль от email
        private const string SMTP_SERVER = "smtp.mail.ru";
        private const int SMTP_PORT = 587;

        public async Task SendNotificationAboutAppointmentToEmail(Appointment appointment)
        {
            MailAddress from = new MailAddress(SENDER_EMAIL, "InnoClinic");
            MailAddress to = new MailAddress(appointment.Patient.Patient_Email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Reminder: Upcoming Appointment at InnoClinic";
            m.IsBodyHtml = true;
            m.Body = $"<h3>Hello, dear {appointment.Patient.Patient_Name}!</h3>" +
                     $"<p>This is a reminder about your upcoming appointment at InnoClinic.</p>" +
                     $"<p><strong>Details of your appointment:</strong><br>" +
                     $"<b>Date:</b> {appointment.Date.ToString("dd MMMM yyyy")}<br>" +
                     $"<b>Time:</b> {appointment.Time.ToString("hh\\:mm")}<br>" +
                     $"<b>Doctor:</b> {appointment.Doctor.Doctro_Name}<br>" +
                     $"<b>Service:</b> {appointment.Service.Service_Name}</p>" +
                     $"<p>Please make sure to arrive on time. If you have any questions or need to reschedule, feel free to contact us.</p>" +
                     $"<p>Thank you for choosing InnoClinic!</p>";

            using (SmtpClient smtp = new SmtpClient(SMTP_SERVER, SMTP_PORT))
            {
                smtp.Credentials = new NetworkCredential(SENDER_EMAIL, PASSWORD);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(m);
            }
        }


        public async Task SendAppointmentResultOnEmail(Results result)
        {
            MailAddress from = new MailAddress(SENDER_EMAIL, "InnoClinic");
            MailAddress to = new MailAddress(result.Appointment.Patient.Patient_Email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Your Appointment Results from InnoClinic";
            m.IsBodyHtml = true;
            m.Body = $"<h3>Hello, dear {result.Appointment.Patient.Patient_Name}!</h3>" +
                     $"<p>We hope you're well. Here are the results from your recent appointment:</p>" +
                     $"<p><strong>Complaints:</strong> {result.Complaints}</p>" +
                     $"<p><strong>Conclusion:</strong> {result.Conclusion}</p>" +
                     $"<p><strong>Recommendations:</strong> {result.Recommendations}</p>" +
                     $"<p>If you have any questions or need further assistance, please don't hesitate to reach out to us.</p>" +
                     $"<p>Thank you for choosing InnoClinic. Take care!</p>";

            using (SmtpClient smtp = new SmtpClient(SMTP_SERVER, SMTP_PORT))
            {
                smtp.Credentials = new NetworkCredential(SENDER_EMAIL, PASSWORD);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(m);
            }
        }

    }
}