using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.AppointmentRules
{
    public class AppointmentRuleManager(IAppointmentRuleRepository appointmentRuleRepository) : DomainService
    {
        public virtual async Task<AppointmentRule> CreateAsync(
            Guid? doctorId, 
            Guid? departmentId, 
            Gender? gender, 
            int? age, 
            int? minAge,
            int? maxAge,
            string? description)
        {
            var appointmentRule = new AppointmentRule(
                GuidGenerator.Create(),
                doctorId,
                departmentId,
                gender,
                age,
                minAge,
                maxAge,
                description
            );

            return await appointmentRuleRepository.InsertAsync(appointmentRule);
        }


        public virtual async Task<AppointmentRule> UpdateAsync(
            Guid id,
            Guid? doctorId,
            Guid? departmentId,
            Gender? gender,
            int? age,
            int? minAge,
            int? maxAge,
            string? description
        )
        {
            var appointmentRule = await appointmentRuleRepository.GetAsync(id);
            appointmentRule.SetDoctorId(doctorId);
            appointmentRule.SetDepartmentId(departmentId);
            appointmentRule.SetGender(gender);
            appointmentRule.SetAge(age);
            appointmentRule.SetMinAge(minAge);
            appointmentRule.SetMaxAge(maxAge);
            appointmentRule.SetDescription(description);

            return await appointmentRuleRepository.UpdateAsync(appointmentRule);
        }
    }
 }

