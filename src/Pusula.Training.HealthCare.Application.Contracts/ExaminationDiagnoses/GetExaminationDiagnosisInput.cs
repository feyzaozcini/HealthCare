﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.ExaminationDiagnoses
{
    public class GetExaminationDiagnosisInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public DiagnosisType? DiagnosisType { get; set; }
        public DateTime? InitialDate { get; set; }
        public string? Note { get; set; } = null!;
        public Guid? ProtocolId { get; set; }
        public Guid? DiagnosisId { get; set; }

        public GetExaminationDiagnosisInput()
        {
        }
    }
}
