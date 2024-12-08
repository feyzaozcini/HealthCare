using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Core.Rules.Appointments
{
    public interface IAppointmentBusinessRules : IBusinessRules
    {
        Task AppointmentDatesCannotOverlapForDoctor(Guid doktorId,DateTime startDate, DateTime endDate);
        Task AppointmentCannotCreate();
    }
}
