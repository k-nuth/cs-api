using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Bitprim;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        private Chain chain_;
        private readonly IOptions<NodeConfig> config_;

        public TransactionController(IOptions<NodeConfig> config, Chain chain)
        {
            config_ = config;
            chain_ = chain;
        }

        // GET api/values
        // GET: api/tx/{hash}
        [HttpGet("/api/tx/{hash}")]
        public ActionResult GetTransactionByHash(string hash, bool requireConfirmed)
        {
            byte[] binaryHash = Binary.HexStringToByteArray(hash);
            Tuple<int, Transaction, UInt64, UInt64> getTxResult = chain_.GetTransaction(binaryHash, requireConfirmed);
            //TODO Check error code and set HTTP code accordingly
            return Json(TxToJSON(getTxResult.Item2, getTxResult.Item4));
        }

        private object TxToJSON(Transaction tx, UInt64 blockHeight)
        {
            return new
            {
                txid = Binary.ByteArrayToHexString(tx.Hash),
                version = tx.Version,
                locktime = tx.Locktime,
                vin = TxInputsToJSON(tx),
                //vout = TODO,
                //blockhash = TODO
                blockheight = blockHeight,
                //confirmations = TODO,
                //time = TODO,
                //blocktime = TODO,
                isCoinBase = tx.IsCoinbase,
                valueOut = tx.TotalOutputValue,
                size = tx.GetSerializedSize()
            };
        }

        private object TxInputsToJSON(Transaction tx)
        {
            var inputs = tx.Inputs;
            var jsonInputs = new List<object>();
            for(var i=0; i<inputs.Count; i++)
            {
                Input input = inputs[i];
                dynamic jsonInput = new ExpandoObject();
                if(tx.IsCoinbase)
                {
                    jsonInput.coinbase = input.Script.SatoshiContentSize; 
                }
                jsonInputs.Add(jsonInput);
            }
            return jsonInputs.ToArray();
        }

    }
}
