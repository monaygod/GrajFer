using System;
using System.Linq;
using Dapper;

namespace Infrastructure.ExtensionMethods
{
    public static class DapperDynamicParametersExtensions
    {
        public static string Set(this DynamicParameters parameters, object value, string key = "")
        {
            while (string.IsNullOrEmpty(key))
            {
                var tempKey = GenerateParameterName();
                if (!parameters.ParameterNames.Contains(tempKey))
                {
                    key = tempKey;
                }
            }

            parameters.Add(key, value);
            return $"@{key}";
        }

        private static string GenerateParameterName()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10);
        }
    }
}