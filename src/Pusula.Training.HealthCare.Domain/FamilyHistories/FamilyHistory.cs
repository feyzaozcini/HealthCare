using JetBrains.Annotations;
using Pusula.Training.HealthCare.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.FamilyHistories
{
    public class FamilyHistory :FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public Guid PatientId { get; private set; }

        public Patient Patient { get; private set; } = null!;

        public string? Mother { get; private set; }

        public string? Father { get; private set; }

        public string? Sister { get; private set; }

        public string? Brother { get; private set; }

        public string? Other { get; private set; }

        public bool IsParentsRelative { get; private set; }

        protected FamilyHistory()
        {
            Mother = string.Empty;
            Father = string.Empty;
            Sister = string.Empty;
            Brother = string.Empty;
            Other = string.Empty;
        }

        public FamilyHistory(Guid id, Guid patientId, string? mother, string? father, string? sister, string? brother, string? other, bool isParentsRelative)
        {
            Id = id;
            SetPatientId(patientId);
            SetMother(mother);
            SetFather(father);
            SetSister(sister);
            SetBrother(brother);
            SetOther(other);
            SetIsParentsRelative(isParentsRelative);
        
        }

        public void SetPatientId(Guid patientId)
        {
            Check.NotNull(patientId, nameof(patientId));
            PatientId = patientId;
        }
        public void SetMother(string? mother)
        {
            Mother = mother;
        }

        public void SetFather(string? father)
        {
            Father = father;
        }

        public void SetSister(string? sister)
        {
            Sister = sister;
        }

        public void SetBrother(string? brother)
        {
            Brother = brother;
        }

        public void SetOther(string? other)
        {
            Other = other;
        }

        public void SetIsParentsRelative(bool isParentsRelative)
        {
            IsParentsRelative = isParentsRelative;
        }
    }
}
