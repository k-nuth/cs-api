using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Bitprim;
using System;
using System.Collections.Generic;

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
                Tuple<UInt64, List<string>, UInt64> balance = GetBalance(history);
                UInt64 addressBalance = balance.Item1;
                List<string> txs = balance.Item2;
                UInt64 received = balance.Item3;
                return Json
                (
                    new
                    {
                        addrStr = paymentAddress,
                        balance = addressBalance,
                        balanceSat = Utils.SatoshisToBTC(addressBalance),
                        totalReceived = Utils.SatoshisToBTC(received),
                        totalReceivedSat = received,
                        totalSent = received - addressBalance,
                        totalSentSat = Utils.SatoshisToBTC(received - addressBalance),
                        unconfirmedBalance = 0, //We don't handle unconfirmed txs
                        unconfirmedBalanceSat = 0, //We don't handle unconfirmed txs
                        unconfirmedTxApperances = 0, //We don't handle unconfirmed txs
                        txApperances = txs.Count,
                        transactions = txs.ToArray(),
                        historyCount = history.Count
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

        private Tuple<UInt64, List<string>, UInt64> GetBalance(HistoryCompactList history)
        {
            UInt64 received = 0;
            UInt64 addressBalance = 0;
            var txs = new List<string>();
            foreach(HistoryCompact compact in history)
            {
                if(compact.PointKind == PointKind.Output)
                {
                    received += compact.ValueOrChecksum;
                    Tuple<ErrorCode, Point> getSpendResult = chain_.GetSpend(new OutputPoint(compact.Point.Hash, compact.Point.Index));
                    txs.Add(Binary.ByteArrayToHexString(compact.Point.Hash));
                    if(getSpendResult.Item1 == ErrorCode.NotFound)
                    {
                        addressBalance += compact.ValueOrChecksum;
                    }
                }
            }
            return new Tuple<UInt64, List<string>, UInt64>(addressBalance, txs, received);
        }
    }
}