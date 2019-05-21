using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.WebAPI.Controllers
{
    public class ValuesController : BaseController
    {
        public ValuesController(ILogger<ValuesController> logger) : base(logger)
        {
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            Logger.LogDebug("GetRequest");
            return Ok(new string[] { "value1", "value2" });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Logger.LogDebug($"GetRequest {id}");
            if(id == 5)
                return NotFound();
            return Ok($"value{id}");
        }

        [HttpGet("other/{id}")]
        public IActionResult GetValue(int id)
        {
            return Ok($"value{id+1}");
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
