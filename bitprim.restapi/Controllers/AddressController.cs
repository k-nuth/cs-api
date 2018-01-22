using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Bitprim;
using System;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class AddressController : Controller
    {
        private Chain chain_;
        private readonly NodeConfig config_;

        public AddressController(IOptions<NodeConfig> config, Chain chain)
        {
            chain_ = chain;
            config_ = config.Value;
        }

        // GET: api/addr/{paymentAddress}
        [HttpGet("/api/addr/{paymentAddress}")]
        public ActionResult GetAddressHistory(string paymentAddress)
        {
            try
            {
                Utils.CheckIfChainIsFresh(chain_, config_.AcceptStaleRequests);
                Tuple<ErrorCode, HistoryCompactList> getAddressHistoryResult = chain_.GetHistory(new PaymentAddress(paymentAddress), UInt64.MaxValue, 0);
                Utils.CheckBitprimApiErrorCode(getAddressHistoryResult.Item1, "GetHistory(" + paymentAddress + ") failed, check error log.");
                HistoryCompactList history = getAddressHistoryResult.Item2;
                UInt64 adressBalance = 0;
                UInt64 received = 0;
                foreach(HistoryCompact compact in history)
                {
                    if(compact.PointKind == PointKind.Output)
                    {
                        received += compact.ValueOrChecksum;
                        Tuple<ErrorCode, Point> getSpendResult = chain_.GetSpend(new OutputPoint(compact.Point.Hash, compact.Point.Index));
                        if(getSpendResult.Item1 == ErrorCode.NotFound)
                        {
                            adressBalance += compact.ValueOrChecksum;
                        }
                    }
                }
                return Json
                (
                    new
                    {
                        addrStr = paymentAddress,
                        balanceSat = Utils.SatoshisToBTC(adressBalance),
                        totalReceived = Utils.SatoshisToBTC(received),
                        totalReceivedSat = received,
                        // "totalSent": 0,
                        // "totalSentSat": 0,
                        // "unconfirmedBalance": 0,
                        // "unconfirmedBalanceSat": 0,
                        // "unconfirmedTxApperances": 0,
                        // "txApperances": 33,
                        // "transactions":,
                        historyCount = history.Count,
                        //network = NodeSettings.NetworkType.ToString(),
                        //currency = NodeSettings.CurrencyType.ToString()
                    }
                );
            }
            catch(Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}