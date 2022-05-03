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
using Service.GameServer.Domain.UserAggregate;

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
            services.AddScoped<IUnitOfWork<GameDbContext>, UnitOfWork<GameDbContext>>();
            services.AddScoped<UnitOfWork<GameDbContext>, UnitOfWork<GameDbContext>>();
            
            var connectionStringBuilder = new SqliteConnectionStringBuilder
            {
                DataSource = "w:\\Database.db",
                Pooling = false,
            };
           //var connectionString = connectionStringBuilder.ToString();
           //var connection = new SqliteConnection(connectionString);
           ////connection.Open();  //see https://github.com/aspnet/EntityFramework/issues/6968
           //services.AddDbContext<UserContext>(options => options.UseSqlite(connection));
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