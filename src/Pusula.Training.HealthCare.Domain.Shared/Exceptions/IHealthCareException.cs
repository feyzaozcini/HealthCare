using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Pusula.Training.HealthCare.Exceptions
{
    public interface IHealthCareException: IUserFriendlyException, ISingletonDependency
    {

    }
}
