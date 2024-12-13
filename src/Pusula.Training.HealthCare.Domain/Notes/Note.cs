using JetBrains.Annotations;
using Pusula.Training.HealthCare.ProtocolTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Notes
{
    public class Note : FullAuditedAggregateRoot<Guid>
    {
        public virtual string? Text { get; private set; }


        protected Note()
        {
            
        }

        public Note(Guid id, string text)
        {
            Id = id;
            SetText(text);
        }

        public void SetText(string? text)
        {
            Text = text == null ? null : Check.Length(text, nameof(text), NoteConsts.TextMaxLength, 0);
        }

    }
}
