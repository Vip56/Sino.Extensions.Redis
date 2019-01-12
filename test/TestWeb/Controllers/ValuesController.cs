using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Sino.Extensions.Redis;

namespace TestWeb.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IDistributedCache _cache;

        public ValuesController(IDistributedCache cache)
        {
            _cache = cache;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            _cache.Append("123", "test123");
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
