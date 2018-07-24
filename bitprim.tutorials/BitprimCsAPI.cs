using Bitprim;
using System;

namespace bitprim.tutorials
{
    public class BitprimCsAPI : IBitprimCsAPI, IDisposable
    {
        private Chain chain_;
        private readonly Executor executor_;

        public BitprimCsAPI(string nodeConfigFile)
        {
            executor_ = new Executor(nodeConfigFile);
        }

        ~BitprimCsAPI()
        {
            Dispose(false);
        }

        public Block GetBlockByHeight(UInt64 height)
        {
            return chain_.FetchBlockByHeightAsync(height).Result.Result.BlockData;
        }

        public Transaction GetTransactionByHash(string txHash)
        {
            return chain_.FetchTransactionAsync(Binary.HexStringToByteArray(txHash), true).Result.Result.Tx;
        }

        public UInt64 GetCurrentBlockchainHeight()
        {
            return chain_.FetchLastHeightAsync().Result.Result;
        }

        public void Dispose(){
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void StartNode()
        {
            var result = executor_.InitAndRunAsync().Result;
            if (result != 0)
            {
                throw new ApplicationException("Executor::InitAndRunAsync failed; error code: " + result);
            }
            chain_ = executor_.Chain;
        }

        protected virtual void Dispose(bool disposing){
            if (disposing)
            {
                //Release managed resources and call Dispose for member variables
            }   
            //Release unmanaged resources
            executor_.Stop();
            executor_.Dispose();
        }
    }
}