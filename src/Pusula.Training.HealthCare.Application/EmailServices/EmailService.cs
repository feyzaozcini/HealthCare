using System;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.Core.EMail;
using Volo.Abp.Emailing;

namespace Pusula.Training.HealthCare.EmailServices
{
    //Feyza Özçini ve Nil Birlik Pair Programming Yaparak Oluşturdu
    public abstract class EmailService<TModel>(IEmailSender emailSender) : IEmailService<TModel> where TModel : class
    {
        public async Task SendAsync(string to, string subject, string body) =>
            await SendAsync(default, to, subject, body);

        public async Task SendAsync(string from, string to, string subject, string body) =>
            await emailSender.SendAsync(from, to, subject, body);

        public async Task QueueAsync(string to, string subject, string body) =>
            await QueueAsync(default, to, subject, body);

        public async Task QueueAsync(string from, string to, string subject, string body) =>
            await emailSender.QueueAsync(from, to, subject, body);

        public abstract string GenerateEmailBody(TModel model);

    }
}
