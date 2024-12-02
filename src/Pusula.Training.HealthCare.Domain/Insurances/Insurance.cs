using Pusula.Training.HealthCare.ProtocolTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp;
using JetBrains.Annotations;

namespace Pusula.Training.HealthCare.Insurances
{
    public class Insurance : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Name { get; private set; }

        protected Insurance()
        {
            Name = string.Empty;
        }

        public Insurance(Guid id, string name)
        {
            Id = id;
            SetName(name);
        }
        public void SetName(string name)
        {
            Check.NotNull(name, nameof(name));
            Check.Length(name, nameof(name), InsuranceConsts.NameMaxLength, 0);
            Name = name;
        }
    }
}
