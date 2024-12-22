using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Districts;
using Pusula.Training.HealthCare.Villages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Addresses
{
    public class AddressCreateDto
    {
        public virtual Guid PatientId { get; set; }
        public virtual Guid CountryId { get; set; }
        public virtual Guid CityId { get; set; }
        public virtual Guid DistrictId { get; set; }
        public virtual Guid VillageId { get; set; }
        public virtual string AddressDescription { get; set; } = string.Empty;
        public virtual bool IsPrimary { get; set; }
        

        public List<CityDto> CityList { get; set; } = new List<CityDto>();
        public List<DistrictDto> DistrictList { get; set; } = new List<DistrictDto>();
        public List<VillageDto> VillageList { get; set; } = new List<VillageDto>();

        public AddressCreateDto()
        {
            
        }
    }
}
