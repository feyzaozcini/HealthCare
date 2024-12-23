using System;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.Core.EMail;
using Volo.Abp.Emailing;

namespace Pusula.Training.HealthCare.EmailServices
{
    //Feyza Özçini ve Nil Birlik Pair Programming Yaparak Oluşturdu
    public class EmailService(IEmailSender emailSender) : IEmailService
    {
        public async Task SendAsync(string to, string subject, string body) =>
            await SendAsync(default,to, subject, body); 

        public async Task SendAsync(string from, string to, string subject, string body) =>
            await emailSender.SendAsync(from, to, subject, body);

        public async Task QueueAsync(string to, string subject, string body) =>
            await QueueAsync(default,to, subject, body);

        public async Task QueueAsync(string from,string to, string subject, string body) =>
            await emailSender.QueueAsync(from,to, subject, body);


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

        //test sonuçları testresultemail service onun kendi servsinde o alan olur 
        public string CreateTestResultsNotificationBody(string patientName)
        {
            return $@"
            Merhaba {patientName},
            Test sonuçlarınız hazırdır. Detaylar için doktorunuzdan randevu alınız.

            Saygılarımızla,
            halleDEViz Ekibi";
        }

        public string CreateEmailBody(params object[] args)
        {
            return $@"
          Merhaba {args[0]} ,
          Randevunuz başarıyla oluşturulmuştur. İşte detaylar:

        - **Departman:  {args[1]}
        - **Randevu Başlangıç:  {args[2]}
        - **Randevu Bitiş:  {args[3]}"; ;
        }
    }
}
