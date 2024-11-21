using Pusula.Training.HealthCare.Anamneses;
using Pusula.Training.HealthCare.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.FallRisks
{
    public class FallRiskManager(IFallRiskRepository fallRiskRepository) : DomainService
    {
        public virtual async Task<FallRisk> CreateAsync(int? score, Guid protocolId, string? description, 
            bool hasFallHistory, bool usesMedications, bool hasAddiction, bool hasBalanceDisorder, 
            bool hasVisionImpairment, bool mentalState, bool generalHealthState)
        {
            var fallRisk = new FallRisk(Guid.NewGuid(),score,protocolId,description,hasFallHistory,usesMedications,hasAddiction,hasBalanceDisorder,hasVisionImpairment,mentalState,generalHealthState);
            return await fallRiskRepository.InsertAsync(fallRisk);
        }

        public virtual async Task<FallRisk> UpdateAsync(Guid id,int? score, Guid protocolId, string? description,
            bool hasFallHistory, bool usesMedications, bool hasAddiction, bool hasBalanceDisorder, 
            bool hasVisionImpairment, bool mentalState, bool generalHealthState)
        {
            var fallRisk = await fallRiskRepository.GetAsync(id);

            fallRisk.Score = score;
            fallRisk.ProtocolId = protocolId;
            fallRisk.Description = description;
            fallRisk.HasFallHistory = hasFallHistory;
            fallRisk.UsesMedications = usesMedications;
            fallRisk.HasAddiction = hasAddiction;
            fallRisk.HasBalanceDisorder = hasBalanceDisorder;
            fallRisk.HasVisionImpairment = hasVisionImpairment;
            fallRisk.MentalState = mentalState;
            fallRisk.GeneralHealthState = generalHealthState;

            return await fallRiskRepository.UpdateAsync(fallRisk);
        }
    }
}
