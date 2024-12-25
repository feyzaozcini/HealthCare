using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.FallRisks
{
    public class FallRisk : Entity<Guid>, ISoftDelete
    {
        public Guid ProtocolId { get; private set; }

        public int? Score { get;  private set; } // Düşme riski skoru (1-10 arasında)
        public string? Description { get;  private set; } 
        public bool HasFallHistory { get;  private set; } // Düşme hikayesi var mı?
        public bool UsesMedications { get; private set; } // İlaç kullanımı var mı?
        public bool HasAddiction { get;  private set; } // Alkol veya madde bağımlılığı var mı?
        public bool HasBalanceDisorder { get; private set; } // Denge bozukluğu var mı?
        public bool HasVisionImpairment { get;  private set; } // Görme bozukluğu var mı?

        public bool MentalState { get;  private set; } // true = Oryante, false = Oryante Değil
        public bool GeneralHealthState { get; private set; } // true = Normal, false = Normal Değil

        public bool IsDeleted { get; set; }

        protected FallRisk()
        {
            Description = string.Empty;
           
        }

        public FallRisk(Guid id, int? score, Guid protocolId, string? description, bool hasFallHistory, bool usesMedications, bool hasAddiction, bool hasBalanceDisorder, bool hasVisionImpairment, bool mentalState, bool generalHealthState)
        {
            Id = id;
            SetScore(score);
            SetProtocolId(protocolId);
            SetDescription(description);
            SetHasFallHistory(hasFallHistory);
            SetUsesMedications(usesMedications);
            SetHasAddiction(hasAddiction);
            SetHasBalanceDisorder(hasBalanceDisorder);
            SetHasVisionImpairment(hasVisionImpairment);
            SetMentalState(mentalState);
            SetMentalState(mentalState);
        }

        public void SetScore(int? score)
        {
            if (score.HasValue)
            {
                Check.Range(score.Value, nameof(score), 1, 10); 
            }

            Score = score;
        }

        public void SetProtocolId(Guid protocolId)
        {
            Check.NotNull(protocolId, nameof(protocolId));
            ProtocolId = protocolId;
        }

        public void SetDescription(string? description)
        {
            Description = description;
        }

        public void SetHasFallHistory(bool hasFallHistory)
        {
            HasFallHistory = hasFallHistory;
        }

        public void SetUsesMedications(bool usesMedications)
        {
            UsesMedications = usesMedications;
        }

        public void SetHasAddiction(bool hasAddiction)
        {
            HasAddiction = hasAddiction;
        }

        public void SetHasBalanceDisorder(bool hasBalanceDisorder)
        {
            HasBalanceDisorder = hasBalanceDisorder;
        }

        public void SetHasVisionImpairment(bool hasVisionImpairment)
        {
            HasVisionImpairment = hasVisionImpairment;
        }

        public void SetMentalState(bool mentalState)
        {
            MentalState = mentalState;
        }

        public void SetGeneralHealthState(bool generalHealthState)
        {
            GeneralHealthState = generalHealthState;
        }
    }
}
