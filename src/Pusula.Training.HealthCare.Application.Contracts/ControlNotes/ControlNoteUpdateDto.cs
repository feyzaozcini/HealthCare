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

        [Required(ErrorMessage = "Note tarihi zorunludur.")]

        public DateTime NoteDate { get; set; }

        [Required(ErrorMessage = "Not alanı boş bırakılamaz")]

        public string Note { get; set; } = null!;
    }
}
