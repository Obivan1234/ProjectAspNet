using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Clean.Core.Infrastructure
{
    public class WebAppTypeFinder
    {
        private string assemblySkipLoadingPattern = "^System|^mscorlib|^Microsoft|^AjaxControlToolkit|^Antlr3|^Autofac|^AutoMapper|" +
           "^Castle|^ComponentArt|^CppCodeProvider|^DotNetOpenAuth|^EntityFramework|^EPPlus|^FluentValidation|^ImageResizer|^itextsharp" +
           "|^log4net|^MaxMind|^MbUnit|^MiniProfiler|^Mono.Math|^MvcContrib|^Newtonsoft|^NHibernate|^nunit|^Org.Mentalis|^PerlRegex|^QuickGraph" +
           "|^Recaptcha|^Remotion|^RestSharp|^Rhino|^Telerik|^Iesi|^TestDriven|^TestFu|^UserAgentStringLibrary|^VJSharpCodeProvider|^WebActivator|^WebDev|^WebGrease";


        public string AssemblySkipLoadingPattern
        {
            get { return assemblySkipLoadingPattern; }
            set { assemblySkipLoadingPattern = value; }
        }

        public IEnumerable<Type> FindClassesOfType<T>()
        {
            return FindClassesOfType(typeof(T), GetAssemblies());
        }

        private IEnumerable<Type> FindClassesOfType(Type type, IEnumerable<Assembly> assemblies)
        {
            var result = new List<Type>();

            try
            {
                foreach (var item in assemblies)
                {
                    Type[] types = item.GetTypes();

                    if (types != null)
                    {
                        foreach (var t in types)
                        {
                            if (type.IsAssignableFrom(t))
                            {
                                if (t.IsClass && !t.IsInterface && !t.IsAbstract)
                                {
                                    result.Add(t);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Error in FindClassesOfType");
            }

            return result;
        }

        private IEnumerable<Assembly> GetAssemblies()
        {
            List<Assembly> assemblies = new List<Assembly>();

            AssembliesInAppDomain(assemblies);

            return assemblies;

        }

        private void AssembliesInAppDomain(List<Assembly> assemblies)
        {
            foreach (var item in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assemblies.Contains(item))
                    continue;

                if (!Matches(item))
                    continue;

                assemblies.Add(item);
            }
        }

        public bool Matches(Assembly assembly)
        {
            return !Matches(assembly.FullName, AssemblySkipLoadingPattern);
        }

        public bool Matches(string fullName, string pattern)
        {
            return Regex.IsMatch(fullName, pattern);
        }

    }
}
