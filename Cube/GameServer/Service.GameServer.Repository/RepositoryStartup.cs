using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Infrastructure.DDD;
using Infrastructure.DDD.Interface;
using Infrastructure.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.GameServer.Domain.PlayerAggregate;
using Service.GameServer.Domain.RoomAggregate;

namespace Service.GameServer.Repository
{
    public class RepositoryStartup : ModularStartup
    {
        public RepositoryStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IRepository<Room>, RoomRepository>();
            services.AddScoped<RoomRepository, RoomRepository>();
            services.AddScoped<IRepository<Player>, PlayerRepository>();
            services.AddScoped<PlayerRepository, PlayerRepository>();
            //services.AddScoped<IRepository<Game>, GameRepository>();
            services.AddScoped<GameRepository, GameRepository>();
            services.AddScoped<IUnitOfWork<GameDbContext>, UnitOfWork<GameDbContext>>();
            services.AddScoped<UnitOfWork<GameDbContext>, UnitOfWork<GameDbContext>>();
            
           services.AddDbContext<GameDbContext>();
            
            services.AddAutoMapper(
                (serviceProvider, automapper) =>
                {
                    automapper.AddCollectionMappers();
                    automapper.UseEntityFrameworkCoreModel<GameDbContext>(serviceProvider);
                },
                typeof(GameDbContext).Assembly);
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}