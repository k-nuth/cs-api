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
            byte[] binaryHash = Binary.HexStringToByteArray(hash);
            Tuple<int, Block, UInt64> getBlockResult = chain_.GetBlockByHash(binaryHash);
            UInt64 topHeight = chain_.GetLastHeight().Item2;
            // TODO Use error information for HTTP code on failure
            return Json(BlockToJSON(getBlockResult.Item2, getBlockResult.Item3, topHeight));
        }

        // GET: api/block-index/{height}
        [HttpGet("/api/block-index/{height}")]
        public ActionResult GetBlockByHeight(UInt64 height)
        {
            Tuple<int, Block, UInt64> getBlockResult = chain_.GetBlockByHeight(height);
            // TODO Use error information for HTTP code on failure
            return Json
            (
                new
                {
                    blockHash = Binary.ByteArrayToHexString(getBlockResult.Item2.Hash)
                } 
            );
        }

        private object BlockToJSON(Block block, UInt64 blockHeight, UInt64 topHeight)
        {
            return new
            {
                hash = Binary.ByteArrayToHexString(block.Hash),
                size = block.GetSerializedSize(block.Header.Version),
                height = blockHeight,
                version = block.Header.Version,
                merkleroot = Binary.ByteArrayToHexString(block.MerkleRoot),
                tx = BlockTxsToJSON(block),
                time = block.Header.Timestamp,
                nonce = block.Header.Nonce,
                bits = block.Header.Bits,
                //difficulty = TODO,
                //chainwork = TODO,
                confirmations = topHeight - blockHeight + 1,
                previousblockhash = Binary.ByteArrayToHexString(block.Header.PreviousBlockHash),
                //nextblockhash
                reward = block.GetBlockReward(blockHeight)
                //isMainChain = TODO
                //poolInfo = TODO
            };
        }

        private object[] BlockTxsToJSON(Block block)
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