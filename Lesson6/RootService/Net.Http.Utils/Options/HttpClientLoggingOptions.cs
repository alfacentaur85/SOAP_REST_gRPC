namespace EuropAssistance.Net.Http.Options
{
    public class HttpClientLoggingOptions
    {
        public const bool DefaultLogMultipartFormData = false;

        public const int DefaultMaxBodyLength = 16384;

        public bool LogMultipartFormData { get; set; } = DefaultLogMultipartFormData;

        public int MaxBodyLength { get; set; } = DefaultMaxBodyLength;
    }
}
