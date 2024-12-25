using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.DiagnosisGroups
{
    public class DiagnosisGroupManager(IDiagnosisGroupRepository diagnosisGroupRepository) : DomainService
    {
        public virtual async Task<DiagnosisGroup> CreateAsync(string name, string code)
        {
            var diagnosisGroup = new DiagnosisGroup(Guid.NewGuid(), code, name);
            return await diagnosisGroupRepository.InsertAsync(diagnosisGroup);
        }

        public virtual async Task<DiagnosisGroup> UpdateAsync(Guid id, string name, string code)
        {
            var diagnosisGroup = await diagnosisGroupRepository.GetAsync(id);

            diagnosisGroup.SetName(name);
            diagnosisGroup.SetCode(code);

            return await diagnosisGroupRepository.UpdateAsync(diagnosisGroup);

        }
    }
}
