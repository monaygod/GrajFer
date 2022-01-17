/*using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Service.IdentityServer.Repository
{
    public class EntityTypeGenerator : CSharpEntityTypeGenerator
    {
        public EntityTypeGenerator(IAnnotationCodeGenerator annotationCodeGenerator, ICSharpHelper helper) : base(annotationCodeGenerator, helper)
        {
        }

        public override string WriteCode(
            IEntityType entityType,
            string @namespace,
            bool useDataAnnotations
        )
        {
            string code = base.WriteCode(entityType, @namespace, useDataAnnotations);

            var newUsing = "using Infrastructure.Database;\n";
            var oldString = "public partial class " + entityType.Name;
            var newString = "public partial class " + entityType.Name + " : DbBuildingBlock";
            code = newUsing + code;
            return code.Replace(oldString, newString);
        }
    }

    public class MyDesignTimeServices : IDesignTimeServices
    {
        public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ICSharpEntityTypeGenerator, EntityTypeGenerator>();
        }
    }
}*/