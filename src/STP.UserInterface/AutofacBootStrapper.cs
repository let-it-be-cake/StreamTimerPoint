using Autofac;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using STP.Composition;
using STP.UserInterface.ViewModels;
using IContainer = Autofac.IContainer;

namespace STP.UserInterface
{
    internal class AutofacBootStrapper : BootstrapperBase
    {
        private IContainer? _container;

        protected IContainer? Container
        {
            get { return _container; }
        }

        protected override void Configure()
        {

            // Grab the configuration as an object
            //services.Configure<ContainerOptions>(Configuration);
            //var containerOptions = Configuration.Get<ContainerOptions>();

            //// Install the container, using our configuration
            //var installer = new ContainerInstaller(containerOptions);
            //var builder = installer.Install();

            //// Pull the .net core dependencies into the container, like controllers
            //builder.Populate(services);

            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
                .Where(type => type.Name.EndsWith("ViewModel"))
                .Where(type => !string.IsNullOrWhiteSpace(type.Namespace) && type.Namespace.EndsWith("ViewModels"))
                .Where(type => type.GetInterface(typeof(INotifyPropertyChanged).Name) != null)
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
                .Where(type => type.Name.EndsWith("View"))
                .Where(type => !(string.IsNullOrWhiteSpace(type.Namespace)) && type.Namespace.EndsWith("Views"))
                .AsSelf()
                .InstancePerDependency();

            builder.Register<IWindowManager>(c => new WindowManager()).InstancePerLifetimeScope();
            builder.Register<IEventAggregator>(c => new EventAggregator()).InstancePerLifetimeScope();

            new ContainerInstaller(builder, new ContainerOptions
            {
                ApplicationName = "Qa",
#warning Move to app.xaml
                PathToSecrets = "client_secrets.json",
            }).Install();

            _container = builder.Build();
        }

        protected override async void OnStartup(object sender, StartupEventArgs e)
        {
            await DisplayRootViewForAsync<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                if (Container!.IsRegistered(service))
                    return Container!.Resolve(service);
            }
            else
            {
                if (Container!.IsRegisteredWithName(key, service))
                    return Container!.ResolveKeyed(key, service);
            }

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", key ?? service.Name));
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            var instanceCollection = Container!
                .Resolve(typeof(IEnumerable<>)
                .MakeGenericType(service)) as IEnumerable<object>;

            return instanceCollection!;
        }

        protected override void BuildUp(object instance)
        {
            Container!.InjectProperties(instance);
        }
    }
}
