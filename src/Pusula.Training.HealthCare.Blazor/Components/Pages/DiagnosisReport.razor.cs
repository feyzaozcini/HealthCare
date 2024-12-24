using Pusula.Training.HealthCare.ExaminationDiagnoses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class DiagnosisReport
    {
        private List<DiagnosisCountDto> DiagnosisCounts { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            await LoadDiagnosisCountsAsync();
        }

        private async Task LoadDiagnosisCountsAsync()
        {
            var result = await ExaminationDiagnosisAppService.GetDiagnosisCountsAsync(new GetExaminationDiagnosisInput
            {
                MaxResultCount = 10, 
                SkipCount = 0
            });

            DiagnosisCounts = result.Items.ToList();
        }
    }
}