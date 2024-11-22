using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.PshychologicalStates
{
    public class PshychologicalStateDto : EntityDto<Guid>
    {
        public string Description { get; set; } = null!;
        public State State { get; set; }
        public Guid ProtocolId { get; set; }

    }
}
