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

        private struct AddressBalance
        {
            public List<string> Transactions { get; set;}
            public UInt64 Balance { get; set;}
            public UInt64 Received { get; set; }
            public UInt64 Sent { get; set; }
        }

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
                AddressBalance balance = GetBalance(paymentAddress);
                return Json
                (
                    new
                    {
                        addrStr = paymentAddress,
                        balance = balance.Balance,
                        balanceSat = Utils.SatoshisToBTC(balance.Balance),
                        totalReceived = Utils.SatoshisToBTC(balance.Received),
                        totalReceivedSat = balance.Received,
                        totalSent = balance.Sent,
                        totalSentSat = Utils.SatoshisToBTC(balance.Sent),
                        unconfirmedBalance = 0, //We don't handle unconfirmed txs
                        unconfirmedBalanceSat = 0, //We don't handle unconfirmed txs
                        unconfirmedTxApperances = 0, //We don't handle unconfirmed txs
                        txApperances = balance.Transactions.Count,
                        transactions = balance.Transactions.ToArray()
                    }
                );
            }
            catch(Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET: api/addr/{paymentAddress}/balance
        [HttpGet("/api/addr/{paymentAddress}/balance")]
        public ActionResult GetAddressBalance(string paymentAddress)
        {
            return GetBalanceProperty(paymentAddress, "Balance");
        }

        // GET: api/addr/{paymentAddress}/totalReceived
        [HttpGet("/api/addr/{paymentAddress}/totalReceived")]
        public ActionResult GetTotalReceived(string paymentAddress)
        {
            return GetBalanceProperty(paymentAddress, "Received");
        }

        // GET: api/addr/{paymentAddress}/totalSent
        [HttpGet("/api/addr/{paymentAddress}/totalSent")]
        public ActionResult GetTotalSent(string paymentAddress)
        {
            return GetBalanceProperty(paymentAddress, "Sent");
        }

        // GET: api/addr/{paymentAddress}/unconfirmedBalance
        [HttpGet("/api/addr/{paymentAddress}/unconfirmedBalance")]
        public ActionResult GetUnconfirmedBalance(string paymentAddress)
        {
            try
            {
                Utils.CheckIfChainIsFresh(chain_, config_.AcceptStaleRequests);
                return Json(0); //We don't handle unconfirmed transactions
            }
            catch(Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private AddressBalance GetBalance(string paymentAddress)
        {
            Tuple<ErrorCode, HistoryCompactList> getAddressHistoryResult = chain_.GetHistory(new PaymentAddress(paymentAddress), UInt64.MaxValue, 0);
            Utils.CheckBitprimApiErrorCode(getAddressHistoryResult.Item1, "GetHistory(" + paymentAddress + ") failed, check error log.");
            HistoryCompactList history = getAddressHistoryResult.Item2;
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
            UInt64 totalSent = received - addressBalance;
            return new AddressBalance{ Balance = addressBalance, Received = received, Sent = totalSent, Transactions = txs };
        }

        private ActionResult GetBalanceProperty(string paymentAddress, string propertyName)
        {
            try
            {
                Utils.CheckIfChainIsFresh(chain_, config_.AcceptStaleRequests);
                AddressBalance balance = GetBalance(paymentAddress);
                return Json(balance.GetType().GetProperty(propertyName).GetValue(balance, null));
            }
            catch(Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}