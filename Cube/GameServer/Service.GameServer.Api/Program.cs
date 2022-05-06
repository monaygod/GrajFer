using System.Security.Authentication;
using Infrastructure.ExtensionMethods;
using Infrastructure.SwaggerGenerator;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Service.GameServer.Api
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
                    webBuilder.ConfigureKestrel(x => x.ConfigureHttpsDefaults(y => y.SslProtocols = SslProtocols.Tls12));
                    webBuilder.UseMainStartup();
                });
    }
}