using FluentValidation.AspNetCore;
using Framework.ASP.Extensions.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.ASP.Extensions.Extensions
{
    public static class FluentValidationServiceExtension
    {
        public static void AddFluentValidationFilter(this IServiceCollection services, Type startUpType)
        {
            services
                .AddControllers((config) =>
                {
                    config.Filters.Add(typeof(ValidationActionFilter));
                })
                .AddFluentValidation((config) =>
                {
                    config.ImplicitlyValidateChildProperties = true;
                    config.RegisterValidatorsFromAssemblyContaining(startUpType);
                });
        }
    }
}