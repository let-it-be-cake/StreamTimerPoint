using Autofac;
using System.ComponentModel;

namespace STP.Composition.Installers
{
    internal class UIStartupInstaller
    {
        private readonly ContainerOptions _options;

        public UIStartupInstaller(ContainerOptions options)
        {
            _options = options;
        }

        public void Install(ContainerBuilder builder)
        {
            //builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
            //    .Where(type => type.Name.EndsWith("ViewModel"))
            //    .Where(type => !string.IsNullOrWhiteSpace(type.Namespace) && type.Namespace.EndsWith("ViewModels"))
            //    .Where(type => type.GetInterface(typeof(INotifyPropertyChanged).Name) != null)
            //    .AsSelf()
            //    .InstancePerDependency();

            //builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
            //    .Where(type => type.Name.EndsWith("View"))
            //    .Where(type => !(string.IsNullOrWhiteSpace(type.Namespace)) && type.Namespace.EndsWith("Views"))
            //    .AsSelf()
            //    .InstancePerDependency();

            //builder.Register<IWindowManager>(c => new WindowManager()).InstancePerLifetimeScope();
            //builder.Register<IEventAggregator>(c => new EventAggregator()).InstancePerLifetimeScope();

        }
    }
}
