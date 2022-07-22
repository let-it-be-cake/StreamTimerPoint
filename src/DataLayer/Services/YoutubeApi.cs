using DataLayer.API;
using DataLayer.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.YouTube.v3;

namespace DataLayer.Services
{
    internal class YoutubeApi : IYoutubeApi
    {
        public async Task AuthorizeAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ConnectionStatus> CurrentStatusAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<StreamInfo> GetInfoAsync()
        {
            throw new NotImplementedException();
        }

        public async Task GetStreamsAsync()
        {
            throw new NotImplementedException();
        }

    }
}
