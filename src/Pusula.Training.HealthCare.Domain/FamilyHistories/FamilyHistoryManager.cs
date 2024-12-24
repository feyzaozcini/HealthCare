using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.FamilyHistories
{
    public class FamilyHistoryManager(IFamilyHistoryRepository familyHistoryRepository) : DomainService
    {
        public virtual async Task<FamilyHistory> CreateAsync(Guid patientId, string? mother, string? father, string? sister, string? brother, string? other, bool isParentsRelative)
        {
            var familyHistory = new FamilyHistory(GuidGenerator.Create(), patientId, mother, father, sister, brother,
                other, isParentsRelative);

            return await familyHistoryRepository.InsertAsync(familyHistory);
        }

        public virtual async Task<FamilyHistory> UpdateAsync(Guid id,Guid patientId, string? mother, string? father, string? sister, string? brother, string? other, bool isParentsRelative)
        {
            var familyHistory = await familyHistoryRepository.GetAsync(id);
            familyHistory.SetPatientId(patientId);
            familyHistory.SetMother(mother);
            familyHistory.SetFather(father);
            familyHistory.SetSister(sister);
            familyHistory.SetBrother(brother);
            familyHistory.SetOther(other);
            familyHistory.SetIsParentsRelative(isParentsRelative);

            return await familyHistoryRepository.UpdateAsync(familyHistory);
        }
    }
}
