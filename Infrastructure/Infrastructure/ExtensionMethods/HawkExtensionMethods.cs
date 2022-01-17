using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Infrastructure.ExtensionMethods
{
    public static class HawkExtensionMethods
    {
        public static IEnumerable<Assembly> GetAssemblies()
        {
            var list = new List<string>();
            var stack = new Stack<Assembly>();

            stack.Push(Assembly.GetEntryAssembly());

            do
            {
                var asm = stack.Pop();
                
                yield return asm;

                foreach (var reference in asm.GetReferencedAssemblies())
                    if (!list.Contains(reference.FullName))
                    {
                        stack.Push(Assembly.Load(reference));
                        list.Add(reference.FullName);
                    }
            }
            while (stack.Count > 0);
        }
        
        public static IEnumerable<Assembly> SelectHawkAssemblies(this IEnumerable<Assembly> assemblies)
        {
            return assemblies.Where(x => x.FullName != null && x.FullName.StartsWith("Hawk."));
        }
    }
}