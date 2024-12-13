using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Pusula.Training.HealthCare.Exceptions
{
    public class HealthCareException : IHealthCareException
    {
        public const string DEFAULT_ERROR_MESSAGE = "An unexpected error occurred";

        public const string DEFAULT_ERROR_CODE = "HCE-500";


        public static void Throw(string? message) => ThrowIf(message,true);

        #region THROW IF

        public static void ThrowIf(bool condition = true) => ThrowIf(default, condition);

        public static void ThrowIf(string? message, bool condition = true) => ThrowIf(message, default, condition);

        public static void ThrowIf(string? message, string? code, bool condition = true)
        {
            if (condition)
                ThrowException(message, code);
        }

        #endregion
        public static void ThrowIfNull(object? obj, string message) => ThrowIf(message, obj == null);
        private static void ThrowException(string? message, string? code) => throw new UserFriendlyException(message ?? DEFAULT_ERROR_MESSAGE, code ?? DEFAULT_ERROR_CODE);
    }
}