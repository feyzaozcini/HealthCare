using Pusula.Training.HealthCare.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Addresses
{
    public class GetAddressesInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }
        public virtual Guid? PatientId { get; set; }
        public virtual Guid? CountryId { get; set; }
        public virtual Guid? CityId { get; set; }
        public virtual Guid? DistrictId { get; set; }
        public virtual Guid? VillageId { get; set; }
        public virtual string? AddressDescription { get; private set; } = string.Empty;
        public virtual bool IsPrimary { get; set; }


        public GetAddressesInput()
        {

        }

        public GetAddressesInput(
            string? filterText,
            Guid? patientId,
            Guid? countryId,
            Guid? cityId,
            Guid? districtId,
            Guid? villageId,
            string? addressDescription,
            bool isPrimary,
            int currentPage,
            int pageSize)
        {
            FilterText = filterText;
            PatientId = patientId;
            CountryId = countryId;
            CityId = cityId;
            DistrictId = districtId;
            VillageId = villageId;
            AddressDescription = addressDescription;
            IsPrimary = isPrimary;
            MaxResultCount = pageSize;
            SkipCount = (currentPage - 1) * pageSize;
        }
    }
}
