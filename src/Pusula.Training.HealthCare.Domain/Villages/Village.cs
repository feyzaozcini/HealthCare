using Pusula.Training.HealthCare.Districts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp;
using JetBrains.Annotations;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Countries;

namespace Pusula.Training.HealthCare.Villages
{
    public class Village : AuditedEntity<Guid>
    {
        [NotNull]
        public virtual string Name { get; private set; }
        public virtual Guid DistrictId { get; private set; }

        protected Village()
        {
            Name = string.Empty;
        }

        public Village(Guid id, Guid districtId, string name)
        {
            Id = id;
            SetName(name);
            SetDistrictId(districtId);
        }

        public void SetName(string name)
        {
            Check.NotNull(name, nameof(name));
            Check.Length(name, nameof(name), VillageConsts.NameMaxLength, 0);
            Name = name;
        }

        public void SetDistrictId(Guid districtId) => DistrictId = Check.NotNull(districtId, nameof(districtId));
    }
}
