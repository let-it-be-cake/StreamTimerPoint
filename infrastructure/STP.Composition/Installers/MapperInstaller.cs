using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;

namespace STP.Composition.Installers
{
    internal class MapperInstaller
    {
        public void Install(ContainerBuilder builder)
        {
            builder.RegisterAutoMapper();
        }
    }
}
