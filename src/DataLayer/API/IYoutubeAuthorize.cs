namespace DataLayer.API
{
    internal interface IYoutubeAuthorize
    {
        public Task AuthorizeAsync();

        public Task ReauthorizeAsync();
    }
}
