using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.PainTypes
{
    public class PainType : Entity<Guid>, IDeletionAuditedObject
    {
        [NotNull]
        public string Name { get; private set; } = string.Empty;

        public Guid? DeleterId { get; set; }

        public DateTime? DeletionTime { get; set; }

        public bool IsDeleted { get; set; }

        protected PainType() 
        {
            Name = string.Empty;
        }

        public PainType(Guid id, string name)
        {
            Id = id;
            SetName(name);
            
        }

        public void SetName(string name) => Name = Check.NotNull(name, nameof(name));
    }
}
