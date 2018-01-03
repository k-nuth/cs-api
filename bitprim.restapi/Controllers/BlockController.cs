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

        // GET api/values
        [HttpGet]
        public string Get()
        {
            return chain_.ToString();
        }

        // GET: api/Block/{hash}
        [HttpGet("/api/Block/{hash}")]
        public ActionResult GetBlockByHash(string hash)
        {
            byte[] binaryHash = Binary.HexStringToByteArray(hash);
            Tuple<int, Block, UInt64> getBlockResult = chain_.GetBlockByHash(binaryHash);
            dynamic jsonBlock = Json(getBlockResult.Item2);
            jsonBlock.Value.hash = Binary.ByteArrayToHexString(jsonBlock.Value.hash);
            return Json
            (
                new
                {            
                    error_code = getBlockResult.Item1,
                    block = jsonBlock,
                    height = getBlockResult.Item3
                }
            );
        }

    }
}