using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.ControlNotes
{
    public class ControlNoteUpdateDto
    {
        public Guid Id { get; set; }

        [Required]
        public Guid ProtocolId { get; set; }

        [Required]
        public DateTime NoteDate { get; set; }

        [Required]
        public string Note { get; set; } = null!;
    }
}
