using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Steeltoe.Common.Discovery;

namespace Walt.TestMicroServoces.Webapi
{
    public class OrderService:IOrderService
    {
        private IConfiguration _config;

        private HttpClient _httpClient;

        public OrderService(IConfiguration config
        ,HttpClient httpClient)
        {
            _config=config;
            _httpClient = httpClient;
        }  
        // DiscoveryHttpClientHandler _handler;

        // public OrderService(IDiscoveryClient client, ILoggerFactory logFactory = null)
        // {

        //     _handler = new DiscoveryHttpClientHandler(client, logFactory?.CreateLogger<DiscoveryHttpClientHandler>());
        // }
        public async Task<string> GetOrder()
        {
           // _httpClient = new HttpClient(_handler,false); 

            var task=await _httpClient.GetStringAsync("http://ORDERSERVICE/api/Order/");
            return task;
        }
    }
}