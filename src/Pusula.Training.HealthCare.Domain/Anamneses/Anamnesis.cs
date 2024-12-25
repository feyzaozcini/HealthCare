using JetBrains.Annotations;
using Pusula.Training.HealthCare.Protocols;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Anamneses
{
    public class Anamnesis : AuditedEntity<Guid>, ISoftDelete
    {

        [NotNull]
        public string Complaint { get; private set; } = string.Empty;

        [NotNull]
        public DateTime StartDate { get; private set; }

        [NotNull]
        public string Story { get; private set; } = string.Empty;

        public bool IsDeleted { get;  set; }

        [NotNull]
        public virtual Guid ProtocolId { get; private set; }

        protected Anamnesis()
        {
            Complaint = string.Empty;
            StartDate = DateTime.Now;
            Story = string.Empty;

        }

        public Anamnesis(Guid id, string complaint, DateTime startDate, string story, Guid protocolId)
        {
            Id = id;
            SetComplaint(complaint);
            SetStartDate(startDate);
            SetStory(story);
            SetProtocolId(protocolId);
        }

        public void SetComplaint(string complaint)
        {
            Check.NotNullOrWhiteSpace(complaint, nameof(complaint));
            Complaint = complaint;
        }

        public void SetStartDate(DateTime startDate)
        {
            Check.NotNull(startDate, nameof(startDate));
            StartDate = startDate;
        }

        public void SetStory(string story)
        {
            Check.NotNullOrWhiteSpace(story, nameof(story));
            Story = story;
        }

        public void SetProtocolId(Guid protocolId)
        {
            Check.NotNull(protocolId, nameof(protocolId));
            ProtocolId = protocolId;
        }
    }
}
