using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Writers;
using Swashbuckle.AspNetCore.Swagger;

namespace Infrastructure.SwaggerGenerator
{
    public static class Swagger
    {
        public static IHost GenerateSwaggerSpecification(this IHost host)
        {
            var prov = host.Services.GetRequiredService<ISwaggerProvider>();
            var config = host.Services.GetRequiredService<IConfiguration>();
            var patchToClientService = config["Client:PathToGenerateApi"] ??
                                       throw new ArgumentNullException("config[\"Client:PathToGenerateApi\"]",
                                           "Brak zdefiniowanej ścieżki do wygenerowania api");
            var swag = prov.GetSwagger("v2");
            var swaggerFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\swagger.json";
            using (var streamWriter = File.CreateText(swaggerFilePath))
            {
                IOpenApiWriter writer = new OpenApiJsonWriter(streamWriter);

                swag.SerializeAsV3(writer);
                Console.WriteLine($"Swagger JSON/YAML succesfully written to {swaggerFilePath}");
            }

            ProcessStartInfo info = new ProcessStartInfo("cmd.exe");
            var modulePrefix = System.Reflection.Assembly.GetEntryAssembly().GetName().Name.Replace(".", "");
            info.Arguments =
                $@"/c {System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\SwaggerGenerator\GenerateAngularClient.cmd {patchToClientService + System.Reflection.Assembly.GetEntryAssembly().GetName().Name} {swaggerFilePath} {modulePrefix}";
            var process = Process.Start(info);
            process.WaitForExit();

            return host;
        }
    }
}