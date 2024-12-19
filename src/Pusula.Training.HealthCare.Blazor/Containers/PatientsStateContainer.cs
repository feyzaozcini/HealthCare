using Pusula.Training.HealthCare.Patients;
using System;

namespace Pusula.Training.HealthCare.Blazor.Containers
{
    public class PatientsStateContainer
    {
        private bool refresh = false;
        public bool Refresh
        {
            get => refresh;
            set
            {
                refresh = value;
                NotifyStateChanged();
            }
        }

        public PatientWithNavigationPropertiesDto SelectedPatient { get; private set; }

        public event Action OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();

        public void SetSelectedPatient(PatientWithNavigationPropertiesDto patient)
        {
            SelectedPatient = patient;
            Refresh = true;
        }
    }
}
