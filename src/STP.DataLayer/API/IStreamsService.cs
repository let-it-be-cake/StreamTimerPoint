using STP.DataLayer.Models;

namespace STP.DataLayer.API
{
    public interface IStreamsService
    {
        /// <summary>
        /// If live stream status changed, notify client.
        /// </summary>
        public event EventHandler<StatusEventArgs> StreamsStatusChanged;

        public Task<List<StreamInfo>> GetMineStreamsAsync();

        public Task<StreamInfo?> GetStreamInfoAsync(string streamId);

        public void StartCheckNewStream(string streamId);
    }
}
