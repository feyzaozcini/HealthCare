using Pusula.Training.HealthCare.Core.EMail;
using Pusula.Training.HealthCare.TestProcesses;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Pusula.Training.HealthCare.Handlers
{
    public class SendMailNotifyTestProcessEventHandler(ITestProcessEmailService<TestProcessEmailDto> emailService) : IDistributedEventHandler<TestProcessSendMailEto>, ITransientDependency
    {
        public async Task HandleEventAsync(TestProcessSendMailEto eventData)
        {
            var emailBody = emailService.GenerateEmailBody(new TestProcessEmailDto
            {
                PatientName = eventData.PatientName
            });
            await emailService.SendAsync(eventData.Email, "Test Sonuçlarınız Hazır", emailBody);
        }
    }
}
