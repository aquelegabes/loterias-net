using System;
using Microsoft.AspNetCore.Hosting;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace Loterias.API.HerokuDevops
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UsePort(this IWebHostBuilder builder)
        {
            var port = Environment.GetEnvironmentVariable("PORT");
            if (string.IsNullOrWhiteSpace(port))
                return builder;

            return builder.UseUrls($"http://+:{port}");
        }
    }
}
