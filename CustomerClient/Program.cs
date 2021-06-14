using System;
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

            AuthenticationResult result;

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
        }
    }
}
