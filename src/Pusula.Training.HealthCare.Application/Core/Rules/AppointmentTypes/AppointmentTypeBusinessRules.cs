using Pusula.Training.HealthCare.Exceptions;
using Pusula.Training.HealthCare.AppointmentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Core.Rules.AppointmentTypes
{
    public class AppointmentTypeBusinessRules(IAppointmentTypeRepository appointmentTypeRepository) : IAppointmentTypeBusinessRules
    {
        public async Task ValidateDoctorsNotAssignedToAnotherAppointmentTypeAsync(List<Guid> doctorIds, string appointmentTypeName)
        {
            var existingAppointmentTypes= await appointmentTypeRepository.GetAppointmentTypesByDoctorIdsAsync(doctorIds);

            // Aynı isimde AppointmentType'a atanmış doktorları kontrol et
            var conflictingDoctors = existingAppointmentTypes
                .Where(at => at.Name == appointmentTypeName)
                .Select(at => at.DoctorAppointmentTypes)
                .ToList();

            // Eğer conflict varsa, hata fırlat
            if (conflictingDoctors.Any())
            {
                HealthCareException.ThrowIf(
                    HealthCareDomainErrorCodes.DoctorAlreadyAssignedToAppointmentType
                );
            }
        }
    }

}
