using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.Core.EMail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Emailing;

namespace Pusula.Training.HealthCare.EmailServices
{
    public class AppointmentEmailService(IEmailSender emailSender) : EmailService<AppointmentEmailDto>(emailSender), IAppointmentEmailService<AppointmentEmailDto>
    {
        public override string GenerateEmailBody(AppointmentEmailDto model)=> $@"<h1>Randevu Onayı</h1>
            <p>Merhaba {model.PatientName},</p>
            <p>Randevunuz başarıyla oluşturulmuştur.</p>
            <p>Randevu Detayları:</p>
            <p>Departman: {model.DepartmentName}</p>
            <p>Başlangıç Tarihi: {model.StartDate}</p>
            <p>Bitiş Tarihi: {model.EndDate}</p>";
    }
}
