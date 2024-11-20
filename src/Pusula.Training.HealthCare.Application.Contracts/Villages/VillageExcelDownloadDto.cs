using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Villages
{
    public class VillageExcelDownloadDto
    {
        public string DownloadToken { get; set; } = null!;

        public string? FilterText { get; set; }

        public string? Name { get; set; }

        public VillageExcelDownloadDto()
        {
        }
    }
}
