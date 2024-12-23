using Pusula.Training.HealthCare.Addresses;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Districts;
using Pusula.Training.HealthCare.PatientCompanies;
using Pusula.Training.HealthCare.Villages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Patients
{
    public class PatientWithNavigationProperties
    {
        public Patient? Patient { get; set; }
        public PatientCompany? PatientCompany { get; set; }
        public Country? PrimaryCountry { get; set; }
        public City? PrimaryCity { get; set; } 
        public District? PrimaryDistrict { get; set; } 
        public Village? PrimaryVillage { get; set; } 
        public Country? SecondaryCountry { get; set; } 
        public City? SecondaryCity { get; set; } 
        public District? SecondaryDistrict { get; set; }
        public Village? SecondaryVillage { get; set; } 
        public Address? Address { get; set; }

        public List<Address>? Addresses { get; set; }
    }
}
