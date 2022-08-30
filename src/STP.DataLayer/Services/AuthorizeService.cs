using STP.DataLayer.API;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.YouTube.v3;
using STP.DataLayer.Interfaces;

namespace STP.DataLayer.Services
{
    internal class AuthorizeService : IAuthorizeService, IUserCredentional
    {
        private readonly string _credentialPath;
        private ClientSecrets? _secrets;
        private UserCredential? _userCredential;

        public bool IsAuthorized => _userCredential is not null;

        public AuthorizeService(string credentialPath)
        {
            _credentialPath = credentialPath;
        }

        public async Task AuthorizeAsync()
        {
            _userCredential =  await GoogleWebAuthorizationBroker.AuthorizeAsync(
                               _secrets ?? await LoadSecretsAsync(),
                               new string[] { YouTubeService.Scope.YoutubeReadonly },
                               "user",
                               CancellationToken.None);
        }

        public Task ReauthorizeAsync()
        {
            return _userCredential is null ?
                AuthorizeAsync() :
                GoogleWebAuthorizationBroker.ReauthorizeAsync(_userCredential, default);
        }


        public async Task DeleteTokenAsync()
        {
            await new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = _secrets ?? await LoadSecretsAsync(),
            }).DeleteTokenAsync(_secrets!.ClientId, default);
        }

        public async Task<UserCredential> GetUserCredentialAsync()
        {
            if (_userCredential is null)
            {
                await AuthorizeAsync();
            }

            return _userCredential!;
        }

        private async Task<ClientSecrets> LoadSecretsAsync()
        {
            using var stream = new FileStream(_credentialPath, FileMode.Open, FileAccess.Read);

            _secrets = (await GoogleClientSecrets.FromStreamAsync(stream)).Secrets;
            return _secrets;
        }
    }
}
