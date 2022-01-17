using Hawk.Infrastructure.Event.Kafka;
using Hawk.Infrastructure.Main.DDD;
using Hawk.Infrastructure.Main.DDD.Interface;
using Hawk.Infrastructure.Main.IntegrationEvent;
using Hawk.Infrastructure.Main.IntegrationEvent.Interface;
using Hawk.Infrastructure.Main.Startup;
using Hawk.Service.IdentityServer.Repository;
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
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork<UserContext>, UnitOfWork<UserContext>>();
            
            services.AddSingleton<IEventBus, KafkaEventBus>();
            services.AddScoped<IIntegrationEventDispatcher<UserContext>, IntegrationEventDispatcher<UserContext>>();

            services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();
            //services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);  //todo
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}