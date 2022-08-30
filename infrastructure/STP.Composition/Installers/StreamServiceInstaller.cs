using Autofac;
using AutoMapper;
using STP.DataLayer.API;
using STP.DataLayer.Interfaces;
using STP.DataLayer.Services;

namespace STP.Composition.Installers
{
    internal class DataLayerServicesInstaller
    {
        private readonly ContainerOptions _options;

        public DataLayerServicesInstaller(ContainerOptions options)
        {
            _options = options;
        }

        public void Install(ContainerBuilder builder)
        {
            builder.Register(c => new AuthorizeService(_options.PathToSecrets))
                .As<IAuthorizeService>()
                .As<IUserCredentional>()
                .SingleInstance();

            builder.Register(c =>
                new LiveStreamService(c.Resolve<IUserCredentional>(), c.Resolve<IMapper>(), _options.ApplicationName))
                .AsSelf()
                .SingleInstance();

            builder.Register(c =>
                new LiveStreamFactory(c.Resolve<LiveStreamService>(), _options.RequestTimeout))
                .As<IStreamFactory>()
                .SingleInstance();

            builder.RegisterType<StreamsService>()
                .As<IStreamsService>()
                .SingleInstance();
        }
    }
}
