using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.ControlNotes
{
    public class GetControlsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }
        public Guid? ProtocolId { get; set; }
        public DateTime? NoteDate { get; set; }
        public string? Note { get; set; } = null!;

    }
}
