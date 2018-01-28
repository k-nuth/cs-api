using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Bitprim;
using System;
using System.Dynamic;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class ChainController : Controller
    {
        private Chain chain_;
        private readonly NodeConfig config_;

        public ChainController(IOptions<NodeConfig> config, Chain chain)
        {
            config_ = config.Value;
            chain_ = chain;
        }

        [HttpGet("/api/sync")]
        public ActionResult GetSyncStatus()
        {
            try
            {
                //TODO Try a more reliable way to know network max height (i.e. ask another node, or some service)
                Tuple<ErrorCode, UInt64> getLastHeightResult = chain_.GetLastHeight();
                Utils.CheckBitprimApiErrorCode(getLastHeightResult.Item1, "GetLastHeight() failed");
                UInt64 currentHeight = getLastHeightResult.Item2;
                bool synced = currentHeight >= config_.BlockchainHeight;
                dynamic syncStatus = new ExpandoObject();
                syncStatus.status = synced? "finished" : "synchronizing";
                syncStatus.blockChainHeight = config_.BlockchainHeight;
                syncStatus.syncPercentage = Math.Min((double)currentHeight / (double)config_.BlockchainHeight * 100.0, 100);
                syncStatus.error = null;
                syncStatus.type = config_.NodeType;
                return Json(syncStatus);
            }
            catch(Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}