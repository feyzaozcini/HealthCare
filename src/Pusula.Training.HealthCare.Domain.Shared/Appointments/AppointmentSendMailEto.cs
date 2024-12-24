using System;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace Pusula.Training.HealthCare.Appointments
{
    // veri transfer nesnesi üzerinden randevu bilgilerini mail olarak göndermek için kullanılır
    public class AppointmentSendMailEto : EtoBase
    {
        public string Email { get; set; }
        public string PatientName { get; set; }
        public string DepartmentName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
