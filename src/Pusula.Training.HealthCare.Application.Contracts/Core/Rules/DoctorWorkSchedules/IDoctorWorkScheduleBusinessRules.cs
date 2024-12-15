using System;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Core.Rules.DoctorWorkSchedules
{
    public interface IDoctorWorkScheduleBusinessRules : IBusinessRules
    {
        //Bir doktorun birden fazla çalışma saatleri olamaz
        Task DoctorWorkScheduleCannotCreate(Guid doctorId);
    }
}
