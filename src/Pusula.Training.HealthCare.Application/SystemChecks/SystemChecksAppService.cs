using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Core;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.PshychologicalStates;
using Pusula.Training.HealthCare.TestGroupItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Caching;

namespace Pusula.Training.HealthCare.SystemChecks
{

    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.Examinations.Default)]
    public class SystemChecksAppService(
    ISystemCheckRepository systemCheckRepository, SystemCheckManager systemCheckManager)
    : HealthCareAppService, ISystemChecksAppService
    {

        [Authorize(HealthCarePermissions.Examinations.Create)]

        public async Task<SystemCheckDto> CreateAsync(SystemCheckCreateDto input)
        {

            var systemCheck = await systemCheckManager.CreateAsync(input.ProtocolId, input.GeneralSystemCheck, input.GenitoUrinary, input.Skin,
                input.Respiratory, input.Nervous, input.MusculoSkeletal, input.Circulatory, input.GastroIntestinal, input.Description);

            return ObjectMapper.Map<SystemCheck, SystemCheckDto>(systemCheck);
        }

        [Authorize(HealthCarePermissions.Examinations.Delete)]

        public async Task DeleteAsync(Guid id) => await systemCheckRepository.DeleteAsync(id);



        public async Task<SystemCheckDto> GetAsync(Guid id) =>
            ObjectMapper.Map<SystemCheck, SystemCheckDto>(await systemCheckRepository.GetAsync(id));


        public async Task<SystemCheckDto> GetByProtocolIdAsync(Guid protocolId)
        {
            var systemCheck = await systemCheckRepository.GetByProtocolIdAsync(protocolId);

            // Eğer kayıt yoksa doğrudan null dönecek
            return systemCheck != null
                ? ObjectMapper.Map<SystemCheck, SystemCheckDto>(systemCheck)
                : null;
            
        }

          

        [Authorize(HealthCarePermissions.Examinations.Edit)]
        public async Task<SystemCheckDto> UpdateAsync(SystemCheckUpdateDto input) => ObjectMapper.Map<SystemCheck, SystemCheckDto>(
                await systemCheckManager.UpdateAsync(input.Id, input.ProtocolId, input.GeneralSystemCheck, input.GenitoUrinary, input.Skin,
                input.Respiratory, input.Nervous, input.MusculoSkeletal, input.Circulatory, input.GastroIntestinal, input.Description));
    }
}
