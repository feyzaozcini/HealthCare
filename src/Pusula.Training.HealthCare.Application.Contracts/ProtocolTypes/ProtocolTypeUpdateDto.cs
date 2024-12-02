using Pusula.Training.HealthCare.DepartmentServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.ProtocolTypes
{
    public class ProtocolTypeUpdateDto : IHasConcurrencyStamp
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(ProtocolTypeConsts.NameMaxLength)]
        public string Name { get; set; } = null!;

        public string ConcurrencyStamp { get; set; } = null!;
    }
}
