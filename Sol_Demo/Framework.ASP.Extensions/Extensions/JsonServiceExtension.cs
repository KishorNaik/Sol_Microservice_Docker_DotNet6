using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.ASP.Extensions.Extensions
{
    public static class JsonServiceExtension
    {
        public static void AddJson(this IServiceCollection services, bool isPascalCase, bool isIgnoreNullValueFromResponse)
        {
            services
                 .AddControllers()
                 .AddJsonOptions((config) =>
                 {
                     if (isPascalCase)
                     {
                         // Pascal Casing
                         config.JsonSerializerOptions.PropertyNamingPolicy = null;
                     }

                     if (isIgnoreNullValueFromResponse)
                     {
                         //config.JsonSerializerOptions.IgnoreNullValues = true;
                         config.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                     }
                 });
        }
    }
}