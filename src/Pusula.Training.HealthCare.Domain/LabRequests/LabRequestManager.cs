using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.LabRequests;

public class LabRequestManager(ILabRequestRepository labRequestRepository) : DomainService
{
    public virtual async Task<LabRequest> CreateAsync(
        Guid protocolId,
        Guid doctorId,
        Guid testGroupItemId,
        string name,
        DateTime date,
        RequestStatusEnum status
        )
    {
       
        var labRequest = new LabRequest(
         GuidGenerator.Create(),
         protocolId,
         doctorId,
         testGroupItemId,
         name,
         date,
         status
        );

        return await labRequestRepository.InsertAsync(labRequest);
    }

    public virtual async Task<LabRequest> UpdateAsync(
        Guid id,
        Guid protocolId,
        Guid doctorId,
        Guid testGroupItemId,
        string name,
        DateTime date,
        RequestStatusEnum status
        )
    {
        var labRequest = await labRequestRepository.GetAsync(id);

        labRequest.SetProtocolId(protocolId);
        labRequest.SetDoctorId(doctorId);
        labRequest.SetTestGroupItemId(testGroupItemId);
        labRequest.SetName(name);
        labRequest.SetDate(date);
        labRequest.SetRequestStatus(status);

        return await labRequestRepository.UpdateAsync(labRequest);
    }
}
