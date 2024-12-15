using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Core.Rules.AppointmentTypes
{
    public interface IAppointmentTypeBusinessRules : IBusinessRules
    {
        Task ValidateDoctorsNotAssignedToAnotherAppointmentTypeAsync(List<Guid> doctorIds, string appointmentTypeName);
    }
}
