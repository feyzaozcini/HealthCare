using Pusula.Training.HealthCare.Districts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp;
using JetBrains.Annotations;

namespace Pusula.Training.HealthCare.Villages
{
    public class Village : AuditedEntity<Guid>
    {
        [NotNull]
        public virtual string Name { get; set; }
        public virtual Guid DistrictId { get; set; }

        protected Village()
        {
            Name = string.Empty;
        }

        public Village(Guid id, Guid districtId, string name)
        {
            Id = id;
            Check.NotNull(name, nameof(name));
            Check.Length(name, nameof(name), VillageConsts.NameMaxLength, 0);
            Name = name;

            DistrictId = districtId;
        }
    }
}
