using System;
using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.Countries;

public class CountryUpdateDto
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Code { get; set; } = null!;

}
