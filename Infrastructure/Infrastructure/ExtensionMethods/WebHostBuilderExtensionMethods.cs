using Infrastructure.Startup;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.ExtensionMethods
{
    public static class WebHostBuilderExtensionMethods
    {
        public static IWebHostBuilder UseHawkStartup(this IWebHostBuilder hostBuilder)
        {
            return hostBuilder.UseStartup<HawkStartup>();
        }
    }
}