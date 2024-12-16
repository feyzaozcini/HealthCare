using Pusula.Training.HealthCare.BlackLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Core.Rules.BlackLists
{
    public interface IBlackListBusinessRules:IBusinessRules
    {
        //Hasta ve doktorun engel durumunu kontrol eder
        Task ValidateBlackList(Guid patientId, Guid doctorId);
    }
}
