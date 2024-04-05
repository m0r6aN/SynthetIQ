// *********************************************************************** Assembly :
// MF.Libraries.Core Author : clint.morgan Created : 04-01-2022
//
// Last Modified By : clint.morgan Last Modified On : 05-06-2022 ***********************************************************************
// <copyright file="HttpHelpers.cs" company="OTR Solutions, LLC">
//     OTR Solutions, LLC 2022
// </copyright>
// <summary>
// </summary>
// ***********************************************************************
namespace SynthetIQ.Utility.Helpers
{
    /// <summary>
    /// Class HttpHelpers.
    /// </summary>
    [RegisterService]
    public sealed class HttpHelpers
    {
        /// <summary>
        /// Querystring Key Value Pairs
        /// </summary>
        public Dictionary<string, string> KvPs;

        /// <summary>
        /// Dynamically built string passed to endpoints
        /// </summary>
        public string QueryString = string.Empty;

        /// <summary>
        /// SynthetIQ to append after BaseUrl
        /// Example: Clients http://localhost:7071/Clients
        /// </summary>
        public string UrlSuffix = string.Empty;

        /// <summary>
        /// The cancellation token
        /// </summary>
        protected readonly CancellationToken CancellationToken = new();

        /// <summary>
        /// Required to use all endpoints except for DialPad related and public
        /// </summary>
        private readonly Dictionary<string, string> _accessKvP;

        /// <summary>
        /// The disposed value
        /// </summary>
        private bool _disposedValue;

        /// <summary>
        /// Base Url for Azure Function endpoints
        /// </summary>
        public string BaseUrl { get; set; }

        public HttpHelpers()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpHelpers"/> class.
        /// </summary>
        /// <param name="baseUrl"> The base URL. </param>
        public HttpHelpers(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpHelpers"/> class.
        /// </summary>
        /// <param name="baseUrl">   The base URL. </param>
        /// <param name="accessKvP"> The access kv p. </param>
        public HttpHelpers(string baseUrl, Dictionary<string, string> accessKvP)
        {
            BaseUrl = baseUrl;
            _accessKvP = accessKvP;
        }

        /// <summary>
        /// Creates a HttpContent instance
        /// </summary>
        /// <param name="content">           The content. </param>
        /// <param name="additionalHeaders"> The additional Headers. </param>
        /// <returns> HttpContent. </returns>
        public HttpContent CreateHttpContent(object content, Dictionary<string, string>? additionalHeaders = null)
        {
            HttpContent httpContent = new StringContent(content.ToString() ?? string.Empty);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            httpContent.Headers.Add(_accessKvP.Keys.First(), _accessKvP.Values.First());

            if (additionalHeaders != null && additionalHeaders.Any())
            {
                foreach (var additionalHeader in additionalHeaders)
                {
                    httpContent.Headers.Add(additionalHeader.Key, additionalHeader.Value);
                }
            }

            return httpContent;
        }

        /// <summary>
        /// Returns a URL as a string
        /// </summary>
        /// <param name="route"> The route. </param>
        /// <param name="kvPs">  The kv ps. </param>
        /// <returns> System.String. </returns>
        public string BuildHttpGetUrl(string route, Dictionary<string, string> kvPs)
        {
            var sb = new StringBuilder($"{BaseUrl}/{route}");
            int i = 0;

            foreach (KeyValuePair<string, string> kvp in kvPs)
            {
                sb.Append(i == 0 ? $"?{kvp.Key}={kvp.Value}" : $"&{kvp.Key}={kvp.Value}");
                i++;
            }

            var qs = sb.ToString();
            sb.Clear();

            return qs;
        }

        /// <summary>
        /// Returns a Uri
        /// </summary>
        /// <param name="route"> The route. </param>
        /// <param name="kvPs">  The kv ps. </param>
        /// <returns> Uri. </returns>
        public Uri BuildHttpGetUri(string route, Dictionary<string, string> kvPs)
        {
            var sb = new StringBuilder($"{BaseUrl}/{route}");
            int i = 0;

            foreach (KeyValuePair<string, string> kvp in kvPs)
            {
                sb.Append(i == 0 ? $"?{kvp.Key}={kvp.Value}" : $"&{kvp.Key}={kvp.Value}");
                i++;
            }

            var qs = sb.ToString();
            sb.Clear();

            return new Uri(qs);
        }

        /// <summary>
        /// Builds the HTTP post put URI.
        /// </summary>
        /// <param name="route"> The route. </param>
        /// <returns> Uri. </returns>
        public Uri BuildHttpPostPutUri(string route)
        {
            var sb = new StringBuilder($"{BaseUrl}/{route}");
            var qs = sb.ToString();
            sb.Clear();

            return new Uri(qs);
        }

        /// <summary>
        /// Builds the query string.
        /// </summary>
        /// <param name="keyValuePairs"> The key value pairs. </param>
        /// <returns> System.String. </returns>
        protected string BuildQueryString(IDictionary<string, string> keyValuePairs)
        {
            StringBuilder sb = new StringBuilder("");
            int i = 0;

            foreach (KeyValuePair<string, string> kvp in keyValuePairs)
            {
                sb.Append(i == 0 ? $"?{kvp.Key}={kvp.Value}" : $"&{kvp.Key}={kvp.Value}");
                i++;
            }

            var qs = sb.ToString();
            sb.Clear();

            return qs;
        }

        /// <summary>
        /// Creates the json HTTP content.
        /// </summary>
        /// <param name="content">           </param>
        /// <param name="additionalHeaders"> </param>
        /// <returns> </returns>
        public HttpContent CreateJsonHttpContent(object content, Dictionary<string, string>? additionalHeaders = null)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            if (_accessKvP != null && _accessKvP.Any())
            {
                jsonContent.Headers.Add(_accessKvP.Keys.First(), _accessKvP.Values.First());
            }

            if (additionalHeaders != null)
            {
                foreach (var header in additionalHeaders)
                {
                    jsonContent.Headers.Add(header.Key, header.Value);
                }
            }

            return jsonContent;
        }
    }
}