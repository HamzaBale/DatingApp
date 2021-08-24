namespace API.Errors
{
    public class ApiException
    {
        public ApiException(int statusCode, string message = null, string stacktrace = null)
        {
            this.statusCode = statusCode;
            this.message = message;
            this.stacktrace = stacktrace;
        }

        public int statusCode { get; set; }
        public string message { get; set; }
        public string stacktrace { get; set; }
    }
}