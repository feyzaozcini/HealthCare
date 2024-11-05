using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.PatientCompanies
{
    public class PatientCompanyManager (IPatientCompanyRepository patientCompanyRepository):DomainService
    {
        public virtual async Task<PatientCompany> CreateAsync(string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name , nameof(name),PatientCompanyConsts.NameMaxLength,PatientCompanyConsts.NameMinLength);

            var patientCompany = new PatientCompany(Guid.NewGuid(), name);

            return await patientCompanyRepository.InsertAsync(patientCompany);
        }

        public virtual async Task<PatientCompany> UpdateAsync(Guid id,string name, [CanBeNull] string? concurrencyStamp = null)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), PatientCompanyConsts.NameMaxLength, PatientCompanyConsts.NameMinLength);

            var patientCompany = await patientCompanyRepository.GetAsync(id);
            patientCompany.Name = name;

            patientCompany.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await patientCompanyRepository.UpdateAsync(patientCompany);
        }
    }
}
