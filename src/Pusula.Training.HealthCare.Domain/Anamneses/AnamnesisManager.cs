﻿using Pusula.Training.HealthCare.Diagnoses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Anamneses
{
    public class AnamnesisManager(IAnamnesisRepository anamnesisRepository ) : DomainService
    {
        public virtual async Task<Anamnesis> CreateAsync(string complaint, DateTime startDate, string story,  Guid protocolId)
        {
            var anamnesis = new Anamnesis(Guid.NewGuid(), complaint, startDate,story,protocolId);
            return await anamnesisRepository.InsertAsync(anamnesis);
        }

        public virtual async Task<Anamnesis> UpdateAsync(Guid id, string complaint, DateTime startDate, string story,  Guid protocolId)
        {
            var anamnesis = await anamnesisRepository.GetAsync(id);

            anamnesis.SetComplaint(complaint);
            anamnesis.SetStartDate(startDate);
            anamnesis.SetStory(story);
            anamnesis.SetProtocolId(protocolId);

            return await anamnesisRepository.UpdateAsync(anamnesis);
        }
    }
}
