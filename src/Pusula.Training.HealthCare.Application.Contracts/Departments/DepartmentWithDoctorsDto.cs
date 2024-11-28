using Pusula.Training.HealthCare.Doctors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Departments
{
    public class DepartmentWithDoctorsDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; } = null!;

        public List<DoctorWithNavigationPropertiesDto> Doctors { get; set; } = null!;
        
    }
}
