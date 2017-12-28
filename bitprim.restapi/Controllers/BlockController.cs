using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class BlockController : Controller
    {
        // GET api/values
        [HttpGet]
        public string Get()
        {
            return "Block test";
        }
    }
}