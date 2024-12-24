using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Pusula.Training.HealthCare.PatientHistories
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.Examinations.Default)]
    public class PatientHistoriesAppService(IPatientHistoryRepository patientHistoryRepository,
        PatientHistoryManager patientHistoryManager)
    : HealthCareAppService, IPatientHistoriesAppService
    {
        [Authorize(HealthCarePermissions.Examinations.Create)]
        public async Task<PatientHistoryDto> CreateAsync(PatientHistoryCreateDto input)
        {
            var patientHistory = await patientHistoryManager.CreateAsync(input.PatientId, input.Habit,input.Disease, 
                input.Medicine, input.Operation,input.Vaccination, input.Allergy, input.SpecialCondition, input.Device, input.Therapy,
                input.Job, input.EducationLevel, input.MaritalStatus);

            return ObjectMapper.Map<PatientHistory, PatientHistoryDto>(patientHistory);
        }

        [Authorize(HealthCarePermissions.Examinations.Delete)]
        public async Task DeleteAsync(Guid id) => await patientHistoryRepository.DeleteAsync(id);

        public async Task<PatientHistoryDto> GetAsync(Guid id) =>
            ObjectMapper.Map<PatientHistory, PatientHistoryDto>(await patientHistoryRepository.GetAsync(id));

        public async Task<PatientHistoryDto> GetByPatientIdAsync(Guid patientId)
        {
            var patientHistory = await patientHistoryRepository.GetByPatientIdAsync(patientId);

            // Eğer kayıt yoksa doğrudan null dönecek
            return patientHistory != null
                ? ObjectMapper.Map<PatientHistory, PatientHistoryDto>(patientHistory)
                : null;
        }

        [Authorize(HealthCarePermissions.Examinations.Edit)]
        public async Task<PatientHistoryDto> UpdateAsync(PatientHistoryUpdateDto input) => ObjectMapper.Map<PatientHistory, PatientHistoryDto>(
                await patientHistoryManager.UpdateAsync(input.Id, input.PatientId, input.Habit, input.Disease,input.Medicine, 
                    input.Operation, input.Vaccination, input.Allergy, input.SpecialCondition, input.Device, input.Therapy,
                    input.Job, input.EducationLevel, input.MaritalStatus));
    }
}
