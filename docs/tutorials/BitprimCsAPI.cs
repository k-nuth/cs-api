using Knuth;
using System;

namespace Knuth.tutorials
{
    public class KnuthCsAPI : IKnuthCsAPI, IDisposable
    {
        private IChain chain_;
        private readonly Node node_;

        public KnuthCsAPI(string nodeConfigFile)
        {
            node_ = new Node(nodeConfigFile);
        }

        ~KnuthCsAPI()
        {
            Dispose(false);
        }

        public IBlock GetBlockByHeight(UInt64 height)
        {
            return chain_.GetBlockByHeightAsync(height).Result.Result.BlockData;
        }

        public ITransaction GetTransactionByHash(string txHash)
        {
            return chain_.GetTransactionAsync(Binary.HexStringToByteArray(txHash), true).Result.Result.Tx;
        }

        public UInt64 GetCurrentBlockchainHeight()
        {
            return chain_.GetLastHeightAsync().Result.Result;
        }

        public void Dispose(){
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void StartNode()
        {
            var result = node_.InitAndRunAsync().Result;
            if (result != 0)
            {
                throw new ApplicationException("Node::InitAndRunAsync failed; error code: " + result);
            }
            chain_ = node_.Chain;
        }

        protected virtual void Dispose(bool disposing){
            if (disposing)
            {
                //Release managed resources and call Dispose for member variables
            }   
            //Release unmanaged resources
            node_.Stop();
            node_.Dispose();
        }
    }
}