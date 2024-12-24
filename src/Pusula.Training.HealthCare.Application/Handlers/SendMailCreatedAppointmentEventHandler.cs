using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.Core.EMail;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Pusula.Training.HealthCare.Handlers
{
    public class SendMailCreatedAppointmentEventHandler(IAppointmentEmailService<AppointmentEmailDto> emailService) : IDistributedEventHandler<AppointmentSendMailEto>, ITransientDependency
    {
        public async Task HandleEventAsync(AppointmentSendMailEto eventData)
        {
            var emailBody = emailService.GenerateEmailBody(new AppointmentEmailDto
            {
                DepartmentName = eventData.DepartmentName,
                EndDate = eventData.EndDate,
                PatientName = eventData.PatientName,
                StartDate = eventData.StartDate
            });
            await emailService.SendAsync(eventData.Email, "Randevu Onayı", emailBody);
        }
    }
}
