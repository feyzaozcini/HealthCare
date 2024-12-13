using JetBrains.Annotations;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Insurances;
using Pusula.Training.HealthCare.Notes;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.ProtocolTypes;
using Pusula.Training.HealthCare.TestProcesses;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Protocols;

public class Protocol : FullAuditedAggregateRoot<Guid>
{

    [NotNull]
    public virtual DateTime StartTime { get; private set; }
    public virtual DateTime? EndTime { get; private set; }
    [NotNull]
    public virtual int No {  get; set; }
    public virtual ProtocolStatus ProtocolStatus { get; private set; }
    [NotNull]
    public virtual Guid ProtocolTypeId { get; private set; }
    public ProtocolType ProtocolType { get; private set; }
    public virtual Guid? ProtocolNoteId { get; private set; }
    public Note? Note { get; private set; }
    [NotNull]
    public virtual Guid ProtocolInsuranceId { get; private set; }
    public Insurance Insurance { get; private set; }
    [NotNull]
    public virtual Guid PatientId { get; private set; }
    public Patient Patient { get; private set; }
    [NotNull]
    public virtual Guid DepartmentId { get; private set; }
    public Department Department { get; private set; }
    [NotNull]
    public virtual Guid DoctorId { get; private set; }
    public Doctor Doctor { get; private set; }

    protected Protocol()
    {
        StartTime = DateTime.Now;
        EndTime = DateTime.Now.AddDays(1);
    }

    public Protocol(Guid id, DateTime startTime, DateTime? endTime, ProtocolStatus protocolStatus, Guid protocolTypeId, Guid? protocolNoteId, Guid protocolInsuranceId,
        Guid patientId, Guid departmentId, Guid doctorId) : base(id)
    {
        
        Id = id;
        SetStartTime(startTime);
        SetEndTime(endTime);
        SetProtocolStatus(protocolStatus);
        SetProtocolTypeId(protocolTypeId);
        SetProtocolNoteId(protocolNoteId);
        SetProtocolInsuranceId(protocolInsuranceId);
        SetPatientId(patientId);
        SetDepartmentId(departmentId);
        SetDoctorId(doctorId);
    }

    public void SetStartTime(DateTime startTime) => StartTime = Check.NotNull(startTime, nameof(startTime));

    public void SetEndTime(DateTime? endTime) => EndTime = endTime;

    public void SetProtocolStatus(ProtocolStatus protocolStatus) => ProtocolStatus = protocolStatus;
    public void SetProtocolTypeId(Guid protocolTypeId) => ProtocolTypeId = Check.NotNull(protocolTypeId, nameof(protocolTypeId));
    
    public void SetProtocolNoteId(Guid? protocolNoteId) => ProtocolNoteId = protocolNoteId;

    public void SetProtocolInsuranceId(Guid protocolInsuranceId) => ProtocolInsuranceId = Check.NotNull(protocolInsuranceId, nameof(protocolInsuranceId));

    public void SetPatientId(Guid patientId) => PatientId = Check.NotNull(patientId, nameof(patientId));

    public void SetDepartmentId(Guid departmentId) => DepartmentId = Check.NotNull(departmentId, nameof(departmentId));

    public void SetDoctorId(Guid doctorId) => DoctorId = Check.NotNull(doctorId, nameof(doctorId));

}