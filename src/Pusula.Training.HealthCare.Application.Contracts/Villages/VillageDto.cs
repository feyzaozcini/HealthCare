using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Villages
{
    public class VillageDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; } = null!;
        public Guid DistrictId { get; set; }
    }
}
