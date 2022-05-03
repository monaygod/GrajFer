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
using Service.GameRepository.Domain.GameFileAggregate;

namespace Service.GameRepository.Repository
{
    public class RepositoryStartup : ModularStartup
    {
        public RepositoryStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IRepository<GameFile>, GameRepository>();
            services.AddScoped<GameRepository, GameRepository>();
            
            services.AddScoped<IUnitOfWork<GameRepositoryContext>, UnitOfWork<GameRepositoryContext>>();
            services.AddScoped<UnitOfWork<GameRepositoryContext>, UnitOfWork<GameRepositoryContext>>();
            
            services.AddDbContext<GameRepositoryContext>();
            
            services.AddAutoMapper(
                (serviceProvider, automapper) =>
                {
                    automapper.AddCollectionMappers();
                    automapper.UseEntityFrameworkCoreModel<GameRepositoryContext>(serviceProvider);
                },
                typeof(GameRepositoryContext).Assembly);
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}