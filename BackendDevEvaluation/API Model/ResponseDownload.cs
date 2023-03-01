namespace BackendDevEvaluation.API_Model
{
    public class ResponseDownload
    {
        public ResponseDownload()
        {

        }

        public bool Success { get; set; }
        public string? Message { get; set; }
        public IDictionary<string, string> UrlAndNames { get; set; }

    }
}
