using JetBrains.Annotations;
using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.DepartmentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.ProtocolTypes
{
    public class ProtocolType : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Name { get; private set; }

        protected ProtocolType()
        {
            Name = string.Empty;
        }

        public ProtocolType(Guid id, string name)
        {
            Id = id;
            SetName(name);
        }
        public void SetName(string name)
        {
            Check.NotNull(name, nameof(name));
            Check.Length(name, nameof(name), ProtocolTypeConsts.NameMaxLength, 0);
            Name = name;
        }
    }
}
