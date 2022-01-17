using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Startup
{
    public abstract class ModularStartup
    {
        public IConfiguration Configuration { get; }

        public ModularStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public abstract void ConfigureServices(IServiceCollection services);

        public abstract void Configure(IApplicationBuilder app, IWebHostEnvironment env);
    }
}