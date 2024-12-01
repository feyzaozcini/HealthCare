using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Notes
{
    public class GetNotesInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? Text { get; set; }

        public GetNotesInput()
        {
        }
    }
}
