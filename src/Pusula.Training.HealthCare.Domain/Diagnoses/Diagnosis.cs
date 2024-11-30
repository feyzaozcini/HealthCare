using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Diagnoses
{
    public class Diagnosis : Entity<Guid>, ISoftDelete
    {
        [NotNull]
        public virtual string Code { get; set; } 

        [NotNull]
        public virtual string Name { get; set; } 

        public Guid GroupId { get; set; }

        public bool IsDeleted { get; set; }

        protected Diagnosis()
        {
            Code = string.Empty;
            Name = string.Empty;
        }

        public Diagnosis(Guid id, string code, string name, Guid groupId) : base(id)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), DiagnosisConsts.CodeMaxLength, DiagnosisConsts.CodeMinLength);

            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), DiagnosisConsts.NameMaxLength, DiagnosisConsts.NameMinLength);
            Id = id;
            Code = code;
            Name = name;
            GroupId = groupId;
        }
    }
}
