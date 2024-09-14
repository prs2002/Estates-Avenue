namespace DotNetBackend
{
    public class REdbSettings
    {
        public string? ConnectionString { get; set; }
        public string? DatabaseName { get; set; }
        public string? CustCollection { get; set; }
        public string? ExecCollectionName { get; set; }
        public string? CustReqCollection { get; set; }
    }
}