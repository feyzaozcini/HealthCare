using Pusula.Training.HealthCare.Addresses;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Districts;
using Pusula.Training.HealthCare.PatientCompanies;
using Pusula.Training.HealthCare.Protocols;
using Pusula.Training.HealthCare.Villages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Patients
{
    public class PatientWithNavigationPropertiesDto
    {
        public PatientDto? Patient { get; set; }
        public CountryDto? Country { get; set; } 
        public CityDto? City { get; set; } 
        public DistrictDto? District { get; set; } 
        public VillageDto? Village { get; set; } 
        public PatientCompanyDto? PatientCompany { get; set; } 
        public AddressDto? Address { get; set; }
        public List<AddressDto>? Addresses { get; set; }
    }
}
