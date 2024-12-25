using JetBrains.Annotations;
using Pusula.Training.HealthCare.PainTypes;
using Pusula.Training.HealthCare.Protocols;
using Pusula.Training.HealthCare.TestGroupItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.PainDetails
{
    public class PainDetail : AuditedEntity<Guid>
    {
        [NotNull]
        public string Area { get; set; } = string.Empty;

        public Guid ProtocolId { get; set; }

        public Protocol Protocol { get; set; } = null!;

        [NotNull]
        public int Score { get; set; }
        public Guid PainTypeId { get; set; }

        public PainType PainType { get; set; } = null!;

        public string? Description { get; set; }

        public PainRhythm PainRhythm { get; set; }

        public string? OtherPain { get; set; }

    
        public DateTime StartDate { get; set; }

        protected PainDetail() 
        {
        }

        public PainDetail(Guid id,string area,Guid protocolId,int score,Guid painTypeId,string? description,PainRhythm painRhythm,string? otherPain,DateTime startDate)
        {
            Id = id;
            SetArea(area);
            SetProtocolId(protocolId);
            SetScore(score);
            SetPainTypeId(painTypeId);
            SetDescription(description);
            SetPainRhythm(painRhythm);
            SetOtherPain(otherPain);
            SetStartDate(startDate);
        }

        public void SetArea(string area)
        {
            Area = area;
        }

        public void SetProtocolId(Guid protocolId)
        {
            ProtocolId = Check.NotNull(protocolId, nameof(protocolId));
        }

        public void SetScore(int score)
        {
            Score = Check.NotNull(score, nameof(score)); ;
        }

        public void SetPainTypeId(Guid painTypeId)
        {
            PainTypeId = Check.NotNull(painTypeId, nameof(painTypeId));
        }

        public void SetDescription(string? description)
        {
            if (!string.IsNullOrEmpty(description))
            {
                Check.Length(description, nameof(description), PainDetailConsts.DescriptionMaxLength);
            }
          
            Description = description;
        }

        public void SetPainRhythm(PainRhythm painRhythm)
        {
            PainRhythm = painRhythm;
        }

        public void SetOtherPain(string? otherPain)
        {
            if (!string.IsNullOrEmpty(otherPain))
            {
                Check.Length(otherPain, nameof(otherPain), PainDetailConsts.OtherPainMaxLength);
            }
            OtherPain = otherPain;
        }

        public void SetStartDate(DateTime startDate)
        {
            StartDate = startDate;
        }
    }
}
