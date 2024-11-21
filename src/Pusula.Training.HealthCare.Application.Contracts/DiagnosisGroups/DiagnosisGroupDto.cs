using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.DiagnosisGroups
{
    public class DiagnosisGroupDto : EntityDto<Guid>
    {
        
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;

        public bool IsDeleted { get; set; } 
    }
}
