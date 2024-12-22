using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Addresses
{
    public class AddressUpdateDto : IHasConcurrencyStamp
    {
        public virtual Guid PatientId { get; set; }
        public virtual Guid CountryId { get; set; }
        public virtual Guid CityId { get; set; }
        public virtual Guid DistrictId { get; set; }
        public virtual Guid VillageId { get; set; }
        public virtual string AddressDescription { get; private set; } = string.Empty;
        public virtual bool IsPrimary { get; set; }
        public string ConcurrencyStamp { get; set; } = null!;

        public AddressUpdateDto()
        {
        }
    }
}
