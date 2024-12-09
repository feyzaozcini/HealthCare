using Pusula.Training.HealthCare.Exceptions;
using Pusula.Training.HealthCare.PainTypes;
using Pusula.Training.HealthCare.TestGroupItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Core.Rules.PainTypes
{
    public class PainTypeBusinessRules(IPainTypeRepository painTypeRepository) : IPainTypeBusinessRules
    {
        public async Task PainTypeNameDuplicatedAsync(string painTypeName)
        {
            var upperCased = painTypeName.ToUpper();
            HealthCareException.ThrowIf(HealthCareDomainErrorCodes.PainTypeAlreadyExist,
                await painTypeRepository.AnyAsync(ti => ti.Name == painTypeName));
        }
    }
}
