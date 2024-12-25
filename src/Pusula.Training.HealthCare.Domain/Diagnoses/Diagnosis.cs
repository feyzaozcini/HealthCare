using JetBrains.Annotations;
using Pusula.Training.HealthCare.DiagnosisGroups;
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
        public virtual string Code { get; private set; } = string.Empty;

        [NotNull]
        public virtual string Name { get; private set; } = string.Empty;

        public Guid GroupId { get; private set; }

        public bool IsDeleted { get; set; }

        protected Diagnosis()
        {
            Code = string.Empty;
            Name = string.Empty;
        }

        public Diagnosis(Guid id, string code, string name, Guid groupId)
        {
           
            Id = id;
            SetCode(code);
            SetName(name);
            SetGroupId(groupId);
          
        }

        public void SetCode(string code)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), DiagnosisConsts.CodeMaxLength, DiagnosisConsts.CodeMinLength);
            Code = code;
        }

        public void SetName(string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), DiagnosisConsts.NameMaxLength, DiagnosisConsts.NameMinLength);
            Name = name;
        }

        public void SetGroupId(Guid groupId)
        {
            Check.NotNull(groupId, nameof(groupId));
            GroupId = groupId;
        }
    }
}
