using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using Microsoft.AspNetCore.Builder;

namespace Framework.ASP.Extensions.Extensions
{
    public static class ResponseCompressionServiceExtension
    {
        public static void AddGzipResponseCompression(this IServiceCollection services, CompressionLevel compressionLevel)
        {
            services.Configure<GzipCompressionProviderOptions>((option) => option.Level = compressionLevel);

            services.AddResponseCompression((option) =>
            {
                option.Providers.Add<GzipCompressionProvider>();
            });
        }
    }
}