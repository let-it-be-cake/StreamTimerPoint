namespace STP.Composition
{
    public readonly struct ContainerOptions
    {
        public string ApplicationName { get; init; }

        public string PathToSecrets { get; init; }

        public string Pattern { get; init; }

        public int RequestTimeout { get; init; }
    }
}
