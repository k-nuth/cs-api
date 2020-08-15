using System;
using System.Runtime.InteropServices;

namespace Knuth.Native
{
    internal static class NodeNative
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

        //Functions
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern int executor_initchain(IntPtr exec);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void executor_run(IntPtr exec, IntPtr ctx, [MarshalAs(UnmanagedType.FunctionPtr)]RunNodeHandler handler);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void executor_init_and_run(IntPtr exec, IntPtr ctx, [MarshalAs(UnmanagedType.FunctionPtr)]RunNodeHandler handler);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr executor_construct_fd([MarshalAs(UnmanagedType.LPStr)]string path, int sout, int serr);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr executor_construct_handles([MarshalAs(UnmanagedType.LPStr)]string path, IntPtr sout, IntPtr serr);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void executor_destruct(IntPtr exec);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void executor_stop(IntPtr exec);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_subscribe_blockchain(IntPtr exec, IntPtr chain, IntPtr context, [MarshalAs(UnmanagedType.FunctionPtr)]ReorganizeHandler handler);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_subscribe_transaction(IntPtr exec, IntPtr chain, IntPtr context, [MarshalAs(UnmanagedType.FunctionPtr)]TransactionHandler handler);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr executor_get_chain(IntPtr exec);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern int /*bool*/ executor_stopped(IntPtr exec);

        [DllImport(Constants.KTH_C_LIBRARY, EntryPoint = "node_settings_get_network")]
        public static extern NetworkType executor_get_network(IntPtr node);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern int /*bool*/ executor_load_config_valid(IntPtr exec);

        #if KEOKEN

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr executor_get_keoken_manager(IntPtr exec);

        #endif

        //TODO Add when networking interface is ready
        //[DllImport(Constants.KTH_C_LIBRARY)]
        //public static extern IntPtr executor_get_p2p(IntPtr exec);

    }

}