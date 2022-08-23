using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using STP.DataLayer.API;

namespace STP.DataLayer.Services
{
    internal class LiveStreamServiceCreator : ILiveStreamServiceCreator
    {
        private readonly IUserCredentional _userCredentional;
        private readonly string _applicationName;

        private static YouTubeService? _youTubeService;

        public LiveStreamServiceCreator(IUserCredentional userCredentional,
                                     string applicationName)
        {
            _userCredentional = userCredentional;
            _applicationName = applicationName;
        }

        public async Task<YouTubeService> GetYoutubeServiceAsync()
        {
            if (_youTubeService is null)
            {
                _youTubeService = new YouTubeService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = await _userCredentional.GetUserCredentialAsync(),
                    ApplicationName = _applicationName,
                });
            }

            return _youTubeService;
        }
    }
}
