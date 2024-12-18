using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.SystemChecks
{
    public class SystemCheckManager(ISystemCheckRepository systemCheckRepository) : DomainService
    {
        public virtual async Task<SystemCheck> CreateAsync(Guid protocolId,bool? generalSystemCheck,bool? genitoUrinary, bool? skin, 
            bool? respiratory,bool? nervous,bool? musculoSkeletal,bool? circulatory, bool? gastroIntestinal, string? description)
        {
            var systemCheck = new SystemCheck(
                GuidGenerator.Create(), protocolId, generalSystemCheck, genitoUrinary, skin, respiratory, nervous, musculoSkeletal,
                circulatory, gastroIntestinal, description);

            return await systemCheckRepository.InsertAsync(systemCheck);
        }

        public virtual async Task<SystemCheck> UpdateAsync(Guid id,Guid protocolId, bool? generalSystemCheck, bool? genitoUrinary, bool? skin,
           bool? respiratory, bool? nervous, bool? musculoSkeletal, bool? circulatory, bool? gastroIntestinal, string? description)
        {
            var systemCheck = await systemCheckRepository.GetAsync(id);
            systemCheck.SetProtocolId(protocolId);
            systemCheck.SetGeneralSystemCheck(generalSystemCheck);
            systemCheck.SetGenitoUrinary(genitoUrinary);
            systemCheck.SetSkin(skin);
            systemCheck.SetRespiratory(respiratory);
            systemCheck.SetNervous(nervous);
            systemCheck.SetMusculoSkeletal(musculoSkeletal);
            systemCheck.SetCirculatory(circulatory);
            systemCheck.SetGastroIntestinal(gastroIntestinal);
            systemCheck.SetDescription(description);

            return await systemCheckRepository.UpdateAsync(systemCheck);

           
        }
    }
}
