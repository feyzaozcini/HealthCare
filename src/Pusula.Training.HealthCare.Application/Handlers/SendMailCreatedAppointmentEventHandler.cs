using Microsoft.Extensions.Logging;
using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.Core.EMail;
using Pusula.Training.HealthCare.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Pusula.Training.HealthCare.Handlers
{
    public class SendMailCreatedAppointmentEventHandler(IEmailService emailService) : IDistributedEventHandler<AppointmentSendMailEto>, ITransientDependency
    {
        public async Task HandleEventAsync(AppointmentSendMailEto eventData)
        {
            var emailBody = emailService.CreateEmailBody(eventData.PatientName, eventData.DepartmentName, eventData.StartDate, eventData.EndDate);

            await emailService.SendAsync(eventData.Email, "Randevu Onayı", emailBody);

        }
    }
}
