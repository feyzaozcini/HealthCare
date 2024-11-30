using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;

namespace Pusula.Training.HealthCare.Diagnoses
{
    public class DiagnosisDataSeeder(IDiagnosisRepository DiagnosisRepository,
        IGuidGenerator GuidGenerator) : ITransientDependency
    {
        public async Task SeedDiagnosesAsync(List<Guid> diagnosisGroupKeys)
        {
            // Mevcut veri kontrolü
            if (await DiagnosisRepository.GetCountAsync() > 0)
            {
                return;
            }

            // Diagnosis seed verileri
            var diagnoses = new List<Diagnosis>
        {
            new Diagnosis(GuidGenerator.Create(), "A00.0", "Kolera, Vibrio cholerae 01, biovar koleraya bağlı", diagnosisGroupKeys[0]),
            new Diagnosis(GuidGenerator.Create(), "A00.1", "Kolera, Vibrio cholerae 01, biovar eltor'a bağlı", diagnosisGroupKeys[0]),
            new Diagnosis(GuidGenerator.Create(), "A00.9", "Kolera, tanımlanmamış", diagnosisGroupKeys[0]),

            new Diagnosis(GuidGenerator.Create(), "A01.0", "Tifo", diagnosisGroupKeys[1]),
            new Diagnosis(GuidGenerator.Create(), "A01.1", "Parafito A", diagnosisGroupKeys[1]),
            new Diagnosis(GuidGenerator.Create(), "A01.2", "Parafito B", diagnosisGroupKeys[1]),
            new Diagnosis(GuidGenerator.Create(), "A01.3", "Parafito C", diagnosisGroupKeys[1]),
            new Diagnosis(GuidGenerator.Create(), "A01.4", "Parafito, tanımlanmamış", diagnosisGroupKeys[1])
        };

            // Veritabanına ekleme
            await DiagnosisRepository.InsertManyAsync(diagnoses, autoSave: true);
        }
    }
}
