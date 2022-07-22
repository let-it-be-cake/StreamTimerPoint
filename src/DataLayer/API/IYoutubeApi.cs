using DataLayer.Models;

namespace DataLayer.API
{
    public interface IYoutubeApi
    {
        /// <summary>
        /// If live stream status changed, notify client.
        /// </summary>
        public delegate void StatusChanged();

        /// <summary>
        /// Get current connection status.
        /// </summary>
        /// <returns>Status of connection.</returns>
        public Task<ConnectionStatus> CurrentStatusAsync();

        /// <summary>
        /// Authorize user in Google account.
        /// </summary>
        public Task AuthorizeAsync();

        /// <summary>
        /// Get current streams.
        /// </summary>
        public Task GetStreamsAsync();

        /// <summary>
        /// Get info about stream.
        /// </summary>
        public Task<StreamInfo> GetInfoAsync();
    }
}
