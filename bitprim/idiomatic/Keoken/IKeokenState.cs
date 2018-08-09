using Bitprim;
using System;

#if KEOKEN

namespace Bitprim.Keoken
{
    // AssetAmount : Int64;
    // AssetId : UInt32;

    public interface IKeokenState : IDisposable
    {
        UInt32 InitialAssetId { set; }

        bool StateAssetIdExists(UInt32 id);

        GetAllAssetsAddressesDataList GetAllAssetAddresses();

        GetAssetsByAddressDataList GetAssetsByAddress(PaymentAddress addr);

        GetAssetsDataList GetAssets();

        Int64 GetBalance(UInt32 id, PaymentAddress addr);

        void CreateAsset(string assetName, Int64 assetAmount, PaymentAddress owner, UInt64 blockHeight, byte[] txId);

        void CreateBalanceEntry(UInt32 assetId, Int64 assetAmount, PaymentAddress source, PaymentAddress target, UInt64 blockHeight, byte[] txId);
    }
}

#endif