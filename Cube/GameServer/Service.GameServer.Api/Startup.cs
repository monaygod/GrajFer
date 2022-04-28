using Infrastructure.DDD;
using Infrastructure.DDD.Interface;
using Infrastructure.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.GameServer.Repository;

namespace Service.GameServer.Api
{
    public class Startup : ModularStartup
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
            
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork<UserContext>, UnitOfWork<UserContext>>();
            
            //services.AddScoped<IIntegrationEventDispatcher<UserContext>, IntegrationEventDispatcher<UserContext>>();
            services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();
            //services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);  //todo
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}