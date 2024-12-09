using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.PainDetails
{
    public class PainDetailManager(IPainDetailRepository painDetailRepository) : DomainService
    {
        public virtual async Task<PainDetail> CreateAsync(
        string area,
        Guid protocolId,
        int score,
        Guid painTypeId,
        string? description,
        PainRhythm painRhythm,
        string? otherPain,
        DateTime startDate)
        {
            var painDetail = new PainDetail(GuidGenerator.Create(), area,protocolId,score, painTypeId,  description, painRhythm,
            otherPain,startDate);

            return await painDetailRepository.InsertAsync(painDetail);
        }


        public virtual async Task<PainDetail> UpdateAsync(
        Guid id,
        string area,
        Guid protocolId,
        int score,
        Guid painTypeId,
        string? description,
        PainRhythm painRhythm,
        string? otherPain,
        DateTime startDate)
        {
            var painDetail = await painDetailRepository.GetAsync(id);
            painDetail.SetArea(area);
            painDetail.SetProtocolId(protocolId);
            painDetail.SetScore(score);
            painDetail.SetPainTypeId(painTypeId);
            painDetail.SetDescription(description);
            painDetail.SetPainRhythm(painRhythm);
            painDetail.SetOtherPain(otherPain);
            painDetail.SetStartDate(startDate);

            return await painDetailRepository.UpdateAsync(painDetail);
        }
    }
}
