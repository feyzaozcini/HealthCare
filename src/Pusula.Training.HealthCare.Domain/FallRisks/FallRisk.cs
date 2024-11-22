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
        public Guid ProtocolId { get;  set; }

        public int? Score { get;  set; } // Düşme riski skoru (1-10 arasında)
        public string? Description { get;  set; } // Düşme riski açıklaması
        public bool HasFallHistory { get;  set; } // Düşme hikayesi var mı?
        public bool UsesMedications { get;  set; } // İlaç kullanımı var mı?
        public bool HasAddiction { get;  set; } // Alkol veya madde bağımlılığı var mı?
        public bool HasBalanceDisorder { get;  set; } // Denge bozukluğu var mı?
        public bool HasVisionImpairment { get;  set; } // Görme bozukluğu var mı?

        public bool MentalState { get;  set; } // true = Oryante, false = Oryante Değil
        public bool GeneralHealthState { get;  set; } // true = Normal, false = Normal Değil

        public bool IsDeleted { get; set; }

        protected FallRisk()
        {
            Description = string.Empty;
           
        }

        public FallRisk(Guid id, int? score, Guid protocolId, string? description, bool hasFallHistory, bool usesMedications, bool hasAddiction, bool hasBalanceDisorder, bool hasVisionImpairment, bool mentalState, bool generalHealthState)
        {
            Id = id;
            Score = score;
            ProtocolId = protocolId;
            Description = description;
            HasFallHistory = hasFallHistory;
            UsesMedications = usesMedications;
            HasAddiction = hasAddiction;
            HasBalanceDisorder = hasBalanceDisorder;
            HasVisionImpairment = hasVisionImpairment;
            MentalState = mentalState;
            GeneralHealthState = generalHealthState;
        }
    }
}
