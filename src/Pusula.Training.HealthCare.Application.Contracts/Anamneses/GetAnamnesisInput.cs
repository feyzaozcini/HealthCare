using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Anamneses
{
    public class GetAnamnesisInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }
        public string? Complaint { get; set; }
        public DateTime? StartDate { get; set; }

        public string? Story { get; set; }

        public Guid? ProtocolId { get; set; }

        public GetAnamnesisInput()
        {
        }
    }
}
