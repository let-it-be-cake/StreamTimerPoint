namespace STP.DataLayer.Models
{
    public struct StreamInfo
    {
        public string Id { get; set; }

        public string? Name { get; set; }

        public DateTime PublishedAt { get; set; }

        public ConnectionStatus ConnectionStatus { get; set; }

        public StreamStatus StreamStatus { get; set; }
    }
}
