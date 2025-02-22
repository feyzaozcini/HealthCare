﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.PatientHistories
{
    public class PatientHistoryConsts
    {
        private const string DefaultSorting = "{0}Disease asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "PatientHistory." : string.Empty);
        }
    }
}
