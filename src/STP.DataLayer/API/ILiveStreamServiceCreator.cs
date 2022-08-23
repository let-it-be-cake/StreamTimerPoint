using Google.Apis.YouTube.v3;

namespace STP.DataLayer.API
{
    public interface ILiveStreamServiceCreator
    {
        public Task<YouTubeService> GetYoutubeServiceAsync();
    }
}
