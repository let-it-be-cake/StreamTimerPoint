using Autofac;
using STP.DataLayer.Interfaces;
using STP.DataLayer.Services;

namespace STP.Composition.Installers
{
    internal class TimeStampSaverInstaller
    {
        private readonly ContainerOptions _options;

        public TimeStampSaverInstaller(ContainerOptions options)
        {
            _options = options;
        }

        public void Install(ContainerBuilder builder)
        {
            builder.Register(c => new TimeStampSaver(_options.Pattern))
                .As<ITimeStampsSaver>();
        }
    }
}
