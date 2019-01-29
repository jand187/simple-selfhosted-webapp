using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HalIntegration.Common;
using Newtonsoft.Json;

namespace ClientConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            while (true)
            {
                var line = Console.ReadLine();

                if (line.ToLower().StartsWith("q"))
                {
                    break;
                }

                if (line.ToLower().StartsWith("createsession"))
                {
                    CreateSessionAsync(line).Wait();
                }
            }
        }

        private static async Task CreateSessionAsync(string line)
        {
            // type: "createsession -id abc123" in console.

            var pattern = @"createsession -id (?<sessionId>[a-zA-Z0-9]+)$";
            var match = Regex.Match(line, pattern, RegexOptions.IgnoreCase);
            if (match.Success)
            {
                var sessionId = match.Groups["sessionId"].Value;
                
                var sessionRequest = new SessionRequest {SessionId = sessionId};

                await PostRequest(sessionRequest);
            }
        }

        private static async Task PostRequest(SessionRequest sessionRequest)
        {
            //var client = new ApiClient<SessionRequest, string>(new Uri("https://localhost:44301"), "api/session");

            //var result = await client.PostRequest(sessionRequest);
            //Console.WriteLine($"Session {result} was created.");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44301");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = JsonConvert.SerializeObject(sessionRequest);

                var response = await client.PostAsync(
                    "api/session",
                    new StringContent(content, Encoding.UTF8, "application/json"));

                var x = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Session {x} was created.");
                }
            }
        }
    }

    public class ApiClient<TRequest, TResponse>
    {
        private readonly Uri clientBaseAddress;
        private readonly string requestUri;

        public ApiClient(Uri baseAddress, string requestUri)
        {
            this.clientBaseAddress = baseAddress;
            this.requestUri = requestUri;
        }

        public async Task<TResponse> PostRequest(TRequest request)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = this.clientBaseAddress;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = JsonConvert.SerializeObject(request);

                var response = await client.PostAsync(
                    this.requestUri,
                    new StringContent(content, Encoding.UTF8, "application/json"));

                var stringValue = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResponse>(stringValue);
            }
        }

    }
}
