﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.Appointments
{
    public class AppointmentUpdateDto
    {
        [Required] 
        public Guid Id { get; set; } = default!;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [StringLength(AppointmentConst.NoteMaxLength)]
        public string Note { get; set; } = null!;

        [Required]
        public AppointmentStatus AppointmentStatus { get; set; }

        public bool IsBlock { get; set; }

        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid AppointmentTypeId { get; set; }
    }
}
