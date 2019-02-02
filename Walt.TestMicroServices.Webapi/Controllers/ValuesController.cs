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
    public class ValuesController : ControllerBase
    {


       private IKafkaService _kafkaService;

       private IZookeeperService _zookeeperService;


       public ValuesController(IKafkaService kafkaService,IZookeeperService zookeeperService)
       {
            _kafkaService=kafkaService;
            _zookeeperService=zookeeperService;
       }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
             await _kafkaService.Producer("my-replicated-topic-morepart","test","你好test");
             return "执行成功了";
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        { 
            string sequenceNode=string.Empty;
            string nodename=_zookeeperService.GetDataByLockNode("/testzookeeper","sequence",ZooDefs.Ids.OPEN_ACL_UNSAFE,out  sequenceNode);
            if(sequenceNode==null)
            {
                return "获取分布式锁失败，请查看日志。";
            }
            
            _zookeeperService.SetDataAsync("/testzookeeper",nodename+"执行了"+DateTime.Now.ToString("yyyyMMddhhmmss"),false);
            if(!string.IsNullOrEmpty(sequenceNode))
            {
                _zookeeperService.DeleteNode(sequenceNode,sequenceNode);
                return "取得锁并且成功处理数据，释放锁成功。";
            }

            return "出现错误，请查日志。";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
