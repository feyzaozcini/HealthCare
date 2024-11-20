using Pusula.Training.HealthCare.DepartmentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp;
using JetBrains.Annotations;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Districts;

namespace Pusula.Training.HealthCare.Cities
{
    public class City : AuditedEntity<Guid>
    {
        [NotNull]
        public virtual string Name { get; set; }
        public virtual Guid CountryId { get; set; }

        protected City()
        {
            Name = string.Empty;
        }

        public City(Guid id, Guid countryId, string name)
        {
            Id = id;
            Check.NotNull(name, nameof(name));
            Check.Length(name, nameof(name), CityConsts.NameMaxLength, 0);
            Name = name;

            CountryId = countryId;
        }
    }
}
