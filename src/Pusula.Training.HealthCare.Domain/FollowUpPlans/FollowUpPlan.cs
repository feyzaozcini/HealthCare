using JetBrains.Annotations;
using Pusula.Training.HealthCare.Protocols;
using Pusula.Training.HealthCare.TestGroupItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.FollowUpPlans
{
    public class FollowUpPlan : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public Guid ProtocolId { get; set; }

        public Protocol Protocol { get; set; } = null!;

        public string? Note { get; set; }

        [NotNull]

        public FollowUpType FollowUpType { get; set; }

        [NotNull]
        public bool IsSurgeryScheduled { get; set; }

        protected FollowUpPlan() { }

        public FollowUpPlan(Guid id,Guid protocolId, string? note, FollowUpType followUpType, bool isSurgeryScheduled)
        {
            Id = id;
            SetProtocolId(protocolId);
            SetNote(note);
            SetFollowUpType(followUpType);
            SetIsSurgeryScheduled(isSurgeryScheduled);
        }

        public void SetProtocolId(Guid protocolId) => ProtocolId = Check.NotNull(protocolId, nameof(protocolId));

        public void SetNote(string? note)
        {
            if (!string.IsNullOrEmpty(note))
            {
                Check.Length(note, nameof(note), FollowUpPlanConsts.NoteMaxLength);
            }
            Note = note;
        }

        public void SetFollowUpType(FollowUpType followUpType)
        {
            FollowUpType = Check.NotNull(followUpType, nameof(followUpType));
        }

        public void SetIsSurgeryScheduled(bool isSurgeryScheduled)
        {
            IsSurgeryScheduled = Check.NotNull(isSurgeryScheduled, nameof(isSurgeryScheduled));
        }

    }
}
