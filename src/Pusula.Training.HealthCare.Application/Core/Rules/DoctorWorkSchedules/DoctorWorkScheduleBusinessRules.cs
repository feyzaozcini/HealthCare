using Pusula.Training.HealthCare.DoctorWorkSchedules;
using Pusula.Training.HealthCare.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Core.Rules.DoctorWorkSchedules
{
    public class DoctorWorkScheduleBusinessRules(IDoctorWorkScheduleRepository doctorWorkScheduleRepository) : IDoctorWorkScheduleBusinessRules
    {
        public async Task DoctorWorkScheduleCannotCreate(Guid doctorId)
        {
            var doctorWorkSchedule = await doctorWorkScheduleRepository.GetWorkScheduleForDoctorAsync(doctorId);

            if (doctorWorkSchedule.Any())
            {
                HealthCareException.ThrowIf(
                    HealthCareDomainErrorCodes.DoctorWorkScheduleConflict
                );
            }
        }
    }
}
