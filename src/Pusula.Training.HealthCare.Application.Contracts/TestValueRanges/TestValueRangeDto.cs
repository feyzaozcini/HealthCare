using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.TestValueRanges
{
    public class TestValueRangeDto : AuditedEntityDto<Guid>
    {
        [Required]
        public Guid TestGroupItemId { get; set; }

        [Required]
        public decimal MinValue { get; set; }

        [Required]
        public decimal MaxValue { get; set; }

        [Required]
        public TestUnitTypes Unit { get; set; }
    }
}
