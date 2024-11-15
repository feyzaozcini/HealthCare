using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Titles
{
    public class TitleUpdateDto
    {
        [Required]
        public Guid Id { get; set; } = default!;

        [Required]
        [StringLength(TitleConsts.NameMaxLength)]
        public string Name { get; set; } = null!;
    }
}
