using STP.DataLayer.Models;

namespace STP.DataLayer.API
{
    public class StatusEventArgs : EventArgs
    {
        public ConnectionStatus Status { get; set; }

        public StatusEventArgs(ConnectionStatus status)
        {
            Status = status;
        }
    }
}
