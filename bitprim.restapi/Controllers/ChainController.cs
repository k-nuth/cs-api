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
        private const string GET_BEST_BLOCK_HASH = "getBestBlockHash";
        private const string GET_LAST_BLOCK_HASH = "getLastBlockHash";
        private const string GET_DIFFICULTY = "getDifficulty";

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

        [HttpGet("/api/status")]
        public ActionResult GetStatus(string method)
        {
            try
            {
                if(method == GET_DIFFICULTY)
                {
                    return GetDifficulty();
                }
                else if(method == GET_BEST_BLOCK_HASH)
                {
                    return GetBestBlockHash();
                }
                else if(method == GET_LAST_BLOCK_HASH)
                {
                    return GetLastBlockHash();
                }
                else
                {
                    return GetInfo();
                }
            }
            catch(Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private ActionResult GetDifficulty()
        {
            return Json(new{});
        }

        private ActionResult GetBestBlockHash()
        {
            return Json(new{});
        }

        private ActionResult GetLastBlockHash()
        {
            return Json(new{});
        }

        private ActionResult GetInfo()
        {
            Tuple<ErrorCode, UInt64> getLastHeightResult = chain_.GetLastHeight();
            Utils.CheckBitprimApiErrorCode(getLastHeightResult.Item1, "GetLastHeight() failed");
            UInt64 currentHeight = getLastHeightResult.Item2;
            return Json
            (
                new
                {
                    info = new 
                    {
                        //version = 120100, //TODO
                        //protocolversion = 70012, //TODO
                        blocks = currentHeight,
                        //timeoffset = 0, //TODO
                        //connections = 8, //TODO
                        //proxy = "", //TODO
                        //difficulty = 2108481.043832448, //TODO
                        testnet = /*NodeSettings.UseTestnetRules*/true //TODO Use NodeSettings when node-cint fixed
                        //relayfee = 0.00001, //TODO
                        //errors = "Warning: unknown new rules activated (versionbit 28)", //TODO
                    },
                    network = /*NodeSettings.NetworkType.ToString()*/ "testnet" //TODO Use NodeSettings when node-cint fixed
                }
            );
        }
    }
}