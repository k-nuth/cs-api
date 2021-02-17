// Copyright (c) 2016-2021 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Threading.Tasks;

namespace Knuth
{
    internal static class TaskHelper
    {
        public static async Task<T> ToTask<T>(Action<TaskCompletionSource<T>> action) {
            var tcs = new TaskCompletionSource<T>();

            try {
                action(tcs);   
            }
            catch (OperationCanceledException) {
                tcs.TrySetCanceled();
            }
            catch (Exception exc) {
                tcs.TrySetException(exc);
            }

            return await tcs.Task.ConfigureAwait(false);
            // return await tcs.Task;
        }
    }
}