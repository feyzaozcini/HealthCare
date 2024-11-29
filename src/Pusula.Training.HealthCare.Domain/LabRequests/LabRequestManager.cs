using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.LabRequests;

public class LabRequestManager(ILabRequestRepository labRequestRepository) : DomainService
{
    public virtual async Task<LabRequest> CreateAsync(
        Guid protocolId,
        Guid doctorId,
        DateTime date,
        RequestStatusEnum status,
        string? description
        )
    {
       
        var labRequest = new LabRequest(
         GuidGenerator.Create(),
         protocolId,
         doctorId,
         date,
         status,
         description
        );

        return await labRequestRepository.InsertAsync(labRequest);
    }

    public virtual async Task<LabRequest> UpdateAsync(
        Guid id,
        Guid protocolId,
        Guid doctorId,
        DateTime date,
        RequestStatusEnum status,
        string? description
        )
    {
        var labRequest = await labRequestRepository.GetAsync(id);

        labRequest.SetProtocolId(protocolId);
        labRequest.SetDoctorId(doctorId);
        labRequest.SetDate(date);
        labRequest.SetRequestStatus(status);
        labRequest.SetDescription(description);

        return await labRequestRepository.UpdateAsync(labRequest);
    }
}
