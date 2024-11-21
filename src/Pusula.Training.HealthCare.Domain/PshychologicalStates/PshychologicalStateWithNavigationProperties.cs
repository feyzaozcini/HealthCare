using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.PatientCompanies;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.PshychologicalStates
{
    public class PshychologicalStateWithNavigationProperties
    {

        public PshychologicalState PhychologicalState { get; set; } = null!;

        public Protocol Protocol { get; set; } = null!;

       
    }
}
