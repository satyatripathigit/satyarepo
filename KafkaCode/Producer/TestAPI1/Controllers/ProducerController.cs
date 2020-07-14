using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TestAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private ProducerConfig _config;
        public ProducerController(ProducerConfig config)
        {
            _config = config;
        }

        [HttpPost("Send")]
        public IActionResult Send(string topic, [FromBody]Employee empoyee)
        {
            string serializedEmployee = JsonConvert.SerializeObject(empoyee);

            using (var producer = new ProducerBuilder<Null, string>(_config).Build())
            {
                producer.Produce(topic, new Message<Null, string> { Value = serializedEmployee });
                producer.Flush(TimeSpan.FromSeconds(10));
                return Ok("Message Sent");
            }
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}