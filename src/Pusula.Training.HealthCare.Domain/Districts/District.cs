using Pusula.Training.HealthCare.Cities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp;
using JetBrains.Annotations;

namespace Pusula.Training.HealthCare.Districts
{
    public class District : AuditedEntity<Guid>
    {
        [NotNull]
        public virtual string Name { get; set; }
        public virtual Guid CityId { get; set; }

        protected District()
        {
            Name = string.Empty;
        }

        public District(Guid id, Guid cityId, string name)
        {
            Id = id;
            Check.NotNull(name, nameof(name));
            Check.Length(name, nameof(name), DistrictConsts.NameMaxLength, 0);
            Name = name;

            CityId = cityId;
        }
    }
}
