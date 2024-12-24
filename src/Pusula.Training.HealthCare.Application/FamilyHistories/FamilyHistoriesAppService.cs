using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Pusula.Training.HealthCare.FamilyHistories
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.Examinations.Default)]
    public class FamilyHistoriesAppService(
    IFamilyHistoryRepository familyHistoryRepository, FamilyHistoryManager familyHistoryManager)
    : HealthCareAppService, IFamilyHistoriesAppService
    {

        [Authorize(HealthCarePermissions.Examinations.Create)]
        public async Task<FamilyHistoryDto> CreateAsync(FamilyHistoryCreateDto input)
        {
            var familyHistory = await familyHistoryManager.CreateAsync(input.PatientId,input.Mother,input.Father,
                input.Sister,input.Brother,input.Other,input.IsParentsRelative);

            return ObjectMapper.Map<FamilyHistory, FamilyHistoryDto>(familyHistory);
        }

        [Authorize(HealthCarePermissions.Examinations.Delete)]

        public async Task DeleteAsync(Guid id) => await familyHistoryRepository.DeleteAsync(id);

        public async Task<FamilyHistoryDto> GetAsync(Guid id) =>
            ObjectMapper.Map<FamilyHistory, FamilyHistoryDto>(await familyHistoryRepository.GetAsync(id));

        public async Task<FamilyHistoryDto> GetByPatientIdAsync(Guid patientId)
        {
            var familyHistory = await familyHistoryRepository.GetByPatientIdAsync(patientId);

            // Eğer kayıt yoksa doğrudan null dönecek
            return familyHistory != null
                ? ObjectMapper.Map<FamilyHistory, FamilyHistoryDto>(familyHistory)
                : null;
        }

        [Authorize(HealthCarePermissions.Examinations.Edit)]
        public async Task<FamilyHistoryDto> UpdateAsync(FamilyHistoryUpdateDto input) => ObjectMapper.Map<FamilyHistory, FamilyHistoryDto>(
                await familyHistoryManager.UpdateAsync(input.Id, input.PatientId, input.Mother, input.Father,input.Sister,
                    input.Brother, input.Other, input.IsParentsRelative));
    }
}
