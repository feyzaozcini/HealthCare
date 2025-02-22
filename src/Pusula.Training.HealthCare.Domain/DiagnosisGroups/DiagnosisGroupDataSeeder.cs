﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;

namespace Pusula.Training.HealthCare.DiagnosisGroups
{
    public class DiagnosisGroupDataSeeder(IDiagnosisGroupRepository DiagnosisGroupRepository,
        IGuidGenerator GuidGenerator) : ITransientDependency
    {
        public async Task<List<Guid>> SeedDiagnosisGroupsAsync()
        {
          
            if (await DiagnosisGroupRepository.GetCountAsync() > 0)
            {
                
                return new List<Guid>();
            }

           
            var diagnosisGroups = new List<DiagnosisGroup>
        {
            new DiagnosisGroup(GuidGenerator.Create(), "A00", "Kolera"),
            new DiagnosisGroup(GuidGenerator.Create(), "A01", "Tifo ve Parafito")
        };

          
            await DiagnosisGroupRepository.InsertManyAsync(diagnosisGroups, autoSave: true);

            // Oluşturulan ID'leri döndür
            return diagnosisGroups.Select(dg => dg.Id).ToList();
        }
    }
}
