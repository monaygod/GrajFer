using Infrastructure.DDD;
using Infrastructure.DDD.Interface;
using Infrastructure.IntegrationEvent;
using Infrastructure.IntegrationEvent.Interface;
using Infrastructure.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.IdentityServer.Repository;

namespace Service.IdentityServer.Api
{
    public class Startup : ModularStartup
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
            
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork<UserContext>, UnitOfWork<UserContext>>();
            
            services.AddScoped<IIntegrationEventDispatcher<UserContext>, IntegrationEventDispatcher<UserContext>>();
            services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}