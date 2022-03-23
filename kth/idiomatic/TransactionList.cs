// Copyright (c) 2016-2022 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using Knuth.Native;

namespace Knuth
{
    /// <summary>
    /// Represents a list of Transactions
    /// </summary>
    public class TransactionList : NativeReadableWritableList<Transaction> {
        protected override IntPtr CreateNativeList() {
            return TransactionListNative.kth_chain_transaction_list_construct_default();
        }

        protected override Transaction GetNthNativeElement(UInt64 n) {
            return new Transaction(TransactionListNative.kth_chain_transaction_list_nth(NativeInstance, n), false);
        }

        protected override UInt64 GetCount() {
            return TransactionListNative.kth_chain_transaction_list_count(NativeInstance);
        }

        protected override void AddElement(Transaction element) {
            TransactionListNative.kth_chain_transaction_list_push_back(NativeInstance, element.NativeInstance);
        }

        protected override void DestroyNativeList() {
            TransactionListNative.kth_chain_transaction_list_destruct(NativeInstance);
        }

        internal TransactionList(IntPtr nativeInstance) : base(nativeInstance) {
        }
    }

}