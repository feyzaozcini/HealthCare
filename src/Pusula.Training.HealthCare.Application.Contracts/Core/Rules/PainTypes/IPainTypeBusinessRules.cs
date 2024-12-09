using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Core.Rules.PainTypes
{
    public interface IPainTypeBusinessRules : IBusinessRules
    {
        Task PainTypeNameDuplicatedAsync(string painTypeName);
    }
}
