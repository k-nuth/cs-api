// Copyright (c) 2016-2024 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

namespace Knuth.Native
{
    // internal
    public static class NodeNative
    {
        //Delegates for callbacks
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void RunNodeHandler(IntPtr node, IntPtr ctx, int error);

        //typedef int (*reorganize_handler_t)(chain_t, void*, error_code_t, uint64_t /*size_t*/, block_list_t, block_list_t);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int ReorganizeHandler(IntPtr node, IntPtr chain, IntPtr context, ErrorCode error, UInt64 u, IntPtr blockList, IntPtr blockList2);

        //typedef int (*transaction_handler_t)(chain_t, void*, error_code_t, transaction_t);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int TransactionHandler(IntPtr node, IntPtr chain, IntPtr context, ErrorCode error, IntPtr transaction);

        // typedef kth_bool_t (*kth_subscribe_ds_proof_handler_t)(kth_node_t, kth_chain_t, void*, kth_error_code_t, kth_double_spend_proof_t);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int DsProofHandler(IntPtr node, IntPtr chain, IntPtr context, ErrorCode error, IntPtr dsp);

        //Functions
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern int kth_node_initchain(IntPtr exec);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_node_run(IntPtr exec, IntPtr ctx, [MarshalAs(UnmanagedType.FunctionPtr)]RunNodeHandler handler);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_node_init_and_run(IntPtr exec, IntPtr ctx, [MarshalAs(UnmanagedType.FunctionPtr)]RunNodeHandler handler);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_node_init_run_and_wait_for_signal(IntPtr exec, IntPtr ctx, StartModules mods, [MarshalAs(UnmanagedType.FunctionPtr)]RunNodeHandler handler);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_node_construct(ref Knuth.Native.Config.Settings settings, int stdout_enabled);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_node_destruct(IntPtr exec);

        // [DllImport(Constants.KTH_C_LIBRARY)]
        // public static extern void kth_node_stop(IntPtr exec);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_node_signal_stop(IntPtr exec);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_node_close(IntPtr exec);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_node_close_on_new_thread(IntPtr exec);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_subscribe_blockchain(IntPtr exec, IntPtr chain, IntPtr context, [MarshalAs(UnmanagedType.FunctionPtr)]ReorganizeHandler handler);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_subscribe_transaction(IntPtr exec, IntPtr chain, IntPtr context, [MarshalAs(UnmanagedType.FunctionPtr)]TransactionHandler handler);

        // void kth_chain_subscribe_ds_proof(kth_node_t exec, kth_chain_t chain, void* ctx, kth_subscribe_ds_proof_handler_t handler);
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_subscribe_ds_proof(IntPtr exec, IntPtr chain, IntPtr context, [MarshalAs(UnmanagedType.FunctionPtr)]DsProofHandler handler);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_node_get_chain(IntPtr exec);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern int /*bool*/ kth_node_stopped(IntPtr exec);

        [DllImport(Constants.KTH_C_LIBRARY, EntryPoint = "kth_node_settings_get_network")]
        public static extern NetworkType kth_node_settings_get_network(IntPtr node);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern int /*bool*/ kth_node_load_config_valid(IntPtr exec);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_node_print_thread_id();

        //TODO(fernando): Add when networking interface is ready
        //[DllImport(Constants.KTH_C_LIBRARY)]
        //public static extern IntPtr kth_node_get_p2p(IntPtr exec);
    }

}