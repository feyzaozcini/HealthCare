using Pusula.Training.HealthCare.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp;
using JetBrains.Annotations;
using Pusula.Training.HealthCare.Cities;

namespace Pusula.Training.HealthCare.DepartmentServices
{
    public class DepartmentService : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Name { get; private set; }

        protected DepartmentService()
        {
            Name = string.Empty;
        }

        public DepartmentService(Guid id, string name)
        {
            Id = id;
            SetName(name);
        }

        public void SetName(string name)
        {
            Check.NotNull(name, nameof(name));
            Check.Length(name, nameof(name), DepartmentServiceConsts.NameMaxLength, 0);
            Name = name;
        }
    }
}
