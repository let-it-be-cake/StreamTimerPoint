using Autofac;
using STP.Composition.Installers;

namespace STP.Composition
{
    public class ContainerInstaller
    {
        private readonly ContainerBuilder _builder;
        private readonly ContainerOptions _options;

        public ContainerInstaller(ContainerOptions options, ContainerBuilder? builder = null)
        {
            _options = options;
            _builder = builder ?? new();
        }

        public ContainerBuilder Install()
        {
            new MapperInstaller().Install(_builder);
            new DataLayerServicesInstaller(_options).Install(_builder);
            new TimeStampSaverInstaller(_options).Install(_builder);

            return _builder;
        }
    }
}
