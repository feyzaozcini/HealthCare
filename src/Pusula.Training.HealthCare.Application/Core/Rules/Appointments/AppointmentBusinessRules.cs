using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.Exceptions;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Core.Rules.Appointments
{
    public class AppointmentBusinessRules(IAppointmentRepository appointmentRepository) : IAppointmentBusinessRules
    {
        public async Task AppointmentCannotCreate() => HealthCareException.ThrowIf(HealthCareDomainErrorCodes.DoctorRules);
        
        //Doktora ait randevuların tarihleri çakışamaz
        public async Task AppointmentDatesCannotOverlapForDoctor(Guid doctorId, DateTime startDate, DateTime endDate)
        {
            var overlappingAppointment = await appointmentRepository.FirstOrDefaultAsync(a =>
                a.DoctorId == doctorId &&
                a.StartDate < endDate && // EndDate'den önce başlayan randevular
                a.EndDate > startDate);  // StartDate'den sonra biten randevular

            HealthCareException.ThrowIf(
                HealthCareDomainErrorCodes.DoctorScheduleConflict,
                overlappingAppointment is not null);
        }
    }
   
}
