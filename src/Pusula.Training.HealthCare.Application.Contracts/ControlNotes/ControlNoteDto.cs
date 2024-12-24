using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.ControlNotes
{
    public class ControlNoteDto : AuditedEntityDto<Guid>
    {
        public Guid ProtocolId { get;  set; }

        public DateTime NoteDate { get;  set; }
        public string Note { get; set; } = null!;

        public string CreatorTitle { get; set; } = null!;

        public string CreatorName { get; set; } = null!;

        public string CreatorSurname { get; set; } = null!;

    }
}
