using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Bitprim;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        private readonly IOptions<NodeConfig> config_;

        public TransactionController(IOptions<NodeConfig> config)
        {
            config_ = config;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2", "value3" };
        }

        // GET api/values/5
        // [HttpGet("{id}")]
        // public string Get(int id)
        // {
        //     return "value";
        // }

        // POST api/values
        // [HttpPost]
        // public void Post([FromBody]string value)
        // {
        // }

        // PUT api/values/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody]string value)
        // {
        // }

        // DELETE api/values/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }

        [HttpGet("Validate")]
        public bool Validate(string tx)
        {
            var exec = new Executor(config_.Value.NodeConfigFile, 0, 0);
            //var s = config_.Value;
            return true;
        }
    }
}
