using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Patients;
using static Pusula.Training.HealthCare.Permissions.HealthCarePermissions;

namespace Pusula.Training.HealthCare.EmailServices
{
    public class EmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;

        public EmailService(string smtpServer, int smtpPort, string smtpUser, string smtpPass)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUser = smtpUser;
            _smtpPass = smtpPass;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("HealthCare System", _smtpUser));
            message.To.Add(new MailboxAddress(to, to));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpServer, _smtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_smtpUser, _smtpPass);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        // Randevu Body Şablonu
        public string CreateAppointmentConfirmationBody(string patientName, string departmentName, DateTime startDate, DateTime endDate)
        {
            return $@"
          Merhaba {patientName} ,
          Randevunuz başarıyla oluşturulmuştur. İşte detaylar:

        - **Departman:  {departmentName}
        - **Randevu Başlangıç:  {startDate.ToString("yyyy-MM-dd HH:mm")}
        - **Randevu Bitiş:  {endDate.ToString("yyyy-MM-dd HH:mm")}"; ;
        }

        public string CreateTestResultsNotificationBody(string patientName)
        {
            return $@"
            Merhaba {patientName},
            Test sonuçlarınız hazırdır. Detaylar için doktorunuzdan randevu alınız.

            Saygılarımızla,
            halleDEViz Ekibi";
        }

    }
}
