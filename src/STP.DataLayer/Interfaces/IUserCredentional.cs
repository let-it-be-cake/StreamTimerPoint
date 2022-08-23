using Google.Apis.Auth.OAuth2;

namespace STP.DataLayer.Interfaces
{
    internal interface IUserCredentional
    {
        public Task<UserCredential> GetUserCredentialAsync();
    }
}
