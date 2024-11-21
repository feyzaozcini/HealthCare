using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Core.Rules.Patients;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.PatientCompanies;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Entities;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.ObjectMapping;

namespace Pusula.Training.HealthCare.Anamneses
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.Examinations.Default)]
    public class AnamnesisAppService(IAnamnesisRepository anamnesisRepository,
        AnamnesisManager anamnesisManager) : HealthCareAppService, IAnamnesisAppService
    {

        [Authorize(HealthCarePermissions.Examinations.Create)]
        public async Task<AnamnesisDto> CreateAsync(AnamnesisCreateDto input)
        {
            var anamnesis = await anamnesisManager.CreateAsync(input.Complaint, input.StartDate, input.Story, input.ProtocolId);

            return ObjectMapper.Map<Anamnesis, AnamnesisDto>(anamnesis);
        }

        [Authorize(HealthCarePermissions.Examinations.Delete)]
        public async Task DeleteAsync(Guid id) => await anamnesisRepository.DeleteAsync(id);

        public async Task<AnamnesisDto> GetWithProtocolIdAsync(Guid protocolId)
        {
            var anamnesis = await anamnesisRepository.GetAsync(a => a.ProtocolId == protocolId);

            // Anamnesis'i DTO'ya mapliyoruz
            var anamnesisDto = ObjectMapper.Map<Anamnesis, AnamnesisDto>(anamnesis);

            return anamnesisDto;
        }
        public async Task<AnamnesisDto> GetAsync(Guid id) => ObjectMapper.Map<Anamnesis, AnamnesisDto>(
                await anamnesisRepository.GetAsync(id));


        public async Task<AnamnesisWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            var anamnesis = await anamnesisRepository.GetWithNavigationPropertiesAsync(id);
           
            return ObjectMapper.Map<AnamnesisWithNavigationProperties, AnamnesisWithNavigationPropertiesDto>(anamnesis);
        }

        [Authorize(HealthCarePermissions.Examinations.Edit)]

        public async Task<AnamnesisDto> UpdateAsync(AnamnesisUpdateDto input) => ObjectMapper.Map<Anamnesis, AnamnesisDto>(
                await anamnesisManager.UpdateAsync(input.Id,input.Complaint,input.StartDate,input.Story,input.ProtocolId));
    }
}
