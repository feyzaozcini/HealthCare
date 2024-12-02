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
        [NotNull]
        public virtual string Text { get; private set; }


        protected Note()
        {
            Text = string.Empty;
        }

        public Note(Guid id, string text)
        {
            Id = id;
            SetText(text);
        }

        public void SetText(string text)
        {
            Check.NotNull(text, nameof(text));
            Check.Length(text, nameof(text), NoteConsts.TextMaxLength, 0);
            Text = text;
        }
    }
}
