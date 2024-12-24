using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.ControlNotes
{
    public class ControlNoteManager(IControlNoteRepository controlNoteRepository) : DomainService
    {
        public virtual async Task<ControlNote> CreateAsync(Guid protocolId, DateTime noteDate, string note)
        {
            var controlNote = new ControlNote(GuidGenerator.Create(),protocolId,noteDate,note);

            return await controlNoteRepository.InsertAsync(controlNote);
        }

        public virtual async Task<ControlNote> UpdateAsync(Guid id, Guid protocolId, DateTime noteDate, string note)
        {
            var controlNote = await controlNoteRepository.GetAsync(id);
            controlNote.SetProtocolId(protocolId);
            controlNote.SetNoteDate(noteDate);
            controlNote.SetNote(note);

            return await controlNoteRepository.UpdateAsync(controlNote);
        }
    }
}
