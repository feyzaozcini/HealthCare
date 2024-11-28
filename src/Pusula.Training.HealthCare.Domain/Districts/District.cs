using Pusula.Training.HealthCare.Cities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp;
using JetBrains.Annotations;
using Pusula.Training.HealthCare.Countries;

namespace Pusula.Training.HealthCare.Districts
{
    public class District : AuditedEntity<Guid>
    {
        [NotNull]
        public virtual string Name { get; private set; }
        public virtual Guid CityId { get; private set; }

        protected District()
        {
            Name = string.Empty;
        }

        public District(Guid id, Guid cityId, string name)
        {
            Id = id;
            SetName(name);
            SetCityId(cityId);
        }

        public void SetName(string name)
        {
            Check.NotNull(name, nameof(name));
            Check.Length(name, nameof(name), DistrictConsts.NameMaxLength, 0);
            Name = name;
        }

        public void SetCityId(Guid cityId) => CityId = Check.NotNull(cityId, nameof(cityId));

    }
}
