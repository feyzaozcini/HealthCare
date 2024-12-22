using Pusula.Training.HealthCare.Protocols;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Addresses
{
    public class AddressDto : AuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public virtual Guid PatientId { get; set; }
        public virtual Guid CountryId { get; set; }
        public virtual Guid CityId { get; set; }
        public virtual Guid DistrictId { get; set; }
        public virtual Guid VillageId { get; set; }
        public virtual string AddressDescription { get; private set; } = string.Empty;
        public string ConcurrencyStamp { get; set; } = null!;
        public virtual bool IsPrimary { get; set; }

        public AddressDto()
        {
        }
    }
}
