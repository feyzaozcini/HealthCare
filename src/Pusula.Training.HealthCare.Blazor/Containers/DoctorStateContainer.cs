using Pusula.Training.HealthCare.Doctors;


namespace Pusula.Training.HealthCare.Blazor.Containers
{
    public class DoctorStateContainer
    {
        //Doctor bilgilerinin blazor tarafında taşınması işlemi için gerekti
        public DoctorWithNavigationPropertiesDto SelectedDoctor { get; set; } = null!;
    }
}
