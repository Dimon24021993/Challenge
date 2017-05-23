using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6
{
    public static class Class1
    {
        private static bool MonitoringIsEnabled { get; set; } = true;

        private static Dictionary<string, Type> GetDictionary()
        {
            var dictionary = new Dictionary<string, Type>();
            AppDomain.MonitoringIsEnabled = MonitoringIsEnabled;
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var assemblyDefinedTypes = assembly.DefinedTypes;
                foreach (var typeInfo in assemblyDefinedTypes)
                {
                    var typeInfoDeclaredConstructors = typeInfo.DeclaredConstructors;
                    foreach (var constructorInfo in typeInfoDeclaredConstructors)
                    {
                        if (!constructorInfo.IsStatic
                            && !constructorInfo.IsAbstract
                            && !constructorInfo.IsPrivate
                            && !constructorInfo.IsVirtual
                            && constructorInfo.IsConstructor
                            && !dictionary.ContainsKey(constructorInfo.DeclaringType.Name))
                        {
                            dictionary.Add(constructorInfo.DeclaringType.Name, constructorInfo.DeclaringType);
                        }
                    }
                }
            }
            return dictionary;
        }

        public static object Create(string @class)
        {
            var constructors = GetDictionary();
            
            return Activator.CreateInstance(constructors[@class]);
        }
    }
}