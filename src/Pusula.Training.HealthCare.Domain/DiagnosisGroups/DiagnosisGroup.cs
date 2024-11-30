using JetBrains.Annotations;
using Pusula.Training.HealthCare.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.DiagnosisGroups
{
    public class DiagnosisGroup : Entity<Guid>, ISoftDelete
    {
        [NotNull]
        public virtual string Code { get; set; }

        [NotNull]
        public virtual string Name { get; set; }

        public bool IsDeleted { get; set; } //= false;

        protected DiagnosisGroup()
        {
            Code = string.Empty;
            Name = string.Empty;
        }

        public DiagnosisGroup(Guid id, string code, string name) : base(id)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), DiagnosisGroupConsts.CodeMaxLength, DiagnosisGroupConsts.CodeMinLength);

            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), DiagnosisGroupConsts.NameMaxLength, DiagnosisGroupConsts.NameMinLength);
            Id = id;
            Code = code;
            Name = name;
        }
    }
}
