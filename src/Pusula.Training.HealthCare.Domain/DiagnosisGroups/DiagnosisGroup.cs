using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.DiagnosisGroups
{
    public class DiagnosisGroup : Entity<Guid>, ISoftDelete
    {
        [NotNull]
        public virtual string Code { get; private set; } = string.Empty;

        [NotNull]
        public virtual string Name { get; private set; } = string.Empty;

        public bool IsDeleted { get; set; }

        protected DiagnosisGroup()
        {
            Code = string.Empty;
            Name = string.Empty;
        }

        public DiagnosisGroup(Guid id, string code, string name)
        {
            Id = id;
            SetCode(code);
            SetName(name);
        }

        public void SetCode(string code)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), DiagnosisGroupConsts.CodeMaxLength, DiagnosisGroupConsts.CodeMinLength);
            Code = code;
        }

        public void SetName(string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), DiagnosisGroupConsts.NameMaxLength, DiagnosisGroupConsts.NameMinLength);
            Name = name;
        }
    }
}
