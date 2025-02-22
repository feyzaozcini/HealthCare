﻿using Pusula.Training.HealthCare.BlackLists;
using Pusula.Training.HealthCare.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Core.Rules.BlackLists
{
    public class BlackListBusinessRules(IBlackListRepository blackListRepository) : IBlackListBusinessRules
    {
        //Hasta ve doktorun engel durumunu kontrol eder, birden fazla eklenmesini engeller
        public virtual async Task DublicateBlackList(Guid patientId, Guid doctorId)
        {
            var isBlackListed = await blackListRepository.AnyAsync(
            bl => bl.PatientId == patientId && bl.DoctorId == doctorId && bl.BlackListStatus == BlackListStatus.Block);

            if (isBlackListed)
            {
                HealthCareException.ThrowIf(
                    HealthCareDomainErrorCodes.DublicateBlackListItem
                );
            }
        }

        //Randevu için hasta ve doktorun engel durumunu kontrol eder
        public virtual async Task ValidateBlackList(Guid patientId, Guid doctorId)
        {
            var isBlackListed = await blackListRepository.AnyAsync(
            bl => bl.PatientId == patientId && bl.DoctorId == doctorId && bl.BlackListStatus == BlackListStatus.Block);

            if (isBlackListed)
            {
                HealthCareException.ThrowIf(
                    HealthCareDomainErrorCodes.PatientWasBlockedByDoctor
                );
            }
        }
    }

}
