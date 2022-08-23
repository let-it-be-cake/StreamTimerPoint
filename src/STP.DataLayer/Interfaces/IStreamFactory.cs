namespace STP.DataLayer.Interfaces
{
    internal interface IStreamFactory
    {
        public IStream GetStreamAsync(string streamId);
    }
}
