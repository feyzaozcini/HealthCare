using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.SystemChecks
{
    public class SystemCheckDto : AuditedEntityDto<Guid>
    {
        public Guid ProtocolId { get;  set; }

        public bool? GeneralSystemCheck { get;  set; }

        public bool? GenitoUrinary { get;  set; }

        public bool? Skin { get;  set; }

        public bool? Respiratory { get;  set; }

        public bool? Nervous { get;  set; }

        public bool? MusculoSkeletal { get;  set; }

        public bool? Circulatory { get;  set; }

        public bool? GastroIntestinal { get;  set; }

        public string? Description { get;  set; }
    }
}
