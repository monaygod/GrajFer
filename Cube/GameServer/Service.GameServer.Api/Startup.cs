using System.Text.Json.Serialization;
using Infrastructure.DDD;
using Infrastructure.DDD.Interface;
using Infrastructure.IntegrationEvent;
using Infrastructure.IntegrationEvent.Interface;
using Infrastructure.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.GameServer.Application.Hubs;
using Service.GameServer.Repository;
using Microsoft.AspNetCore.SignalR;

namespace Service.GameServer.Api
{
    public class Startup : ModularStartup
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
            
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork<GameDbContext>, UnitOfWork<GameDbContext>>();
            
            services.AddScoped<IIntegrationEventDispatcher<GameDbContext>, IntegrationEventDispatcher<GameDbContext>>();
            services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();
            services.AddScoped<GameHub>();
            services.AddSignalR();
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<GameHub>("api/game/hub");
            });
        }
    }
}