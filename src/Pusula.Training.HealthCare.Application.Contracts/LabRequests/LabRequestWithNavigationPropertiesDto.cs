using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Protocols;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.LabRequests;

public class LabRequestWithNavigationPropertiesDto
{
    public ProtocolDto Protocol { get; set; }
    public DoctorDto Doctor { get; set; }
    public LabRequestDto LabRequest { get; set; }
    public Guid Id { get; set; }

    public LabRequestWithNavigationPropertiesDto()
    {
    }
}
