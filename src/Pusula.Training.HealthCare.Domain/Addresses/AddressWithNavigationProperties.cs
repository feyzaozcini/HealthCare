using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Districts;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Protocols;
using Pusula.Training.HealthCare.Villages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Addresses
{
    public class AddressWithNavigationProperties
    {
        public Address Address { get; set; } = null!;
        public Patient Patient { get; set; } = null!;
        public Country Country { get; set; } = null!;
        public City City { get; set; } = null!;
        public District District { get; set; } = null!;
        public Village Village { get; set; } = null!;
    }
}