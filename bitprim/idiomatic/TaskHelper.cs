using System;
using System.Threading.Tasks;

namespace Bitprim
{
    public static class TaskHelper
    {
        public static async Task<T> ToTask<T>(Func<T> action)
        {
            var tcs = new TaskCompletionSource<T>();

            try
            {
                tcs.TrySetResult(action());
            }
            catch (OperationCanceledException)
            {
                tcs.TrySetCanceled();
            }
            catch (Exception exc)
            {
                tcs.TrySetException(exc);
            }

            return await tcs.Task.ConfigureAwait(false);
        }
    }
}