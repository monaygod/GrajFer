using System;
using System.Collections.Generic;
using System.Reflection;
using Infrastructure.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Startup
{
    public class MainStartupHelper
    {
        public IConfiguration Configuration { get; }
        public ICollection<Type> Types { get; set; }

        public MainStartupHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            Types ??= new List<Type>();
            LoadStartupTypes();
        }

        private void LoadStartupTypes()
        {
            var allAssemblies = ExtensionMethods.ExtensionMethods.GetAssemblies().SelectMainAssemblies();
            foreach (Assembly assembly in allAssemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.BaseType == typeof(ModularStartup))
                    {
                        Types.Add(type);
                    }
                }
            }
        }

        public void UseModularConfigureServices(IServiceCollection services)
        {
            foreach (Type type in Types)
            {
                var modularStartupInstance = (ModularStartup) Activator.CreateInstance(type, Configuration);
                modularStartupInstance?.ConfigureServices(services);   
            }
        }

        public void UseModularConfigure(
            IApplicationBuilder app,
            IWebHostEnvironment env
        )
        {
            foreach (Type type in Types)
            {
                var modularStartupInstance = (ModularStartup) Activator.CreateInstance(type, Configuration);
                modularStartupInstance?.Configure(app,env);   
            }
        }
    }
}