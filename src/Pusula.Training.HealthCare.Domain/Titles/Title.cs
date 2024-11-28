using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp;
using JetBrains.Annotations;
using Pusula.Training.HealthCare.Doctors;

namespace Pusula.Training.HealthCare.Titles
{
    public class Title : AuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Name { get; set; }


        public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>(); // Navigasyon Özelliği
        protected Title()
        {
            Name = string.Empty;
        }

        public Title(Guid id, string name)
        {
            Id = id;
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), TitleConsts.NameMaxLength, TitleConsts.NameMinLength);
            Name = name;
        }
    }
}
