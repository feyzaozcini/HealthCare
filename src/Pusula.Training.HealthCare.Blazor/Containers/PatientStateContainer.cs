using Pusula.Training.HealthCare.Patients;

namespace Pusula.Training.HealthCare.Blazor.Containers
{
    public class PatientStateContainer
    {
        public string? PreviousPageUrl { get; set; }

        public PatientWithNavigationPropertiesDto SelectedPatientNavigation { get; set; } = null!;
    }
}
