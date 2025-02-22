﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.ProtocolTypes
{
    public static class ProtocolTypeConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "ProtocolType." : string.Empty);
        }

        public const int NameMaxLength = 128;

        public const string ProtocolTypeSuccessfullyCreated = "Protokol Tipi başarıyla oluşturuldu.";
        public const string ProtocolTypeSuccessfullyUpdated = "Protokol Tipi başarıyla güncellendi.";
        public const string ProtocolTypeSuccessfullyDeleted = "Protokol Tipi başarıyla silindi.";
    }
}
