using Microsoft.AspNetCore.Mvc;
using Bitprim;
using System;

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

        // GET: api/addr/{paymentAddress}
        [HttpGet("/api/addr/{paymentAddress}")]
        public ActionResult GetAddressHistory(string paymentAddress)
        {
            try
            {
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
                        historyCount = history.Count 
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