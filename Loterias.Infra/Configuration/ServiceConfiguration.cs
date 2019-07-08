using System;
using System.Net.Http;

namespace Loterias.Infra.Configuration
{
    public class ServiceConfiguration
    {
        internal readonly HttpClient _httpClient;

        internal Uri Endpoint { get; }

        public ServiceConfiguration(Uri baseEndpoint)
        {
            Endpoint = baseEndpoint ?? throw new ArgumentNullException(nameof(baseEndpoint), "Base endpoint cannot be null");
            _httpClient = new HttpClient();
        }
    }
}
