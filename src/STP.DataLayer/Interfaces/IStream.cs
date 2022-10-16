using STP.DataLayer.API;
using STP.DataLayer.Models;

namespace STP.DataLayer.Interfaces
{
    public interface IStream
    {
        /// <summary>
        /// The stream in which the status will be checked.
        /// </summary>
        public string StreamId { get; }

        /// <summary>
        /// If live stream status changed, notify client.
        /// </summary>
        public event EventHandler<StatusEventArgs> StreamStatusChanged;

        /// <summary>
        /// Get current connection status.
        /// </summary>
        /// <returns>Status of connection.</returns>
        public Task<ConnectionStatus> CurrentStatusAsync();

        /// <summary>
        /// Get info about stream.
        /// </summary>
        public Task<StreamInfo?> GetInfoAsync();
    }
}
