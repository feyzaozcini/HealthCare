using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Countries;

public class CountryUpdateDto
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Code { get; set; } = null!;

}
