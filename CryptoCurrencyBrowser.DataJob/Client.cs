using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptoCurrencyBrowser.DataJob
{
    public interface IClient
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }

    public class Client : IClient
    {
        private static readonly HttpClient _client = new HttpClient();

        private readonly ILogger<Client> _logger;

        private object lockObject = new object();

        static Client()
        {
            _client.Timeout = TimeSpan.FromMinutes(2);
        }

        public Client(ILogger<Client> logger)
        {
            _logger = logger;
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return await _client.SendAsync(request);
        }
    }
}
