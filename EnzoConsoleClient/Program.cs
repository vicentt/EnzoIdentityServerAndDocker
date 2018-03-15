using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace EnzoConsoleClient
{
    class Program
    {
        static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();


        private static async Task MainAsync()
        {
            var disco = await DiscoveryClient.GetAsync("http://localhost:8080");
            if (disco.IsError)
            { Console.WriteLine(disco.Error); return; }

            var tokenClient = new TokenClient(disco.TokenEndpoint, "Client", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

            if (tokenResponse.IsError)
            { Console.Write(tokenResponse.Error); return; }

            Console.WriteLine(tokenResponse.Json);

            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            while (true)
            {
                var response = await client.GetAsync("http://localhost:8081/identity");
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.StatusCode);
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(JArray.Parse(content));
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(JArray.Parse(content));
                }
            }
        }
    }
}
