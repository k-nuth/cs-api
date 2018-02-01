using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Bitprim;
using System;
using System.Dynamic;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class PeerController : Controller
    {
        [HttpGet("/api/peer")]
        public ActionResult GetPeerStatus()
        {
            try
            {
                //TODO Get this information from node-cint
                dynamic peerStatus = new ExpandoObject();
                peerStatus.connected = true;
                peerStatus.host = "127.0.0.1";
                peerStatus.port = null;
                return Json(peerStatus);
            }
            catch(Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}