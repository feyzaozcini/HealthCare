using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.BlackLists
{
    public class BlackListManager(IBlackListRepository blackListRepository) : DomainService
    {
        public virtual async Task<BlackList> CreateAsync(
            Guid patientId, Guid doctorId, BlackListStatus blackListStatus,string note)
        {
            Check.NotNull(patientId, nameof(patientId));
            Check.NotNull(doctorId, nameof(doctorId));
            Check.NotNull(note, nameof(note));

            var blackList = new BlackList(
                GuidGenerator.Create(),
                blackListStatus,
                note,
                patientId,
                doctorId
            );

            return await blackListRepository.InsertAsync(blackList);
        }

        public virtual async Task<BlackList> UpdateAsync(
            Guid id,
            Guid patientId,
            Guid doctorId,
            BlackListStatus blackListStatus,
            string note
        )
        {
            var blackList = await blackListRepository.GetAsync(id);
            blackList.SetBlackListStatus(blackListStatus);
            blackList.SetNote(note);
            blackList.SetPatientId(patientId);
            blackList.SetDoctorId(doctorId);

            return await blackListRepository.UpdateAsync(blackList);
        }
}
}
