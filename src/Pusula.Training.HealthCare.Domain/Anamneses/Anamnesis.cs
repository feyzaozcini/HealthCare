using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Anamneses
{
    public class Anamnesis : AuditedEntity<Guid>, ISoftDelete
    {

        [NotNull]
        public string Complaint { get; set; }

        [NotNull]
        public DateTime StartDate { get; set; }

        [NotNull]
        public string Story { get; set; }

        public bool IsDeleted { get; set; }

        [NotNull]
        public virtual Guid ProtocolId { get; set; }

        protected Anamnesis()
        {
            Complaint = string.Empty;
            StartDate = DateTime.Now;
            Story = string.Empty;

        }

        public Anamnesis(Guid id, string complaint, DateTime startDate, string story, Guid protocolId)
        {

            Id = id;
            Check.NotNull(complaint, nameof(complaint));
            Check.NotNull(story, nameof(story));


            Complaint = complaint;
            StartDate = startDate;
            Story = story;

            ProtocolId = protocolId;

        }
    }
}
