using AutoMapper;
using STP.DataLayer.API;
using STP.DataLayer.Interfaces;

namespace STP.DataLayer.Services
{
    internal class LiveStreamServiceFactory : IStreamFactory
    {
        private readonly IUserCredentional _userCredentional;
        private readonly ILiveStreamServiceCreator _liveStreamServiceCreator;
        private readonly IMapper _mapper;

        public LiveStreamServiceFactory(IUserCredentional userCredentional,
            ILiveStreamServiceCreator liveStreamServiceCreator,
            IMapper mapper)
        {
            _userCredentional = userCredentional;
            _liveStreamServiceCreator = liveStreamServiceCreator;
            _mapper = mapper;
        }

        public Task<IStream> GetStreamAsync(string streamId)
        {
            return new LiveStreamService(await _userCredentional.GetUserCredentialAsync())
        }
    }
}
