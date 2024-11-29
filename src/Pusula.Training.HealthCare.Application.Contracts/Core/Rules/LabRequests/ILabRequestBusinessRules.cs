using Pusula.Training.HealthCare.LabRequests;
using System;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Core.Rules.LabRequests;

public interface ILabRequestBusinessRules : IBusinessRules
{
    //Task ValidateLabRequestAsync(Guid protocolId, Guid testItemId);
    //void ValidateStatusChange(LabRequestDto labRequest, string newStatus);
    //void ValidateRequestDeletion(LabRequestDto labRequest);
}
