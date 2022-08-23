using STP.DataLayer.API;
using STP.DataLayer.Models;
using STP.DataLayer.Interfaces;

namespace STP.DataLayer.Services
{
    /// <inheritdoc cref="IStream"/>
    internal class YoutubeStream : IStream
    {
        private readonly LiveStreamService _liveStreamService;
        private readonly int _requestTimeout;

        private StreamInfo _stream;

        public string StreamId { get; private set; }

        public YoutubeStream(LiveStreamService liveStreamService,
                             string streamId,
                             int requestTimeout)
        {
            _liveStreamService = liveStreamService;
            _requestTimeout = requestTimeout;
            StreamId = streamId;

            StartCheckStatus();
        }

        /// <inheritdoc/>
        public event EventHandler<StatusEventArgs>? StreamStatusChanged;

        /// <inheritdoc/>
        public Task<ConnectionStatus> CurrentStatusAsync()
        {
            return _liveStreamService.GetConnectionStatusAsync(StreamId);
        }

        /// <inheritdoc/>
        public Task<StreamInfo?> GetInfoAsync()
        {
            return _liveStreamService.GetStreamInfoAsync(StreamId);
        }

        private void StartCheckStatus()
        {
            var statusCheckThread = new Thread(StatusCheckThread);
            statusCheckThread.Start();
        }

        private async void StatusCheckThread()
        {
            const int MAX_ERROR_COUNT = 5;
            var currentErrors = 0;

            while (true)
            {
                var stream = await _liveStreamService.GetStreamInfoAsync(StreamId);

                if (stream is not null && stream.Value.ConnectionStatus != _stream.ConnectionStatus)
                {
                    currentErrors = 0;
                    StreamStatusChanged?.Invoke(stream, new StatusEventArgs(stream.Value.ConnectionStatus));
                    _stream = stream.Value;
                }
                else
                {
                    currentErrors++;
                }

                if (currentErrors >= MAX_ERROR_COUNT)
                {
                    StreamStatusChanged?.Invoke(stream, new StatusEventArgs(ConnectionStatus.Error));
                }

                Thread.Sleep(_requestTimeout);
            }
        }
    }
}
