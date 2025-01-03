using Pusula.Training.HealthCare.AppointmentRules;
using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.Exceptions;
using Pusula.Training.HealthCare.Patients;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Core.Rules.Appointments
{
    public class AppointmentBusinessRules(IAppointmentRepository appointmentRepository,
        IPatientRepository patientRepository,
        IAppointmentRuleRepository appointmentRuleRepository) : IAppointmentBusinessRules
    {
        //Doktorların kuralları için eklendi
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

        //Takvim Template'i bazen geçmiş tarihli randevu oluşturabilir (aynı hafta içerisinde) bunu engellemek için eklendi 
        public async Task AppointmentCannotCreatePastTime(DateTime startDate)
        {
            HealthCareException.ThrowIf(HealthCareDomainErrorCodes.PastTimeAppointmentCannotCreate,
                startDate < DateTime.Now);
        }

        public async Task CheckRulesAsync(AppointmentCreateDto input)
        {
            var patient = await patientRepository.GetAsync(input.PatientId);
            var patientAge = DateTime.Now.Year - patient.BirthDate.Year;
            var patientGender = patient.Gender.ToString();

            //seçilen departman ve doktor için tanımlanmış kuralların getirilmesi(Hepsi gezilmiyor sadece randevu için seçilenler)
            var departmentRules = await appointmentRuleRepository.GetRulesForDepartmentAsync(input.DepartmentId);
            var doctorRules = await appointmentRuleRepository.GetRulesForDoctorAsync(input.DoctorId);
            var allRules = departmentRules.Concat(doctorRules).ToList();


            foreach (var rule in allRules.Where(r =>
              (r.MinAge.HasValue && patientAge < r.MinAge) ||
              (r.MaxAge.HasValue && patientAge > r.MaxAge) ||
              (r.Gender.HasValue && patientGender != r.Gender.ToString())))
            {
                await AppointmentCannotCreate();
            }

        }
    }

}
