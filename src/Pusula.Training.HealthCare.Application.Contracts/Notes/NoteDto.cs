using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Notes
{
    public class NoteDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public Guid Id { get; set; }
        public string? Text { get; set; } 
        public string ConcurrencyStamp { get; set; } = null!;
    }
}
