using Microsoft.AspNetCore.Mvc;
using Bitprim;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class BlockController : Controller
    {
        private Chain chain_;

        public BlockController(Chain chain)
        {
            chain_ = chain;
        }

        // GET api/values
        [HttpGet]
        public string Get()
        {
            return chain_.ToString();
        }
    }
}