using JetBrains.Annotations;
using Pusula.Training.HealthCare.ProtocolTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;

namespace Pusula.Training.HealthCare.Notes
{
    public class NoteManager(INoteRepository noteRepository) : DomainService
    {
        public virtual async Task<Note> CreateAsync(
        string? text)
        {

            var note = new Note(
             GuidGenerator.Create(),
             text
             );

            return await noteRepository.InsertAsync(note);
        }

        public virtual async Task<Note> UpdateAsync(
            Guid id,
            string? text, [CanBeNull] string? concurrencyStamp = null
        )
        {

            var note = await noteRepository.GetAsync(id);
            note.SetText(text);
            note.SetConcurrencyStampIfNotNull(concurrencyStamp);

            return await noteRepository.UpdateAsync(note);
        }

    }
}
