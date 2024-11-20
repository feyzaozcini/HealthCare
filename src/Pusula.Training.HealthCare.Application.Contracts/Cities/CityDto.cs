using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Cities
{
    public class CityDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; } = null!;

        // City'nin hangi Country'ye ait olduğunu belirten ID
        public Guid CountryId { get; set; }
    }
}
