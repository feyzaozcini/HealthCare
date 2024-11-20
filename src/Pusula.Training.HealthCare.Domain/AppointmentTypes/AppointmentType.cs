using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.AppointmentTypes
{
    public class AppointmentType : AuditedEntity<Guid>
    {
        [NotNull]
        public string Name { get; private set; } = null!; 

        protected AppointmentType()
        {
        }

        public AppointmentType(Guid id, string name)
        {
            Id = id;
            SetName(name);
        }

        public void SetName(string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), AppointmentTypeConst.NameMaxLength, AppointmentTypeConst.NameMinLength);
            Name = name;
        }
    }
}
