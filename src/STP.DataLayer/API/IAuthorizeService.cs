using Google.Apis.Auth.OAuth2;

namespace STP.DataLayer.API
{
    public interface IAuthorizeService
    {
        public bool IsAuthorized { get; }

        public Task AuthorizeAsync();

        public Task ReauthorizeAsync();

        public Task DeleteTokenAsync();

        public Task<UserCredential> GetUserCredentialAsync();
    }
}
