using STP.DataLayer.Models;

namespace STP.DataLayer.API
{
    public interface IStreamsService
    {
        public Task<List<StreamInfo>> GetMineStreams();
    }
}
