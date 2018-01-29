using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Bitprim;
using Bitprim.Native;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        private Chain chain_;
        private readonly NodeConfig config_;

        public TransactionController(IOptions<NodeConfig> config, Chain chain)
        {
            config_ = config.Value;
            chain_ = chain;
        }

        // GET: api/tx/{hash}
        [HttpGet("/api/tx/{hash}")]
        public ActionResult GetTransactionByHash(string hash, bool requireConfirmed)
        {
            try
            {
                Utils.CheckIfChainIsFresh(chain_, config_.AcceptStaleRequests);
                byte[] binaryHash = Binary.HexStringToByteArray(hash);
                Tuple<ErrorCode, Transaction, UInt64, UInt64> getTxResult = chain_.GetTransaction(binaryHash, requireConfirmed);
                Utils.CheckBitprimApiErrorCode(getTxResult.Item1, "GetTransaction(" + hash + ") failed, check error log");
                return Json(TxToJSON(getTxResult.Item2, getTxResult.Item3));
            }
            catch(Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET: api/rawtx/{hash}
        [HttpGet("/api/rawtx/{hash}")]
        public ActionResult GetRawTransactionByHash(string hash)
        {
            try
            {
                Utils.CheckIfChainIsFresh(chain_, config_.AcceptStaleRequests);
                byte[] binaryHash = Binary.HexStringToByteArray(hash);
                Tuple<ErrorCode, Transaction, UInt64, UInt64> getTxResult = chain_.GetTransaction(binaryHash, false);
                Utils.CheckBitprimApiErrorCode(getTxResult.Item1, "GetTransaction(" + hash + ") failed, check error log");
                Transaction tx = getTxResult.Item2;
                return Json
                (
                    new
                    {
                        rawtx = Binary.ByteArrayToHexString(tx.ToData(false).Reverse().ToArray())
                    }
                );
            }
            catch(Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET: api/txs/?block=HASH
        [HttpGet("/api/txs/#by_block_hash")]
        public ActionResult GetTransactionsByBlock(string block)
        {
            try
            {
                Utils.CheckIfChainIsFresh(chain_, config_.AcceptStaleRequests);
                Tuple<ErrorCode, Block, UInt64> getBlockResult = chain_.GetBlockByHash(Binary.HexStringToByteArray(block));
                Utils.CheckBitprimApiErrorCode(getBlockResult.Item1, "GetBlockByHash(" + block + ") failed, check error log");
                Block fullBlock = getBlockResult.Item2;
                UInt64 blockHeight = getBlockResult.Item3;
                List<object> txs = new List<object>();
                for(UInt64 i=0; i<fullBlock.TransactionCount; i++)
                {
                    Transaction tx = fullBlock.GetNthTransaction(i);
                    txs.Add(TxToJSON(tx, blockHeight));
                }
                return Json(new
                {
                     pagesTotal = 1, //TODO Implement pagination
                     txs = txs.ToArray()
                });
            }
            catch(Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("/api/txs/#by_address")]
        public ActionResult GetTransactionsByAddress(string addr)
        {
            try
            {
                List<object> txs = GetTransactionsBySingleAddress(addr);
                return Json(new{
                    pagesTotal = 1, //TODO Implement pagination
                    txs = txs.ToArray()
                });
            }
            catch(Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("/api/addrs/{paymentAddresses}/txs")]
        public ActionResult GetTransactionsForMultipleAddresses(string addresses)
        {
            try
            {
                var txs = new List<object>();
                foreach(string address in addresses.Split(","))
                {
                    txs.Concat(GetTransactionsBySingleAddress(address));
                }
                return Json(new{
                    totalItems = txs.Count, //TODO paging
                    from = 0,
                    to = txs.Count,
                    items = txs.ToArray()
                });
            }
            catch(Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost("/api/tx/send")]
        public ActionResult BroadcastTransaction([FromBody] string rawtx)
        {
            try
            {
                Utils.CheckIfChainIsFresh(chain_, config_.AcceptStaleRequests);
                var tx = new Transaction(rawtx);
                ErrorCode ec = chain_.OrganizeTransactionSync(tx);
                Utils.CheckBitprimApiErrorCode(ec, "OrganizeTransaction(" + rawtx + ") failed");
                return Json
                (
                    new
                    {
                        txid = Binary.ByteArrayToHexString(tx.Hash) //TODO Check if this should be returned by organize call
                    }
                );
            }
            catch(Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private List<object> GetTransactionsBySingleAddress(string paymentAddress)
        {
            Utils.CheckIfChainIsFresh(chain_, config_.AcceptStaleRequests);
            Tuple<ErrorCode, HistoryCompactList> getAddressHistoryResult = chain_.GetHistory(new PaymentAddress(paymentAddress), UInt64.MaxValue, 0);
            Utils.CheckBitprimApiErrorCode(getAddressHistoryResult.Item1, "GetHistory(" + paymentAddress + ") failed, check error log.");
            HistoryCompactList history = getAddressHistoryResult.Item2;
            var txs = new List<object>();
            foreach(HistoryCompact compact in history)
            {
                Tuple<ErrorCode, Transaction, UInt64, UInt64> getTxResult = chain_.GetTransaction(compact.Point.Hash, true);
                Utils.CheckBitprimApiErrorCode(getTxResult.Item1, "GetTransaction(" + Binary.ByteArrayToHexString(compact.Point.Hash) + ") failed, check error log");
                txs.Add(TxToJSON(getTxResult.Item2, getTxResult.Item3));
            }
            return txs;
        }

        private object TxToJSON(Transaction tx, UInt64 blockHeight)
        {
            Tuple<ErrorCode, Header, UInt64> getBlockHeaderResult = chain_.GetBlockHeaderByHeight(blockHeight);
            Utils.CheckBitprimApiErrorCode(getBlockHeaderResult.Item1, "GetBlockHeaderByHeight(" + blockHeight + ") failed, check error log");
            Header blockHeader = getBlockHeaderResult.Item2;
            Tuple<ErrorCode, UInt64> getLastHeightResult = chain_.GetLastHeight();
            Utils.CheckBitprimApiErrorCode(getLastHeightResult.Item1, "GetLastHeight failed, check error log");
            return new
            {
                txid = Binary.ByteArrayToHexString(tx.Hash),
                version = tx.Version,
                locktime = tx.Locktime,
                vin = TxInputsToJSON(tx),
                vout = TxOutputsToJSON(tx),
                blockhash = Binary.ByteArrayToHexString(blockHeader.Hash),
                blockheight = blockHeight,
                confirmations = getLastHeightResult.Item2 - blockHeight + 1,
                time = blockHeader.Timestamp,
                blocktime = blockHeader.Timestamp,
                isCoinBase = tx.IsCoinbase,
                valueOut = Utils.SatoshisToBTC(tx.TotalOutputValue),
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
                    byte[] scriptData = input.Script.ToData(false);
                    Array.Reverse(scriptData, 0, scriptData.Length);
                    jsonInput.coinbase = Binary.ByteArrayToHexString(scriptData);
                }
                else
                {
                    SetInputNonCoinbaseFields(jsonInput, input);
                }
                jsonInput.sequence = input.Sequence;
                jsonInput.n = i;
                jsonInputs.Add(jsonInput);
            }
            return jsonInputs.ToArray();
        }

        private void SetInputNonCoinbaseFields(dynamic jsonInput, Input input)
        {
            OutputPoint previousOutput = input.PreviousOutput;
            jsonInput.txid = Binary.ByteArrayToHexString(previousOutput.Hash);
            jsonInput.vout = previousOutput.Index;
            jsonInput.script = InputScriptToJSON(input.Script);
            Tuple<ErrorCode, Transaction, UInt64, UInt64> getTxResult = chain_.GetTransaction(previousOutput.Hash, false);
            Utils.CheckBitprimApiErrorCode(getTxResult.Item1, "GetTransaction(" + Binary.ByteArrayToHexString(previousOutput.Hash) + ") failed, check errog log");
            Output output = getTxResult.Item2.Outputs[(int)previousOutput.Index];
            //TODO Awaiting fix (get_network returning none)
            jsonInput.addr =  output.PaymentAddress(NodeSettings.UseTestnetRules).Encoded;
            jsonInput.valueSat = output.Value;
            jsonInput.value = Utils.SatoshisToBTC(output.Value);
            jsonInput.doubleSpentTxID = null; //We don't handle double spent transactions
        }

        private object InputScriptToJSON(Script inputScript)
        {
            byte[] scriptData = inputScript.ToData(false);
            Array.Reverse(scriptData, 0, scriptData.Length);
            return new
            {
                asm = inputScript.ToString(0),
                hex = Binary.ByteArrayToHexString(scriptData)
            };
        }

        private object TxOutputsToJSON(Transaction tx)
        {
            var outputs = tx.Outputs;
            var jsonOutputs = new List<object>();
            for(var i=0; i<outputs.Count; i++)
            {
                Output output = outputs[i];
                dynamic jsonOutput = new ExpandoObject();
                jsonOutput.value = Utils.SatoshisToBTC(output.Value);
                jsonOutput.n = i;
                jsonOutput.scriptPubKey = OutputScriptToJSON(output);
                SetOutputSpendInfo(jsonOutput, tx.Hash, (UInt32)i);
                jsonOutputs.Add(jsonOutput);
            }
            return jsonOutputs.ToArray();
        }

        private void SetOutputSpendInfo(dynamic jsonOutput, byte[] txHash, UInt32 index)
        {
            Tuple<ErrorCode, Point> fetchSpendResult = chain_.GetSpend(new OutputPoint(txHash, index));
            if(fetchSpendResult.Item1 == ErrorCode.NotFound)
            {
                jsonOutput.spentTxId = null;
                jsonOutput.spentIndex = null;
                jsonOutput.spentHeight = null;
            }
            else
            {
                Utils.CheckBitprimApiErrorCode(fetchSpendResult.Item1, "GetSpend failed, check error log");
                Point spend = fetchSpendResult.Item2;
                jsonOutput.spentTxId = Binary.ByteArrayToHexString(spend.Hash);
                jsonOutput.spentIndex = spend.Index;
                Tuple<ErrorCode, Transaction, UInt64, UInt64> getTxResult = chain_.GetTransaction(spend.Hash, false);
                Utils.CheckBitprimApiErrorCode(getTxResult.Item1, "GetTransaction(" + Binary.ByteArrayToHexString(spend.Hash) + "), check error log");
                jsonOutput.spentHeight = getTxResult.Item3;
            }
        }

        private static object OutputScriptToJSON(Output output)
        {
            Script script = output.Script;
            byte[] scriptData = script.ToData(false);
            Array.Reverse(scriptData, 0, scriptData.Length);
            return new
            {
                asm = script.ToString(0),
                hex = Binary.ByteArrayToHexString(scriptData),
                addresses = ScriptAddressesToJSON(output),
                type = script.Type
            };
        }

        private static object ScriptAddressesToJSON(Output output)
        {
            var jsonAddresses = new List<object>();
            jsonAddresses.Add(output.PaymentAddress(NodeSettings.UseTestnetRules).Encoded);
            return jsonAddresses.ToArray();
        }

    }
}
