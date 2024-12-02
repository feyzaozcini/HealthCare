using Pusula.Training.HealthCare.ProtocolTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Notes
{
    public class NoteUpdateDto : IHasConcurrencyStamp
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(NoteConsts.TextMaxLength)]
        public string Text { get; set; } = null!;
        public string ConcurrencyStamp { get; set; } = null!;
    }
}
