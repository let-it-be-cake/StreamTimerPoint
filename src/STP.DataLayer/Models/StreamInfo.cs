namespace STP.DataLayer.Models
{
    public readonly struct StreamInfo
    {
        public StreamInfo()
        {
            Id = string.Empty;
            Name = default;
            PublishedAt = default;
            ConnectionStatus = default;
            StreamStatus = default;
        }

        public StreamInfo(string id,
                          string? name,
                          DateTime publishedAt,
                          ConnectionStatus connectionStatus,
                          StreamStatus streamStatus)
        {
            Id = id;
            Name = name;
            PublishedAt = publishedAt;
            ConnectionStatus = connectionStatus;
            StreamStatus = streamStatus;
        }

        public string Id { get; init; }

        public string? Name { get; init; }

        public DateTime PublishedAt { get; init; }

        public ConnectionStatus ConnectionStatus { get; init; }

        public StreamStatus StreamStatus { get; init; }

        public bool IsStreamGood => StreamStatus == StreamStatus.Active
            && ConnectionStatus != ConnectionStatus.BadConnection 
            && ConnectionStatus != ConnectionStatus.NoData
            && ConnectionStatus != ConnectionStatus.Error;

        public override bool Equals(object? obj)
        {
            return obj is StreamInfo info &&
                   Id == info.Id &&
                   Name == info.Name &&
                   PublishedAt == info.PublishedAt &&
                   ConnectionStatus == info.ConnectionStatus &&
                   StreamStatus == info.StreamStatus;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id,
                                    Name,
                                    PublishedAt,
                                    ConnectionStatus,
                                    StreamStatus);
        }
    }
}
