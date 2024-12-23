using JetBrains.Annotations;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;

namespace Pusula.Training.HealthCare.Addresses
{
    public class AddressManager(IAddressRepository addressRepository) : DomainService
    {
        public virtual async Task<Address> CreateAsync(
        Guid patientId,
        Guid countryId,
        Guid cityId,
        Guid districtId,
        Guid villageId,
        string addressDescription,
        bool isPrimary
        )
        {
            var address = new Address(
             GuidGenerator.Create(),
             patientId,
             countryId,
             cityId,
             districtId,
             villageId,
             addressDescription,
             isPrimary
             );

            return await addressRepository.InsertAsync(address);
        }

        public virtual async Task<Address> UpdateAsync(
            Guid id,
            Guid patientId,
            Guid countryId,
            Guid cityId,
            Guid districtId,
            Guid villageId,
            string addressDescription,
            bool isPrimary,
            [CanBeNull] string? concurrencyStamp = null
        )
        {
            var address = await addressRepository.GetAsync(id);

            address.SetPatientId(patientId);
            address.SetCountryId(countryId);
            address.SetCityId(cityId);
            address.SetDistrictId(districtId);
            address.SetVillageId(villageId);
            address.SetAddressDescription(addressDescription);
            address.SetIsPrimary(isPrimary);

            return await addressRepository.UpdateAsync(address);
        }

    }
}
