using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.PatientCompanies
{
    public class PatientCompany : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Name { get; private set; }

        protected PatientCompany() 
        {

        }

        public PatientCompany(Guid id,string name)
        {
            Id = id;
            SetName(name);
        }

        public void SetName(string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), PatientCompanyConsts.NameMaxLength, PatientCompanyConsts.NameMinLength);
            Name = name;
        }
    }
}
