using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.PainTypes
{
    public class PainTypeDto : EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
