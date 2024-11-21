using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Anamneses
{
    public class AnamnesisDto : AuditedEntityDto<Guid>
    {
        
        public string Complaint { get; set; } = null!;
        public DateTime StartDate { get; set; }

        public string Story { get; set; } = null!;

        public  Guid ProtocolId { get; set; } 
    }
}
