using Pusula.Training.HealthCare.Departments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.DepartmentServices
{
    public class DepartmentServiceCreateDto
    {
        [Required]
        [StringLength(DepartmentServiceConsts.NameMaxLength)]
        public string Name { get; set; } = null!;
    }
}
