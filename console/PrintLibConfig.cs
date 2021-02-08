// Copyright (c) 2016-2021 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Threading.Tasks;
using Knuth;

namespace console
{
    public class Program {
        static async Task Main(string[] args) {
            var config = Knuth.LibConfig.Get();
            Console.WriteLine("log_library:          {}", config.LogLibrary);
            Console.WriteLine("use_libmdbx:          {}", config.UseLibmdbx);
            Console.WriteLine("version:              {}", config.Version);
            Console.WriteLine("microarchitecture:    {}", config.Microarchitecture);
            Console.WriteLine("microarchitecture_id: {}", config.MicroarchitectureId);
            Console.WriteLine("currency:             {}", config.Currency);
            Console.WriteLine("mempool:              {}", config.Mempool);
            Console.WriteLine("db_mode:              {}", config.DbMode);
            Console.WriteLine("db_readonly:          {}", config.DbReadonly);
            Console.WriteLine("debug_mode:           {}", config.DebugMode);
        }
    }
}