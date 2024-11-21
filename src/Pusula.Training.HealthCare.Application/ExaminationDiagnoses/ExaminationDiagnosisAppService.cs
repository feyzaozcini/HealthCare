using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Anamneses;
using Pusula.Training.HealthCare.Diagnoses;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.ObjectMapping;

namespace Pusula.Training.HealthCare.ExaminationDiagnoses
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.Examinations.Default)]
    public class ExaminationDiagnosisAppService(IExaminationDiagnosisRepository examinationDiagnosisRepository,
        ExaminationDiagnosisManager examinationDiagnosisManager) : HealthCareAppService, IExaminationDiagnosisAppService
    {
        [Authorize(HealthCarePermissions.Examinations.Create)]
        public async Task<ExaminationDiagnosisDto> CreateAsync(ExaminationDiagnosisCreateDto input)
        {
            var examinationDiagnosis = await examinationDiagnosisManager.CreateAsync(input.DiagnosisType,input.ProtocolId,input.InitialDate,input.DiagnosisId,input.Note);

            return ObjectMapper.Map<ExaminationDiagnosis, ExaminationDiagnosisDto>(examinationDiagnosis);
        }

        [Authorize(HealthCarePermissions.Examinations.Delete)]
        public async Task DeleteAsync(Guid id) => await examinationDiagnosisRepository.DeleteAsync(id);


        [Authorize(HealthCarePermissions.Examinations.Delete)]
        public async Task DeleteByDiagnosisIdAsync(Guid diagnosisId)
        {
            var deletedEntity = await examinationDiagnosisRepository.GetAsync(x => x.DiagnosisId == diagnosisId);

            await examinationDiagnosisRepository.DeleteAsync(deletedEntity.Id);
        }

        public async Task<ExaminationDiagnosisDto> GetAsync(Guid id)=> ObjectMapper.Map<ExaminationDiagnosis, ExaminationDiagnosisDto>(
                await examinationDiagnosisRepository.GetAsync(id));

        public async Task<PagedResultDto<ExaminationDiagnosisWithNavigationPropertiesDto>> GetListAsync(GetExaminationDiagnosisInput input)
        {
            var totalCount = await examinationDiagnosisRepository.GetCountAsync(input.FilterText,input.InitialDate,input.Note,input.ProtocolId,input.DiagnosisId,input.Sorting,input.MaxResultCount,input.SkipCount);

            var items = await examinationDiagnosisRepository.GetListWithNavigationPropertiesAsync(input.FilterText,input.InitialDate,input.Note,input.ProtocolId,input.DiagnosisId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ExaminationDiagnosisWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ExaminationDiagnosisWithNavigationProperties>, List<ExaminationDiagnosisWithNavigationPropertiesDto>>(items)
            };
        }

        public async Task<List<ExaminationDiagnosisWithNavigationPropertiesDto>> GetListNotPagedAsync(GetExaminationDiagnosisInput input)
        {
            // Repository'den ProtocolId'ye göre kayıtları getir
            var diagnosisList = await examinationDiagnosisRepository.GetListWithNavigationPropertiesAsync(input.FilterText,input.InitialDate,input.Note,input.ProtocolId,input.DiagnosisId);

            // DTO'ya map et
            var result = diagnosisList.Select(examinationDiagnosis =>
                new ExaminationDiagnosisWithNavigationPropertiesDto
                {
                    ExaminationDiagnosis = ObjectMapper.Map<ExaminationDiagnosis, ExaminationDiagnosisDto>(examinationDiagnosis.ExaminationDiagnosis),
                    Protocol = ObjectMapper.Map<Protocol, ProtocolDto>(examinationDiagnosis.Protocol),
                    Diagnosis = ObjectMapper.Map<Diagnosis, DiagnosisDto>(examinationDiagnosis.Diagnosis)
                    // Daha fazla navigation property varsa buraya eklenebilir
                }
            ).ToList();

            return result;
        }

        public async Task<ExaminationDiagnosisWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            var patient = await examinationDiagnosisRepository.GetWithNavigationPropertiesAsync(id);
           
            return ObjectMapper.Map<ExaminationDiagnosisWithNavigationProperties, ExaminationDiagnosisWithNavigationPropertiesDto>(patient);
        }


        [Authorize(HealthCarePermissions.Examinations.Edit)]
        public async Task<ExaminationDiagnosisDto> UpdateAsync(ExaminationDiagnosisUpdateDto input) => ObjectMapper.Map<ExaminationDiagnosis, ExaminationDiagnosisDto>(
                await examinationDiagnosisManager.UpdateAsync(
                    input.Id,input.DiagnosisType,input.ProtocolId,input.InitialDate,input.DiagnosisId,input.Note));

        
    }
}
