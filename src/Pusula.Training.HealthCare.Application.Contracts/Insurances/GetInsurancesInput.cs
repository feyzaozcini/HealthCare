using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Insurances
{
    public class GetInsurancesInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? Name { get; set; }

        public GetInsurancesInput()
        {
            FilterText = string.Empty;
            Name = string.Empty;
            MaxResultCount = PagedAndSortedResultRequestDto.MaxMaxResultCount;
            SkipCount = 0;
        }
    }
}
