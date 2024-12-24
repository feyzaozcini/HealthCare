using System;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Core.Rules.Appointments
{
    public interface IAppointmentBusinessRules : IBusinessRules
    {
        Task AppointmentDatesCannotOverlapForDoctor(Guid doktorId,DateTime startDate, DateTime endDate);
        Task AppointmentCannotCreate();
    }
}
