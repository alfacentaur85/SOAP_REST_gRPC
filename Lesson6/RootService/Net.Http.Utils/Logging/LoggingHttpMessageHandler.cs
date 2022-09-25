using EuropAssistance.Net.Http.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

namespace Net.Http.Utils.Logging
{
    public class LoggingHttpMessageHandler : DelegatingHandler
    {
        public LoggingHttpMessageHandler(
            ILogger logger,
            IOptions<HttpClientLoggingOptions> options)
        {
            Logger = logger;
            Options = options.Value;
        }

        public ILogger Logger { get; }

        public HttpClientLoggingOptions Options { get; }

        [DebuggerStepThrough]
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var correlationId = Guid.NewGuid();

            if (Logger.IsEnabled(LogLevel.Information))
            {
                var requestText = new StringBuilder();
                requestText.AppendLine($"Request #{correlationId}");

                var requestUri = request.RequestUri;
                requestText.AppendLine($"Host: {requestUri.Host}");
                requestText.AppendLine($"Path: {requestUri.AbsolutePath}");
                requestText.AppendLine($"QueryString: {requestUri.Query}");
                requestText.AppendLine($"Method: {request.Method}");
                requestText.AppendLine($"Scheme: {requestUri.Scheme}");

                foreach (KeyValuePair<string, IEnumerable<string>> header in request.Headers)
                {
                    foreach (string value in header.Value)
                    {
                        requestText.AppendLine($"{header.Key}: {value}");
                    }
                }

                if (request.Content != null)
                {
                    if (CheckContentType(request.Content.Headers.ContentType))
                    {
                        var requestBody = await request.Content.ReadAsStringAsync();

                        if (requestBody != null && requestBody.Length >= Options.MaxBodyLength)
                        {
                            requestBody = requestBody.Substring(0, Options.MaxBodyLength) + "... <Truncated>";
                        }

                        requestText.AppendLine($"RequestBody:");
                        requestText.AppendLine(requestBody);
                    }
                    else
                    {
   
                        requestText.AppendLine($"RequestBody: <Not Logged>");
                    }
                }


                Logger.LogInformation(requestText.ToString());
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();


            HttpResponseMessage response;
            try
            {
                response = await base.SendAsync(request, cancellationToken);

                stopwatch.Stop();
            }
            catch (Exception exception)
            {
                stopwatch.Stop();

                var responseText = new StringBuilder();
                responseText.AppendLine($"Response #{correlationId}");
                responseText.AppendLine($"Elapsed: {stopwatch.ElapsedMilliseconds}ms");
                responseText.AppendLine($"Exception: {exception.Message}");

                Logger.LogInformation(responseText.ToString());

                throw;
            }


            // response tracing 
            if (Logger.IsEnabled(LogLevel.Information))
            {
                // tracing response
                StringBuilder responseText = new StringBuilder();
                responseText.AppendLine($"Response #{correlationId}");
                responseText.AppendLine($"Elapsed: {stopwatch.ElapsedMilliseconds}ms");
                responseText.AppendLine($"StatusCode: {(int)response.StatusCode} {response.ReasonPhrase}");

                // get response headers
                IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers = response.Headers;

                // add content response headers
                if (response.Content != null && response.Content.Headers != null)
                    headers = headers.Concat(response.Content.Headers);

                // tracing response headers
                foreach (KeyValuePair<string, IEnumerable<string>> header in headers.OrderBy(item => item.Key))
                {
                    foreach (string value in header.Value)
                    {
                        responseText.AppendLine($"{header.Key}: {value}");
                    }
                }

                // tracing response contents
                if (response.Content != null)
                {
                    if (IsTextContent(response.Content.Headers.ContentType))
                    {
                        // fetch body 
                        string responseBody = await response.Content.ReadAsStringAsync();

                        // truncate too long bodies
                        if (responseBody != null && responseBody.Length >= Options.MaxBodyLength)
                        {
                            responseBody = responseBody.Substring(0, Options.MaxBodyLength) + "... <Truncated>";
                        }

                        // append log 
                        responseText.AppendLine($"ResponseBody:");
                        responseText.AppendLine(responseBody);
                    }
                    else
                    {
                        // append log 
                        responseText.AppendLine($"ResponseBody: <Not Logged>");
                    }
                }

                // tracing output
                Logger.LogInformation(responseText.ToString());
            }

            return response;
        }

        private bool CheckContentType(MediaTypeHeaderValue contentType)
        {
            // for multipart form data we allow body logging only
            // when explicitly specified
            if (contentType != null && contentType.MediaType.Contains("multipart/form-data"))
            {
                return Options.LogMultipartFormData;
            }

            // allow body logging 
            return true;
        }

        private bool IsTextContent(MediaTypeHeaderValue contentType)
        {
            return contentType != null &&
                (contentType.MediaType.EndsWith("/json") ||
                 contentType.MediaType.EndsWith("/xml") ||
                 contentType.MediaType.StartsWith("text/"));
        }

    }
}
