using Pusula.Training.HealthCare.ProtocolTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Notes
{
    public class NoteCreateDto
    {
        [Required]
        [StringLength(NoteConsts.TextMaxLength)]
        public string Text { get; set; } = null!;
    }
}
