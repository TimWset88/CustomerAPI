using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace CustomerClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Making http request for token");
            RunAsync().GetAwaiter().GetResult();
        }

        private static async Task RunAsync()
        {
            AuthConfig config = AuthConfig.ReadPropertiesFromFile("C:\\VS Projects\\Commander\\CustomerClient\\appsettings.json");

            IConfidentialClientApplication application;

            application = ConfidentialClientApplicationBuilder.Create(config.ClientId)
                .WithClientSecret(config.ClientSecret)
                .WithAuthority(new Uri("https://login.microsoftonline.com/afdf589f-4ceb-4b10-b924-268d2a3ba113"))
                .Build();

            string[] ResourceIds = new string[] { config.ResourceId };

            AuthenticationResult result = null;

            try
            {
                result = await application.AcquireTokenForClient(ResourceIds).ExecuteAsync();
                Console.WriteLine("Token Aquired \n" + result.AccessToken);
            }
            catch (MsalClientException ex)
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }

            if (!string.IsNullOrEmpty(result.AccessToken))
            {
                var httpClient = new HttpClient();
                var defaultHeaders = httpClient.DefaultRequestHeaders;

                if (defaultHeaders.Accept == null || !defaultHeaders.Accept.Any
                    (m => m.MediaType == "/application/json"))
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new
                        MediaTypeWithQualityHeaderValue("application/json"));
                }

                defaultHeaders.Authorization =
                    new AuthenticationHeaderValue("bearer", result.AccessToken);

                HttpResponseMessage response = await httpClient.GetAsync(config.BaseAddress);

                if(response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(jsonResponse);
                }
                else
                {
                    Console.WriteLine($"Failed to call API: {response.StatusCode}");
                    string content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Content: {content}");
                }
            }


        }
    }
}
