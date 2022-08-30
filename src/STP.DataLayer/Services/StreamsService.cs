using STP.DataLayer.API;
using STP.DataLayer.Interfaces;
using STP.DataLayer.Models;

namespace STP.DataLayer.Services
{
    internal class StreamsService : IStreamsService
    {
        public event EventHandler<StatusEventArgs>? StreamsStatusChanged;

        private readonly LiveStreamService _liveStreamService;
        private readonly IStreamFactory _streamFactory;

        private readonly Dictionary<string, IStream> _streamDictionary = new();

        public StreamsService(LiveStreamService liveStreamService, IStreamFactory streamFactory)
        {
            _liveStreamService = liveStreamService;
            _streamFactory = streamFactory;
        }

        public Task<List<StreamInfo>> GetMineStreamsAsync()
        {
            return _liveStreamService.GetUserStreamsAsync();
        }

        public async Task<StreamInfo?> GetStreamInfoAsync(string streamId)
        {
            return _streamDictionary.TryGetValue(streamId, out var stream) ?
                await stream!.GetInfoAsync() :
                null;
        }

        public void StartCheckNewStream(string streamId)
        {
            if (!_streamDictionary.TryGetValue(streamId, out _))
            {
                var stream = _streamFactory.GetStream(streamId);
                stream.StreamStatusChanged += StreamsStatusChanged;
                _streamDictionary.Add(streamId, stream);
            }
        }
    }
}
