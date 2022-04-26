using Infrastructure.Startup;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.ExtensionMethods
{
    public static class WebHostBuilderExtensionMethods
    {
        public static IWebHostBuilder UseMainStartup(this IWebHostBuilder hostBuilder)
        {
            return hostBuilder.UseStartup<MainStartup>();
        }
    }
}