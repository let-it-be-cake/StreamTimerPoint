using Autofac;
using STP.Composition.Installers;

namespace STP.Composition
{
    public class ContainerInstaller
    {
        private readonly ContainerOptions _options;
        private readonly ContainerBuilder _builder;

        public ContainerInstaller(ContainerBuilder builder, ContainerOptions options)
        {
            _options = options;
            _builder = builder;
        }

        public void Install()
        {
            //var assemblies = Assembly.GetEntryAssembly()!.GetReferencedAssemblies();

            new DataLayerServicesInstaller(_options).Install(_builder);
            new UIStartupInstaller(_options).Install(_builder);
        }
    }
}
