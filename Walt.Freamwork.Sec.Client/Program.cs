using System;
using System.Net.Http;
using IdentityModel.Client;
using IdentityServer4;
using Newtonsoft.Json.Linq;

namespace Walt.Freamwork.Sec.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var disco =  DiscoveryClient.GetAsync("http://localhost:64433/").Result;
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }


            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
            var tokenResponse =  tokenClient.RequestClientCredentialsAsync("api1").Result;

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);

            // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response =  client.GetAsync("http://localhost:50403/api/identity").Result;
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content =  response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(JArray.Parse(content));
            }
        }
    }

     

}
