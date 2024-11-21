using Pusula.Training.HealthCare.Titles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.DiagnosisGroups
{
    public class DiagnosisGroupManager(IDiagnosisGroupRepository diagnosisGroupRepository):DomainService
    {
        public virtual async Task<DiagnosisGroup> CreateAsync(string name, string code)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), DiagnosisGroupConsts.CodeMaxLength, DiagnosisGroupConsts.CodeMinLength);

            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), DiagnosisGroupConsts.NameMaxLength, DiagnosisGroupConsts.NameMinLength);

            var diagnosisGroup = new DiagnosisGroup(Guid.NewGuid(), code, name);
            return await diagnosisGroupRepository.InsertAsync(diagnosisGroup);

        }

        public virtual async Task<DiagnosisGroup> UpdateAsync(Guid id, string name,string code)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), DiagnosisGroupConsts.CodeMaxLength, DiagnosisGroupConsts.CodeMinLength);

            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), DiagnosisGroupConsts.NameMaxLength, DiagnosisGroupConsts.NameMinLength);

            var diagnosisGroup = await diagnosisGroupRepository.GetAsync(id);

            diagnosisGroup.Name = name;
            diagnosisGroup.Code = code;

            return await diagnosisGroupRepository.UpdateAsync(diagnosisGroup);

         

        }
    }
}
