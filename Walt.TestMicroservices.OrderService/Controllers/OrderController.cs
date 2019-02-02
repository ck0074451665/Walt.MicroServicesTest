
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
namespace Walt.TestMicroservices.OrderService
{ 
    [Route("api/[Controller]")]
    [ApiController]
    //[ApiVersion("1.0")]
    public class OrderController:ControllerBase
    {
        ///获取订单
         [HttpGet()]
        public async Task<ActionResult<string>> Get()
        {
            return  await Task.FromResult("order服务调用成功。");
        }
    }
}