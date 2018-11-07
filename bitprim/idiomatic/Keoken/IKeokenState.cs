using Bitprim;
using System;
using System.Collections.Generic;
using Bitprim.Native;

#if KEOKEN

namespace Bitprim.Keoken
{
    // AssetAmount : Int64;
    // AssetId : UInt32;

    public interface IKeokenState : IDisposable
    {
        UInt32 InitialAssetId { set; }

        void RemoveUpTo(UInt64 height);

        void Reset();

        bool StateAssetIdExists(UInt32 id);
         
        INativeList<IGetAllAssetsAddressesData> GetAllAssetAddresses();

        INativeList<IGetAssetsByAddressData> GetAssetsByAddress(PaymentAddress addr);

        INativeList<IGetAssetsData> GetAssets();

        Int64 GetBalance(UInt32 id, PaymentAddress addr);

        void CreateAsset(string assetName, Int64 assetAmount, PaymentAddress owner, UInt64 blockHeight, byte[] txId);

        void CreateBalanceEntry(UInt32 assetId, Int64 assetAmount, PaymentAddress source, PaymentAddress target, UInt64 blockHeight, byte[] txId);
    }
}

#endif