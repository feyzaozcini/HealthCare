using Pusula.Training.HealthCare.DiagnosisGroups;
using Pusula.Training.HealthCare.Doctors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Diagnoses
{
    public class DiagnosisManager(IDiagnosisRepository diagnosisRepository) : DomainService
    {
        public virtual async Task<Diagnosis> CreateAsync(string name, string code,Guid groupId)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), DiagnosisConsts.CodeMaxLength, DiagnosisConsts.CodeMinLength);

            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), DiagnosisConsts.NameMaxLength, DiagnosisConsts.NameMinLength);

            var diagnosisGroup = new Diagnosis(Guid.NewGuid(), code, name, groupId);
            return await diagnosisRepository.InsertAsync(diagnosisGroup);

        }

        public virtual async Task<Diagnosis> UpdateAsync(Guid id, string name, string code,Guid groupId)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), DiagnosisConsts.CodeMaxLength, DiagnosisConsts.CodeMinLength);

            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), DiagnosisConsts.NameMaxLength, DiagnosisConsts.NameMinLength);

            var diagnosis = await diagnosisRepository.GetAsync(id);

            diagnosis.Name = name;
            diagnosis.Code = code;
            diagnosis.GroupId = groupId;

            return await diagnosisRepository.UpdateAsync(diagnosis);



        }
    }
}
