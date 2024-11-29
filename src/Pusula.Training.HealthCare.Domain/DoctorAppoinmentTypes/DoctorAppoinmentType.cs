using Pusula.Training.HealthCare.AppointmentTypes;
using Pusula.Training.HealthCare.Doctors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.DoctorAppoinmentTypes
{
    public class DoctorAppoinmentType : Entity
    {
        public Guid DoctorId { get; set; }
        public Guid AppoinmentTypeId { get; set; }

        public Doctor Doctor { get; set; }
        public AppointmentType AppoinmentType { get; set; }

        private DoctorAppoinmentType()
        {
        }

        public DoctorAppoinmentType(Guid doctorId, Guid appoinmentTypeId)
        {
            DoctorId = doctorId;
            AppoinmentTypeId = appoinmentTypeId;
        }

        public override object?[] GetKeys()
        {
            return new object?[] { DoctorId, AppoinmentTypeId };
        }
    }
}
