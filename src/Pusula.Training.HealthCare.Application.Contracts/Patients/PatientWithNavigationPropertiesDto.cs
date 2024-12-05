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
        public CountryDto? PrimaryCountry { get; set; } 
        public CityDto? PrimaryCity { get; set; } 
        public DistrictDto? PrimaryDistrict { get; set; } 
        public VillageDto? PrimaryVillage { get; set; } 
        public CountryDto? SecondaryCountry { get; set; } 
        public CityDto? SecondaryCity { get; set; } 
        public DistrictDto? SecondaryDistrict { get; set; } 
        public VillageDto? SecondaryVillage { get; set; }
        public PatientCompanyDto? PatientCompany { get; set; } 
        //public ProtocolDto? Protocol { get; set; }
    }
}
