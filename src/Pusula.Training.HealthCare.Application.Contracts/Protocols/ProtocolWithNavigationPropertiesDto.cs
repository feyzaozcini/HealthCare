using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Patients;

namespace Pusula.Training.HealthCare.Protocols;

public class ProtocolWithNavigationPropertiesDto
{
    public ProtocolDto Protocol { get; set; } = null!;
    public PatientDto Patient { get; set; } = null!;
    public DepartmentDto Department { get; set; } = null!;
    public DoctorDto Doctor { get; set; } = null!;
}