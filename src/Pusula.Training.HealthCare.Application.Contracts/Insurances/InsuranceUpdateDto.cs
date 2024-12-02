using Pusula.Training.HealthCare.ProtocolTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Insurances
{
    public class InsuranceUpdateDto : IHasConcurrencyStamp
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(InsuranceConsts.NameMaxLength)]
        public string Name { get; set; } = null!;

        public string ConcurrencyStamp { get; set; } = null!;
    }
}
