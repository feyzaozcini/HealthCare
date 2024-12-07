using Pusula.Training.HealthCare.Patients;

namespace Pusula.Training.HealthCare.Blazor.Containers
{
    public class PatientStateContainer
    {
        //Patient bilgilerinin blazor tarafında taşınması işlemi için gerekti
        //public PatientDto SelectedPatient { get; set; } = null!;
        public string? PreviousPageUrl { get; set; }

        public PatientWithNavigationPropertiesDto SelectedPatientNavigation { get; set; } = null!;
    }
}
