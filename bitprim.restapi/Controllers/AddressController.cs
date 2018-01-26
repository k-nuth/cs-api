using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Bitprim;
using System;
using System.Collections.Generic;
using System.Linq;

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

        // GET: api/addr/{paymentAddress}/utxo
        [HttpGet("/api/addr/{paymentAddress}/utxo")]
        public ActionResult GetUtxoForSingleAddress(string paymentAddress)
        {
            try
            {
                List<object> utxo = GetUtxo(paymentAddress);
                return Json(utxo.ToArray());
            }
            catch(Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpGet("/api/addrs/{paymentAddresses}/utxo")]
        public ActionResult GetUtxoForMultipleAddresses(string addresses)
        {
            try
            {
                var utxo = new List<object>();
                foreach(string address in addresses.Split(","))
                {
                    utxo.Concat(GetUtxo(address));
                }
                return Json(utxo.ToArray());
            }
            catch(Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost("/api/addrs/utxo")]
        public ActionResult GetUtxoForMultipleAddressesPost([FromBody] string addrs)
        {
            return GetUtxoForMultipleAddresses(addrs);
        }

        private List<object> GetUtxo(string paymentAddress)
        {
            Utils.CheckIfChainIsFresh(chain_, config_.AcceptStaleRequests);
            Tuple<ErrorCode, HistoryCompactList> getAddressHistoryResult = chain_.GetHistory(new PaymentAddress(paymentAddress), UInt64.MaxValue, 0);
            Utils.CheckBitprimApiErrorCode(getAddressHistoryResult.Item1, "GetHistory(" + paymentAddress + ") failed, check error log.");
            HistoryCompactList history = getAddressHistoryResult.Item2;
            var utxo = new List<dynamic>();
            Tuple<ErrorCode, UInt64> getLastHeightResult = chain_.GetLastHeight();
            Utils.CheckBitprimApiErrorCode(getLastHeightResult.Item1, "GetLastHeight failed, check error log");
            UInt64 topHeight = getLastHeightResult.Item2;
            foreach(HistoryCompact compact in history)
            {
                if(compact.PointKind == PointKind.Output)
                {
                    Tuple<ErrorCode, Point> getSpendResult = chain_.GetSpend(new OutputPoint(compact.Point.Hash, compact.Point.Index));
                    ErrorCode errorCode = getSpendResult.Item1;
                    Point outputPoint = getSpendResult.Item2;
                    if(errorCode == ErrorCode.NotFound) //Unspent = it's an utxo
                    {
                        //Get the tx to get the script
                        Tuple<ErrorCode, Transaction, UInt64, UInt64> getTxResult = chain_.GetTransaction(outputPoint.Hash, true);
                        ErrorCode getTxEc = getTxResult.Item1;
                        Transaction tx = getTxResult.Item2;
                        utxo.Add(UtxoToJSON(paymentAddress, outputPoint, getTxEc, tx, compact, topHeight));
                    }
                }
            }
            return utxo;
        }

        private static object UtxoToJSON(string paymentAddress, Point outputPoint, ErrorCode getTxEc, Transaction tx, HistoryCompact compact, UInt64 topHeight)
        {
            return new
            {
                address = paymentAddress,
                txid = Binary.ByteArrayToHexString(outputPoint.Hash),
                vout = outputPoint.Index,
                scriptPubKey = getTxEc == ErrorCode.Success? tx.Outputs[(int)outputPoint.Index].Script.ToData(false) : null,
                amount = Utils.SatoshisToBTC(compact.ValueOrChecksum),
                satoshis = compact.ValueOrChecksum,
                height = compact.Height,
                confirmations = topHeight - compact.Height
            };
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