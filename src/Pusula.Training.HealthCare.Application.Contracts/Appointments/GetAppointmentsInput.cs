﻿using System;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Appointments
{
    public class GetAppointmentsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Note { get; set; }
        public AppointmentStatus? AppointmentStatus { get; set; }
        public bool? IsBlock { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? DoctorId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? AppointmentTypeId { get; set; }

        public GetAppointmentsInput()
        {
            
        }
    }
}
