using Pusula.Training.HealthCare.LabRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Pusula.Training.HealthCare.Core.Rules.LabRequests;

public class LabRequestBusinessRules : ILabRequestBusinessRules
{
    public Task ValidateLabRequestAsync(Guid protocolId, Guid testItemId)
    {
        throw new NotImplementedException();
    }

    public void ValidateRequestDeletion(LabRequestDto labRequest)
    {
        if (labRequest.Status != RequestStatusEnum.Pending)
        {
            //Globale çekilecek.
            throw new BusinessException("Bu tetkik talebi silinemez. Yalnızca bekleme durumundaki talepler silinebilir.");
        }
    }

    public void ValidateStatusChange(LabRequestDto labRequest, string newStatus)
    {
        throw new NotImplementedException();
    }
}
