using System;
using System.Threading.Tasks;

namespace Bitprim
{
    public static class TaskHelper
    {
        public static async Task<T> ToTask<T>(Action<TaskCompletionSource<T>> action)
        {
            var tcs = new TaskCompletionSource<T>();

            try
            {
                action(tcs);   
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