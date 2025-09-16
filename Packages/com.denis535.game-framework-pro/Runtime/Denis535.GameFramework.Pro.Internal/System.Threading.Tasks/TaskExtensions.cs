#nullable enable
namespace System.Threading.Tasks {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static class TaskExtensions {

        // WaitAsync
        public static async Task WaitAsync(this Task task, CancellationToken cancellationToken) {
            if (cancellationToken.IsCancellationRequested) {
                throw new OperationCanceledException( cancellationToken );
            }
            if (!cancellationToken.CanBeCanceled) {
                await task.ConfigureAwait( false );
                return;
            }
            if (task.IsCompleted) {
                await task.ConfigureAwait( false );
                return;
            }
            using (cancellationToken.WaitAsync( out var cancellationTokenTask )) {
                var anyTask = await Task.WhenAny( task, cancellationTokenTask ).ConfigureAwait( false );
                if (anyTask == cancellationTokenTask) {
                    throw new OperationCanceledException( cancellationToken );
                }
            }
            await task.ConfigureAwait( false );
        }

        // WaitAsync
        public static async Task<T> WaitAsync<T>(this Task<T> task, CancellationToken cancellationToken) {
            if (cancellationToken.IsCancellationRequested) {
                throw new OperationCanceledException( cancellationToken );
            }
            if (!cancellationToken.CanBeCanceled) {
                return await task.ConfigureAwait( false );
            }
            if (task.IsCompleted) {
                return await task.ConfigureAwait( false );
            }
            using (cancellationToken.WaitAsync( out var cancellationTokenTask )) {
                var anyTask = await Task.WhenAny( task, cancellationTokenTask ).ConfigureAwait( false );
                if (anyTask == cancellationTokenTask) {
                    throw new OperationCanceledException( cancellationToken );
                }
            }
            return await task.ConfigureAwait( false );
        }

        // Throw
        public static async void Throw(this Task task) {
            await task;
        }

        // Helpers
        private static CancellationTokenRegistration WaitAsync(this CancellationToken cancellationToken, out Task cancellationTokenTask) {
            var tcs = new TaskCompletionSource<object?>();
            cancellationTokenTask = tcs.Task;
            return cancellationToken.Register( tcs => ((TaskCompletionSource<object?>) tcs).SetResult( null ), tcs );
        }

    }
}
