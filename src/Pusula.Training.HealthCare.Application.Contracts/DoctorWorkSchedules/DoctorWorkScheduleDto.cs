﻿using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.DoctorWorkSchedules
{
    public class DoctorWorkScheduleDto : Entity<Guid>
    {
        [Required]
        public virtual Guid DoctorId { get; set; }

        [Required]
        public virtual int[] WorkingDays { get; set; }

        [Required]
        public virtual string StartHour { get; set; }

        [Required]
        public virtual string EndHour { get; set; }

        public DoctorWorkScheduleDto()
        {
        }
    }
}
