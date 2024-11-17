using System;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Countries;

public class CountryDto : AuditedEntityDto<Guid>
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;

}
