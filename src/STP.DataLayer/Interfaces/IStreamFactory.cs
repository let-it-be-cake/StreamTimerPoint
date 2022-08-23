namespace STP.DataLayer.Interfaces
{
    internal interface IStreamFactory
    {
        public IStream GetStream(string streamId);
    }
}
