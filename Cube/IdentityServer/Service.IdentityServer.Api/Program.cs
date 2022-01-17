using Hawk.Infrastructure.Main.ExtensionMethods;
using Hawk.Infrastructure.Main.SwaggerGenerator;
using Infrastructure.ExtensionMethods;
using Microsoft.Extensions.Hosting;

namespace Service.IdentityServer.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
#if GENERATECLIENTAPI
            host.GenerateSwaggerSpecification();
            return;
#endif
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseHawkStartup();
                });
    }
}