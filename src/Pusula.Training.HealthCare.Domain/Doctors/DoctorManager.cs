using Pusula.Training.HealthCare.Patients;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Doctors
{
    public class DoctorManager(IDoctorRepository doctorRepository) : DomainService
    {
        public virtual async Task<Doctor> CreateAsync(
        Guid userId,
        Guid titleId,
        string identityNumber,
        DateTime birthDate,
        Gender gender)
        {
            Check.NotNull(birthDate, nameof(birthDate));
            Check.NotNull(gender, nameof(gender));
            Check.NotNull(userId, nameof(userId));
            Check.NotNull(titleId, nameof(titleId));
            Check.Length(identityNumber, nameof(identityNumber), DoctorConsts.IdentityNumberMaxLength);
            Check.NotNull(identityNumber, nameof(identityNumber));


            var doctor = new Doctor(GuidGenerator.Create(), userId, birthDate, gender, titleId, identityNumber);

                return await doctorRepository.InsertAsync(doctor);

        }

        public virtual async Task<Doctor> UpdateAsync(
        Guid id,
        Guid titleId,
        string identityNumber,
        DateTime birthDate,
        Gender gender)
        {

            Check.NotNull(birthDate, nameof(birthDate));
            Check.NotNull(gender, nameof(gender));
            Check.NotNull(titleId, nameof(titleId));
            Check.Length(identityNumber, nameof(identityNumber), DoctorConsts.IdentityNumberMaxLength);
            Check.NotNull(identityNumber, nameof(identityNumber));

            var doctor = await doctorRepository.GetAsync(id);

            
            doctor.TitleId = titleId;
            doctor.SetBirthDate(birthDate);
            doctor.SetGender(gender);
            doctor.SetIdentityNumber(identityNumber);
            return await doctorRepository.UpdateAsync(doctor);
        }
    }
}

