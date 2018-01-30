using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Bitprim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class BlockController : Controller
    {
        private Chain chain_;
        private readonly NodeConfig config_;

        public BlockController(IOptions<NodeConfig> config, Chain chain)
        {
            config_ = config.Value;
            chain_ = chain;
        }

        // GET: api/block/{hash}
        [HttpGet("/api/block/{hash}")]
        public ActionResult GetBlockByHash(string hash)
        {
            try
            {
                Utils.CheckIfChainIsFresh(chain_, config_.AcceptStaleRequests);
                byte[] binaryHash = Binary.HexStringToByteArray(hash);
                Tuple<ErrorCode, Block, UInt64> getBlockResult = chain_.GetBlockByHash(binaryHash);
                Utils.CheckBitprimApiErrorCode(getBlockResult.Item1, "GetBlockByHash(" + hash + ") failed, check error log");
                Tuple<ErrorCode, UInt64> getLastHeightResult = chain_.GetLastHeight();
                Utils.CheckBitprimApiErrorCode(getLastHeightResult.Item1, "GetLastHeight() failed, check error log");
                UInt64 topHeight = getLastHeightResult.Item2;
                UInt64 blockHeight = getBlockResult.Item3;
                Tuple<ErrorCode, Block, UInt64> getNextBlockResult = chain_.GetBlockByHeight(blockHeight + 1);
                Utils.CheckBitprimApiErrorCode(getNextBlockResult.Item1, "GetBlockByHeight(" + blockHeight + 1 + ") failed, check error log");
                return Json(BlockToJSON(getBlockResult.Item2, blockHeight, topHeight, getNextBlockResult.Item2.Hash));
            }
            catch(Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET: api/block-index/{height}
        [HttpGet("/api/block-index/{height}")]
        public ActionResult GetBlockByHeight(UInt64 height)
        {
            try
            {
                Utils.CheckIfChainIsFresh(chain_, config_.AcceptStaleRequests);
                Tuple<ErrorCode, Block, UInt64> getBlockResult = chain_.GetBlockByHeight(height);
                Utils.CheckBitprimApiErrorCode(getBlockResult.Item1, "GetBlockByHeight(" + height + ") failed, error log");
                return Json
                (
                    new
                    {
                        blockHash = Binary.ByteArrayToHexString(getBlockResult.Item2.Hash)
                    }
                );
            }
            catch(Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }            
        }

        // GET: api/rawblock/{hash}
        [HttpGet("/api/rawblock/{hash}")]
        public ActionResult GetRawBlockByHash(string hash)
        {
            try
            {
                Utils.CheckIfChainIsFresh(chain_, config_.AcceptStaleRequests);
                byte[] binaryHash = Binary.HexStringToByteArray(hash);
                Tuple<ErrorCode, Block, UInt64> getBlockResult = chain_.GetBlockByHash(binaryHash);
                Utils.CheckBitprimApiErrorCode(getBlockResult.Item1, "GetBlockByHash(" + hash + ") failed, check error log");
                Block block = getBlockResult.Item2;
                return Json
                (
                    new
                    {
                        rawblock = Binary.ByteArrayToHexString(block.ToData(false).Reverse().ToArray())
                    }
                );
            }
            catch(Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET: api/rawblock-index/{height}
        [HttpGet("/api/rawblock-index/{height}")]
        public ActionResult GetRawBlockByHeight(UInt64 height)
        {
            try
            {
                Utils.CheckIfChainIsFresh(chain_, config_.AcceptStaleRequests);
                Tuple<ErrorCode, Block, UInt64> getBlockResult = chain_.GetBlockByHeight(height);
                Utils.CheckBitprimApiErrorCode(getBlockResult.Item1, "GetBlockByHeight(" + height + ") failed, check error log");
                Block block = getBlockResult.Item2;
                return Json
                (
                    new
                    {
                        rawblock = Binary.ByteArrayToHexString(block.ToData(false).Reverse().ToArray())
                    }
                );
            }
            catch(Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET: api/blocks/?limit={limit}&blockDate={blockDate}
        [HttpGet("/api/blocks/")]
        public ActionResult GetBlocksByDate(UInt64 limit, string blockDate)
        {
            try
            {
                //Find blocks starting point
                Utils.CheckIfChainIsFresh(chain_, config_.AcceptStaleRequests);
                Tuple<ErrorCode, UInt64> getLastHeightResult = chain_.GetLastHeight();
                Utils.CheckBitprimApiErrorCode(getLastHeightResult.Item1, "GetLastHeight failed, check error log");
                UInt64 topHeight = getLastHeightResult.Item2;
                DateTime blockDateToSearch = Convert.ToDateTime(blockDate);
                UInt64 low = FindHighestBlockFromPreviousDay(blockDateToSearch, topHeight);
                if(low == 0) //No blocks
                {
                    return Json(BlocksByDateToJSON(new List<object>(), blockDateToSearch, false, -1));
                }
                //Grab the specified amount of blocks (limit)
                UInt64 startingHeight = low + 1;
                List<object> blocks = GetBlocks(startingHeight, topHeight, limit);
                //Check if there are more blocks: grab the next block
                Tuple<bool, int> moreBlocks = CheckIfMoreBlocks(startingHeight, limit, topHeight, blockDateToSearch);
                return Json(BlocksByDateToJSON(blocks, blockDateToSearch, moreBlocks.Item1, moreBlocks.Item2));
            }
            catch(Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private UInt64 FindHighestBlockFromPreviousDay(DateTime blockDateToSearch, UInt64 topHeight)
        {
            //Adapted binary search
            UInt64 low = 0;
            UInt64 high = topHeight;
            UInt64 mid = 0;
            while(low <= high)
            {
                mid = (UInt64) ((double)low + (double) high/2); //Adds as doubles to prevent overflow
                Tuple<ErrorCode, Block, UInt64> getBlockResult = chain_.GetBlockByHeight(mid);
                Utils.CheckBitprimApiErrorCode(getBlockResult.Item1, "GetBlockByHeight(" + mid + ") failed, check error log");
                if(DateTimeOffset.FromUnixTimeSeconds(getBlockResult.Item2.Header.Timestamp).Date >= blockDateToSearch.Date)
                {
                    high = mid - 1; 
                }else
                {
                    low = mid + 1;
                }
            }
            return low;
        } 

        private List<object> GetBlocks(UInt64 startingHeight, UInt64 topHeight, UInt64 blockCount)
        {
            var blocks = new List<object>();
            for(UInt64 i=0; i<blockCount && startingHeight+i<topHeight; i++)
            {
                Tuple<ErrorCode, Block, UInt64> getBlockResult = chain_.GetBlockByHeight(startingHeight + i);
                Utils.CheckBitprimApiErrorCode(getBlockResult.Item1, "GetBlockByHeight(" + startingHeight + i + ") failed, check error log");
                Block block = getBlockResult.Item2;
                blocks.Add(new
                {
                    height = getBlockResult.Item3,
                    size = block.GetSerializedSize(block.Header.Version),
                    hash = Binary.ByteArrayToHexString(block.Hash),
                    time = block.Header.Timestamp,
                    txLength = block.TransactionCount
                    //TODO Add pool info
                });
            }
            return blocks;
        }

        private Tuple<bool, int> CheckIfMoreBlocks(UInt64 startingHeight, UInt64 limit, UInt64 topHeight, DateTime blockDateToSearch)
        {
            bool moreBlocks = false;
            int moreBlocksTs = -1;
            if(startingHeight + limit >= topHeight)
            {
                moreBlocks = false;
            }
            else
            {
                Tuple<ErrorCode, Block, UInt64> getBlockResult = chain_.GetBlockByHeight(startingHeight + limit);
                Utils.CheckBitprimApiErrorCode(getBlockResult.Item1, "GetBlockByHeight(" + startingHeight + limit + ") failed, check error log");
                Block block = getBlockResult.Item2;
                moreBlocks = DateTimeOffset.FromUnixTimeSeconds(block.Header.Timestamp).Date == blockDateToSearch.Date;
                moreBlocksTs = moreBlocks? (int) block.Header.Timestamp : -1;
            }
            return new Tuple<bool, int>(moreBlocks, moreBlocksTs);
        }

        private static object BlocksByDateToJSON(List<object> blocks, DateTime blockDate, bool moreBlocks, int moreBlocksTs)
        {
            const string dateFormat = "YYYY-MM-dd";
            return new
            {
                blocks = blocks.ToArray(),
                length = blocks.Count,
                pagination = new
                {
                    next = blockDate.Date.AddDays(-1).ToString(dateFormat),
                    prev = blockDate.Date.AddDays(+1).ToString(dateFormat),
                    currentTs = new DateTimeOffset(blockDate).ToUnixTimeSeconds(),
                    current = blockDate.Date.ToString(dateFormat),
                    isToday = (blockDate.Date == DateTime.Now.Date),
                    more = moreBlocks,
                    moreTs = moreBlocks? (object) moreBlocksTs : null
                }
            };
        }

        private static object BlockToJSON(Block block, UInt64 blockHeight, UInt64 topHeight, byte[] nextBlockHash)
        {
            Header blockHeader = block.Header;
            BigInteger proof;
            BigInteger.TryParse(block.Proof, out proof);
            return new
            {
                hash = Binary.ByteArrayToHexString(block.Hash),
                size = block.GetSerializedSize(blockHeader.Version),
                height = blockHeight,
                version = blockHeader.Version,
                merkleroot = Binary.ByteArrayToHexString(block.MerkleRoot),
                tx = BlockTxsToJSON(block),
                time = blockHeader.Timestamp,
                nonce = blockHeader.Nonce,
                bits = Utils.EncodeInBase16(blockHeader.Bits),
                difficulty = Utils.BitsToDifficulty(blockHeader.Bits), //TODO Use bitprim API when implemented
                chainwork = (proof * 2).ToString("X64"), //TODO Does not match Blockdozer value; check how bitpay calculates it
                previousblockhash = Binary.ByteArrayToHexString(blockHeader.PreviousBlockHash),
                nextblockhash = Binary.ByteArrayToHexString(nextBlockHash),
                reward = block.GetBlockReward(blockHeight) / 100000000,
                isMainChain = true, //TODO Check value
                poolInfo = new{} //TODO Check value
            };
        }

        private static object[] BlockTxsToJSON(Block block)
        {
            var txs = new List<object>();
            for(uint i = 0; i<block.TransactionCount; i++)
            {
                txs.Add(Binary.ByteArrayToHexString(block.GetNthTransaction(i).Hash));
            }
            return txs.ToArray();
        }

    }
}