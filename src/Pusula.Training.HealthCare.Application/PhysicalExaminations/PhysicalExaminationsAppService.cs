using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Anamneses;
using Pusula.Training.HealthCare.FallRisks;
using Pusula.Training.HealthCare.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Pusula.Training.HealthCare.PhysicalExaminations
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.Examinations.Default)]
    public class PhysicalExaminationsAppService(IPhysicalExaminationRepository physicalExaminationRepository,
        PhysicalExaminationManager physicalExaminationManager) : HealthCareAppService, IPhysicalExaminationsAppService
    {
        [Authorize(HealthCarePermissions.Examinations.Create)]
        public async Task<PhysicalExaminationDto> CreateAsync(PhysicalExaminationCreateDto input)
        {
            var physicalExamination = await physicalExaminationManager.CreateAsync(input.ProtocolId,input.Weight,
                input.Height,input.BMI,input.VYA,input.Temperature,input.Pulse,input.SystolicBP,input.DiastolicBP,
                input.SPO2,input.Note);

            return ObjectMapper.Map<PhysicalExamination, PhysicalExaminationDto>(physicalExamination);
        }

        [Authorize(HealthCarePermissions.Examinations.Delete)]
        public async Task DeleteAsync(Guid id) => await physicalExaminationRepository.DeleteAsync(id);


        public async Task<PhysicalExaminationDto> GetAsync(Guid id) => ObjectMapper.Map<PhysicalExamination, PhysicalExaminationDto>(
                await physicalExaminationRepository.GetAsync(id));


        public async Task<PhysicalExaminationWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            var physicalExamination = await physicalExaminationRepository.GetWithNavigationPropertiesAsync(id);

            return ObjectMapper.Map<PhysicalExaminationWithNavigationProperties, PhysicalExaminationWithNavigationPropertiesDto>(physicalExamination);
        }

        public async Task<PhysicalExaminationDto> GetWithProtocolIdAsync(Guid protocolId)
        {
            //var physicalExamination = await physicalExaminationRepository.GetAsync(a => a.ProtocolId == protocolId);

            var physicalExamination = await physicalExaminationRepository.FirstOrDefaultAsync(a => a.ProtocolId == protocolId);
            // Eğer fallRisk null ise null döner
            if (physicalExamination == null)
            {
                return new PhysicalExaminationDto();
            }

            // Anamnesis'i DTO'ya mapliyoruz
            var physicalExaminationDto = ObjectMapper.Map<PhysicalExamination, PhysicalExaminationDto>(physicalExamination);

            return physicalExaminationDto;
        }

        public async Task<PhysicalExaminationDto> UpdateAsync(PhysicalExaminationUpdateDto input) => ObjectMapper.Map<PhysicalExamination, PhysicalExaminationDto>(
                await physicalExaminationManager.UpdateAsync(input.Id,input.ProtocolId,input.Weight,input.Height,input.BMI,
                    input.VYA,input.Temperature,input.Pulse,input.SystolicBP,input.DiastolicBP,input.SPO2,input.Note));

    }
}
