using JetBrains.Annotations;
using Pusula.Training.HealthCare.ProtocolTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;

namespace Pusula.Training.HealthCare.Insurances
{
    public class InsuranceManager(IInsuranceRepository insuranceRepository) : DomainService
    {
        public virtual async Task<Insurance> CreateAsync(
        string name)
        {

            var insurance = new Insurance(
             GuidGenerator.Create(),
             name
             );

            insurance.SetName(name);

            return await insuranceRepository.InsertAsync(insurance);
        }

        public virtual async Task<Insurance> UpdateAsync(
            Guid id,
            string name, [CanBeNull] string? concurrencyStamp = null
        )
        {

            var insurance = await insuranceRepository.GetAsync(id);
            insurance.SetName(name);
            insurance.SetConcurrencyStampIfNotNull(concurrencyStamp);

            return await insuranceRepository.UpdateAsync(insurance);
        }

    }
}
