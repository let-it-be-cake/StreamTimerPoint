using STP.DataLayer.Interfaces;

namespace STP.DataLayer.Services
{
    internal class LiveStreamServiceFactory : IStreamFactory
    {
        private readonly LiveStreamService _liveStreamService;
        private readonly int _requestTimeout;

        public LiveStreamServiceFactory(LiveStreamService liveStreamService, int requestTimeout)
        {
            _liveStreamService = liveStreamService;
            _requestTimeout = requestTimeout;
        }

        public IStream GetStream(string streamId)
        {
            return new YoutubeStream(_liveStreamService, streamId, _requestTimeout);
        }
    }
}
