using Google.Apis.Auth.OAuth2;

namespace STP.DataLayer.API
{
    public interface IUserCredentional
    {
        public Task<UserCredential> GetUserCredentialAsync();
    }
}
