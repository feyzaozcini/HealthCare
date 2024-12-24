using Pusula.Training.HealthCare.Doctors;
using System;

namespace Pusula.Training.HealthCare.Blazor.Containers
{
    public class DoctorsStateContainer
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

        public DoctorWithNavigationPropertiesDto SelectedDoctor { get; private set; }

        public event Action OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();

        public void SetSelectedDoctor(DoctorWithNavigationPropertiesDto doctor)
        {
            SelectedDoctor = doctor;
            Refresh = true;
        }

        public void ClearSelectedDoctor()
        {
            SelectedDoctor = null;
            Refresh = true;
        }
    }
}
