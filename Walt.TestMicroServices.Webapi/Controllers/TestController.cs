using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using org.apache.zookeeper;
using org.apache.zookeeper.data;
using Walt.Framework.Service.Kafka;
using Walt.Framework.Service.Zookeeper;

namespace Walt.TestMicroServoces.Webapi
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {


       private IOrderService _orderService;



       public TestController(IOrderService orderService)
       {
            _orderService = orderService;
        }

        // GET api/values
        [HttpGet]
        public async Task<string> Get()
        {
             return  await _orderService.GetOrder();
        }
    }
}
