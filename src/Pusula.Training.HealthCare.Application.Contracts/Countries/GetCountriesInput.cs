using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Countries;

public class GetCountriesInput : PagedAndSortedResultRequestDto
{
    public string? FilterText { get; set; }

    public string? Name { get; set; }
    public string? Code { get; set; }

    public GetCountriesInput()
    {
        FilterText = string.Empty;
        Name = string.Empty;
        Code = string.Empty;
        MaxResultCount = 200;
        SkipCount = 0;
    }
}