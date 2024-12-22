using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Districts;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Villages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Addresses
{
    public class Address : AuditedEntity<Guid>
    {
        public virtual Guid PatientId { get; private set; }
        public virtual Guid CountryId { get; private set; }
        public virtual Guid CityId { get; private set; }
        public virtual Guid DistrictId { get; private set; }
        public virtual Guid VillageId { get; private set; }
        public virtual string AddressDescription { get; private set; } = string.Empty; //string.Empty protected const da tanımlayınca null uyarısı verdiği için burada tanımladım.
        public virtual bool IsPrimary { get; private set; }
        public Patient? Patient { get; private set; }
        public Country? Country { get; private set; }
        public City? City { get; private set; }
        public District? District { get; private set; }
        public Village? Village { get; private set; }

        protected Address()
        {
        }


        public Address(Guid id, Guid patientId, Guid countryId, Guid cityId, Guid districtId, Guid villageId, string addressDescription, bool isPrimary)
        {
            Id = id;
            SetPatientId(patientId);
            SetCountryId(countryId);
            SetCityId(cityId);
            SetDistrictId(districtId);
            SetVillageId(villageId);
            SetAddressDescription(addressDescription);
            SetIsPrimary(isPrimary);
        }


        public void SetPatientId(Guid patientId) => PatientId = Check.NotNull(patientId, nameof(patientId));
        public void SetCountryId(Guid countryId) => CountryId = countryId;
        public void SetCityId(Guid cityId) => CityId = cityId;
        public void SetDistrictId(Guid districtId) => DistrictId = districtId;
        public void SetVillageId(Guid villageId) => VillageId = villageId;
        public void SetAddressDescription(string addressDescription) => AddressDescription = addressDescription;
        public void SetIsPrimary(bool isPrimary) => IsPrimary = isPrimary;
    }
}
