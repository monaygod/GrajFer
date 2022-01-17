using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.ExtensionMethods
{
    public static class ServiceCollectionExtensionMethods
    {
        public static void InstallGenericInterfaceWithAllInheritedClasses(this IServiceCollection services, Type interfaceType)
        {
            var handleTypes = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany
                (
                    a => a.GetTypes().Where
                    (
                        x => !x.IsInterface &&
                             !x.IsAbstract &&
                             x.GetInterfaces().Any(y => y.Name.Equals(interfaceType.Name, StringComparison.InvariantCulture))
                    )
                );
            foreach (var type in handleTypes)
            {
                foreach (var implementedInterface in type.GetInterfaces())
                {
                    services.AddScoped(implementedInterface, type);
                }
            }
        }
    }
}