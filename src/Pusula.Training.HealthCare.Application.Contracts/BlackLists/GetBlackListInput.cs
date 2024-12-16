using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.BlackLists
{
    public class GetBlackListInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? DoctorId { get; set; }
        public BlackListStatus? BlackListStatus { get; set; }
        public string? Note { get; set; }

        public GetBlackListInput()
        {

        }
    }
}
