using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using System.Reflection;

namespace STP.Composition.Installers
{
    internal class MapperInstaller
    {
        public void Install(ContainerBuilder builder)
        {
            var assemblies = Assembly
                .GetExecutingAssembly()
                .GetReferencedAssemblies()
                .Select(x => Assembly.Load(x))
                .ToArray();
            builder.RegisterAutoMapper(propertiesAutowired: false, assemblies);
        }
    }
}
