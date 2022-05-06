using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using Infrastructure.Application.Command.Interface;
using Infrastructure.Application.Command.PipelineDecorator;
using Infrastructure.Application.Query.Interface;
using Infrastructure.Application.Query.PipelineDecorator;
using Infrastructure.Database;
using Infrastructure.ExtensionMethods;
using Infrastructure.IntegrationEvent;
using Infrastructure.IntegrationEvent.Interface;
using Infrastructure.Mapping;
using Infrastructure.Middleware;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Infrastructure.Startup
{
    public class MainStartup
    {
        public MainStartup(IConfiguration configuration)
        {
            Configuration = configuration;
            StartupHelper = new MainStartupHelper(configuration);
            EntryAssemblyName = Assembly.GetEntryAssembly()
                .FullName.Split(',')
                .First();
        }

        public IConfiguration Configuration { get; }
        protected MainStartupHelper StartupHelper { get; set; }
        protected string EntryAssemblyName { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            StartupHelper.UseModularConfigureServices(services); //uruchomienie configure services wszystkich modular startup

            //ważne od microsoftu
            services.AddHttpContextAccessor();

            services.AddDistributedMemoryCache();

            services.AddMediatR(
                ExtensionMethods.ExtensionMethods.GetAssemblies()
                    .ToArray());
            
            services.InstallGenericInterfaceWithAllInheritedClasses(typeof(ICommandValidator<>));
            services.InstallGenericInterfaceWithAllInheritedClasses(typeof(IQueryValidator<>));
            
            services.AddScoped<SqlConnection>(x =>
                new SqlConnection("")
            );
            services.AddScoped<TransactionManager>();
            services.AddScoped<MappingManager>();
            services.AddSingleton<IEventBus, FakeEventBuss>();
            services.AddSingleton<IRabbitPersistentConnection>(sp =>
            {
                var factory = new ConnectionFactory();
                return new RabbitPersistentConnection(factory);
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DebugQueryHandlerDecorator<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(QueryHandlerValidationDecorator<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandHandlerValidationDecorator<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandWithReturnValueHandlerValidationDecorator<,>));
            
            //api config
            services.AddSwaggerGen(
                c =>
                {
                    c.AddSecurityDefinition(
                        "Bearer",
                        new OpenApiSecurityScheme()
                        {
                            Name = "Authorization",
                            Type = SecuritySchemeType.ApiKey,
                            Scheme = "Bearer",
                            BearerFormat = "JWT",
                            In = ParameterLocation.Header,
                            Description = "DO POPRAWY | DODAJ SE TOKEN",
                        });
                    c.AddSecurityRequirement(
                        new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                new string[] { }
                            }
                        });
                    c.CustomOperationIds(apiDesc => apiDesc.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null);
                    // c.CustomSchemaIds( type => type.ToString().Replace("+","-") ); //TODO do weryfikacji
                    c.SwaggerDoc("v2", new OpenApiInfo {Title = EntryAssemblyName, Version = "v2"});
                    
                    var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    c.IncludeXmlComments(xmlPath);
                });
            services.AddControllers()
                .AddApplicationPart(Assembly.GetEntryAssembly())
                .AddControllersAsServices()
                .AddJsonOptions(options => 
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    //options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                });
            services.AddCors(
                options =>
                {
                    options.AddDefaultPolicy(
                        builder =>
                        {
                            builder.AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowAnyOrigin()
                                .AllowAnyOrigin();
                        });
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            StartupHelper.UseModularConfigure(app, env); //uruchomienie configure wszystkich modular startup


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v2/swagger.json", $"{EntryAssemblyName} v2"));
            }

            //autoryzacja
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseMiddleware<JwtMiddleware>();
            
            app.UseRouting();
            
            app.UseAuthorization();
            app.UseCors();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            if (env.IsDevelopment())
            {
                //app.MapWhen(context => context.Request.Path.StartsWithSegments("/api/game"),
                //    userApp =>
                //    {
                //        userApp.UseSpa(spa =>
                //        {
                //            spa.UseProxyToSpaDevelopmentServer("https://localhost:5002");
                //        });
                //    });
                //app.MapWhen(context => context.Request.Path.StartsWithSegments("/api/repository"),
                //    userApp =>
                //    {
                //        userApp.UseSpa(spa =>
                //        {
                //            spa.UseProxyToSpaDevelopmentServer("https://localhost:5003");
                //        });
                //    });
                //to jest przekierowanie do aplikacji webowej (możliwosć)
                //app.UseSpa(x=>x.UseProxyToSpaDevelopmentServer("http://localhost:4200"));
            }
        }
    }
}