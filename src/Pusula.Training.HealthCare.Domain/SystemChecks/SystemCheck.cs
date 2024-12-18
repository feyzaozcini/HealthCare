using JetBrains.Annotations;
using Pusula.Training.HealthCare.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.SystemChecks
{
    public class SystemCheck : AuditedEntity<Guid>
    {
        [NotNull]
        public Guid ProtocolId { get; private set; }

        public Protocol Protocol { get; private set; }

       
        public bool? GeneralSystemCheck { get; private set; }

       
        public bool? GenitoUrinary { get; private set; }

       
        public bool? Skin { get; private set; }

    
        public bool? Respiratory { get; private set; }

      
        public bool? Nervous { get; private set; }

 
        public bool? MusculoSkeletal { get; private set; }

   
        public bool? Circulatory { get; private set; }

   
        public bool? GastroIntestinal { get; private set; }

        public string? Description { get; private set; }
        protected SystemCheck()
        {
        }

        public SystemCheck(Guid id,Guid protocolId, bool? generalSystemCheck, bool? genitoUrinary, bool? skin, bool? respiratory, bool? nervous, bool? musculoSkeletal, bool? circulatory, bool? gastroIntestinal, string? description)
        {
            Id = id;
            SetProtocolId(protocolId);
            SetGeneralSystemCheck(generalSystemCheck);
            SetGenitoUrinary(genitoUrinary);
            SetSkin(skin);
            SetRespiratory(respiratory);
            SetNervous(nervous);
            SetMusculoSkeletal(musculoSkeletal);
            SetCirculatory(circulatory);
            SetGastroIntestinal(gastroIntestinal);
            SetDescription(description);
        }

        public void SetProtocolId(Guid protocolId)
        {
            ProtocolId = Check.NotNull(protocolId, nameof(protocolId));
        }

        public void SetGeneralSystemCheck(bool? generalSystemCheck)
        {
            GeneralSystemCheck = generalSystemCheck;
        }

        public void SetGenitoUrinary(bool? genitoUrinary)
        {
            GenitoUrinary = genitoUrinary;
        }

        public void SetSkin(bool? skin)
        {
            Skin = skin;
        }

        public void SetRespiratory(bool? respiratory)
        {
            Respiratory = respiratory;
        }

        public void SetNervous(bool? nervous)
        {
            Nervous = nervous;
        }

        public void SetMusculoSkeletal(bool? musculoSkeletal)
        {
            MusculoSkeletal = musculoSkeletal;
        }

        public void SetCirculatory(bool? circulatory)
        {
            Circulatory = circulatory;
        }

        public void SetGastroIntestinal(bool? gastroIntestinal)
        {
            GastroIntestinal = gastroIntestinal;
        }

        public void SetDescription(string? description)
        {
            Description = description;
        }
    }
}
