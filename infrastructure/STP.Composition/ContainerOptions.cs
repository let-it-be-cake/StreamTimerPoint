namespace STP.Composition
{
    public struct ContainerOptions
    {
        public string ApplicationName { get; set; }

        public string PathToSecrets { get; set; }

        public int RequestTimeout { get; set; }
    }
}
