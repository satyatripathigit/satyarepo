using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestConsumerAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumerController : ControllerBase
    {
        private ConsumerConfig _config;

        public ConsumerController(ConsumerConfig config)
        {
            _config = config;
        }

        [HttpGet("GetData/{token}")]
        public IActionResult GetData(string token)
        {
            using (var consumer = new ConsumerBuilder<Null, string>(_config).Build())
            {
                consumer.Subscribe(token);
                var cr = consumer.Consume();
                

                return Ok(cr.Message.Value);
            }
        }
    }
}