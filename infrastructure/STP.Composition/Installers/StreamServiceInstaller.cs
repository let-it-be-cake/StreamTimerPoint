using Autofac;
using Google.Apis.Services;
using Google.Apis.Util;
using Google.Apis.YouTube.v3;
using STP.DataLayer.API;
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
                new LiveStreamServiceCreator(c.Resolve<IUserCredentional>(), _options.ApplicationName))
                .As<ILiveStreamServiceCreator>()
                .SingleInstance();
            //builder.RegisterType<StreamService>().As<IStreamService>();
        }
    }
}

