using Microsoft.AspNetCore.Mvc;
using Bitprim;
using System;
using System.Collections.Generic;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class BlockController : Controller
    {
        private Chain chain_;

        public BlockController(Chain chain)
        {
            chain_ = chain;
        }

        // GET: api/block/{hash}
        [HttpGet("/api/block/{hash}")]
        public ActionResult GetBlockByHash(string hash)
        {
            try
            {
                byte[] binaryHash = Binary.HexStringToByteArray(hash);
                Tuple<int, Block, UInt64> getBlockResult = chain_.GetBlockByHash(binaryHash);
                Utils.CheckBitprimApiErrorCode(getBlockResult.Item1, "GetBlockByHash(" + hash + ") failed, check error log");
                Tuple<int, UInt64> getLastHeightResult = chain_.GetLastHeight();
                Utils.CheckBitprimApiErrorCode(getLastHeightResult.Item1, "GetLastHeight() failed, check error log");
                UInt64 topHeight = getLastHeightResult.Item2;
                UInt64 blockHeight = getBlockResult.Item3;
                Tuple<int, Block, UInt64> getNextBlockResult = chain_.GetBlockByHeight(blockHeight + 1);
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
                Tuple<int, Block, UInt64> getBlockResult = chain_.GetBlockByHeight(height);
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

        private static object BlockToJSON(Block block, UInt64 blockHeight, UInt64 topHeight, byte[] nextBlockHash)
        {
            Header blockHeader = block.Header;
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
                difficulty = BitsToDifficulty(blockHeader.Bits), //TODO Use bitprim API when implemented
                //chainwork = TODO,
                confirmations = topHeight - blockHeight + 1,
                previousblockhash = Binary.ByteArrayToHexString(blockHeader.PreviousBlockHash),
                nextblockhash = Binary.ByteArrayToHexString(nextBlockHash),
                reward = block.GetBlockReward(blockHeight)
                //isMainChain = TODO
                //poolInfo = TODO
            };
        }

        //TODO Remove this when bitprim wrapper implemented
        private static double BitsToDifficulty(UInt32 bits)
        {
            double diff = 1.0;
            int shift = (int) (bits >> 24) & 0xff;
            diff = (double)0x0000ffff / (double)(bits & 0x00ffffff);
            while (shift < 29)
            {
                diff *= 256.0;
                ++shift;
            }
            while (shift > 29)
            {
                diff /= 256.0;
                --shift;
            }
            return diff;
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