using Knuth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Knuth.Tutorials
{
    public class MemoService
    {
        private readonly IKnuthCsAPI kthApi_;
        private readonly Regex memoRegex_;
    
        public MemoService(IKnuthCsAPI kthApi) {
            kthApi_ = kthApi;
            memoRegex_ = new Regex("^return " + Regex.Escape("[") + "6d[0-1][1-e]" + Regex.Escape("]"), RegexOptions.Compiled);
        }

        public bool TransactionIsMemo(string txHash) {
            using(ITransaction tx = kthApi_.GetTransactionByHash(txHash)) {
                return TransactionIsMemo(tx);
            }
        }

        public string GetPost(string txHash) {
            using(ITransaction tx = kthApi_.GetTransactionByHash(txHash)) {
                return GetPost(tx);
            }
        }

        public List<string> GetLatestPosts(int nPosts, Action<string> progressReportCallback) {
            UInt64 blockchainHeight = kthApi_.GetCurrentBlockchainHeight();
            int postsFound = 0;
            var posts = new List<string>();
            while (postsFound < nPosts && blockchainHeight > 1) {
                progressReportCallback("Searching block " + blockchainHeight + "...");
                using(IBlock block = kthApi_.GetBlockByHeight(blockchainHeight)) {
                    for (uint iTx=0; iTx<block.TransactionCount; ++iTx)
                    {
                        progressReportCallback("\tSearching tx " + (iTx+1) + "/" + block.TransactionCount + "...");
                        using(var tx = block.GetNthTransaction(iTx))
                        {
                            if (TransactionIsMemo(tx))
                            {
                                posts.Add(GetPost(tx));
                                ++postsFound;
                                progressReportCallback("\t\tFound post " + postsFound + " of " + nPosts + " in tx " + Binary.ByteArrayToHexString(tx.Hash) + "!");
                                if (postsFound == nPosts)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                blockchainHeight--;
            }
            return posts;
        }

        private bool TransactionIsMemo(ITransaction tx) {
            foreach(Output output in tx.Outputs) {
                if (memoRegex_.Match(output.Script.ToString(0)).Success) {
                    return true;
                }
            }
            return false;
        }

        private static byte[] HexStringToBytes(string hexString) {
            if (hexString == null) {
                throw new ArgumentNullException("hexString");
            }
            if (hexString.Length % 2 != 0) {
                throw new ArgumentException("hexString must have an even length", "hexString");
            }
            var bytes = new byte[hexString.Length / 2];
            for (int i = 0; i < bytes.Length; i++) {
                string currentHex = hexString.Substring(i * 2, 2);
                bytes[i] = Convert.ToByte(currentHex, 16);
            }
            return bytes;
        }

        private string GetPost(ITransaction tx) {
            foreach(Output output in tx.Outputs) {
                string outputScript = output.Script.ToString(0);
                if (!memoRegex_.Match(outputScript).Success) {
                    continue;
                }
                int iStart = outputScript.LastIndexOf("[");
                string toDecode = outputScript.Substring(iStart + 1, outputScript.Length - (iStart + 2));
                byte[] bytesToDecode = HexStringToBytes(toDecode);
                return Encoding.GetEncoding("UTF-8").GetString(bytesToDecode);
            }
            return "";
        }
    }
}