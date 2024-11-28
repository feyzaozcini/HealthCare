using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp;
using JetBrains.Annotations;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Cities;

namespace Pusula.Training.HealthCare.Titles
{
    public class Title : AuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Name { get; private set; }

        //public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>(); // Navigasyon Özelliği
        protected Title()
        {
            Name = string.Empty;
        }

        public Title(Guid id, string name)
        {
            Id = id;
            SetName(name);
        }

        public void SetName(string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), TitleConsts.NameMaxLength, TitleConsts.NameMinLength);
            Name = name;
        }
    }
}
