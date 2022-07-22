using DataLayer.API;
using Google.Apis.Auth.OAuth2;
using Google.Apis.YouTube.v3;

namespace DataLayer.Services
{
    internal class YoutubeAuthorize : IYoutubeAuthorize
    {
        public UserCredential UserCredential { get; private set; }

        public async Task AuthorizeAsync()
        {
            UserCredential =  await GoogleWebAuthorizationBroker.AuthorizeAsync(
                               (await LoadSecretsAsync()).Secrets,
                               new string[] { YouTubeService.Scope.YoutubeReadonly },
                               "user",
                               CancellationToken.None);
        }

        public async Task ReauthorizeAsync()
        {
            await GoogleWebAuthorizationBroker.ReauthorizeAsync(UserCredential, CancellationToken.None);
        }

        private Task<GoogleClientSecrets> LoadSecretsAsync()
        {
            using var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read);

            return GoogleClientSecrets.FromStreamAsync(stream);
        }

        private async Task<UserCredential> LoadCredentialAsync()
        {
        }
    }
}
