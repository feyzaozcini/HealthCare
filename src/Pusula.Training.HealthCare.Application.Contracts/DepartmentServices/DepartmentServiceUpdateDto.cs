using Pusula.Training.HealthCare.Departments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.DepartmentServices
{
    public class DepartmentServiceUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(DepartmentServiceConsts.NameMaxLength)]
        public string Name { get; set; } = null!;

        public string ConcurrencyStamp { get; set; } = null!;
    }
}
