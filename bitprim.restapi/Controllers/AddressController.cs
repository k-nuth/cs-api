using Microsoft.AspNetCore.Mvc;
using Bitprim;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class AddressController : Controller
    {
        private Chain chain_;

        public AddressController(Chain chain)
        {
            chain_ = chain;
        }

        
    }
}