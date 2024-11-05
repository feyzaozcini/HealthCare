using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.PatientCompanies
{
    public class PatientCompanyExcelDownloadDto
    {
        public string DownloadToken { get; set; } = null!;
        public string? FilterText { get; set; }

        public string? Name { get; set; }

        public PatientCompanyExcelDownloadDto()
        {
        }
    }
}
