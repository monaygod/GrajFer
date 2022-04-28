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
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<UserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork<UserContext>, UnitOfWork<UserContext>>();
            services.AddScoped<UnitOfWork<UserContext>, UnitOfWork<UserContext>>();
            
            var connectionStringBuilder = new SqliteConnectionStringBuilder
            {
                DataSource = "w:\\Database.db",
                Pooling = false,
            };
           //var connectionString = connectionStringBuilder.ToString();
           //var connection = new SqliteConnection(connectionString);
           ////connection.Open();  //see https://github.com/aspnet/EntityFramework/issues/6968
           //services.AddDbContext<UserContext>(options => options.UseSqlite(connection));
           services.AddDbContext<UserContext>();
            
            services.AddAutoMapper(
                (serviceProvider, automapper) =>
                {
                    automapper.AddCollectionMappers();
                    automapper.UseEntityFrameworkCoreModel<UserContext>(serviceProvider);
                },
                typeof(UserContext).Assembly);
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}