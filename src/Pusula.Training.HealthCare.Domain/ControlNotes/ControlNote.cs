using JetBrains.Annotations;
using Pusula.Training.HealthCare.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Pusula.Training.HealthCare.ControlNotes
{
    public class ControlNote : AuditedEntity<Guid>  
    {
        [NotNull]
        public Guid ProtocolId { get; private set; }

        public Protocol Protocol { get; private set; }

        [NotNull]
        public DateTime NoteDate { get; private set; }


        [NotNull]
        public string Note { get; private set; }

        public IdentityUser  User { get; set; }

        protected ControlNote() 
        {
            Note = string.Empty;
        }

        public ControlNote(Guid id,Guid protocolId, DateTime noteDate,string note)
        {
            Id= id;
            SetProtocolId(protocolId);
            SetNoteDate(noteDate);
            SetNote(note);
        }

        public void SetProtocolId(Guid protocolId)
        {
            ProtocolId = Check.NotNull(protocolId, nameof(protocolId));
        }
        public void SetNoteDate(DateTime noteDate)
        {
            NoteDate = Check.NotNull(noteDate, nameof(noteDate));
        }
        public void SetNote(string note)
        {
            Check.NotNullOrWhiteSpace(note, nameof(note));
            Check.Length(note, nameof(note), ControlNoteConsts.NoteMaxLength);
            Note= note;
        }

      

    }
}
