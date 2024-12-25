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
            var diagnosisGroup = new Diagnosis(Guid.NewGuid(), code, name, groupId);
            return await diagnosisRepository.InsertAsync(diagnosisGroup);
        }

        public virtual async Task<Diagnosis> UpdateAsync(Guid id, string name, string code,Guid groupId)
        {
          
            var diagnosis = await diagnosisRepository.GetAsync(id);

            diagnosis.SetName(name);

            diagnosis.SetCode(code);
            diagnosis.SetGroupId(groupId);

            return await diagnosisRepository.UpdateAsync(diagnosis);

        }
    }
}
