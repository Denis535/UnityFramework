#nullable enable
namespace UnityEngine.ResourceManagement.AsyncOperations {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using UnityEngine;

    public static partial class AsyncOperationHandleExtensions {

        // IsState
        public static bool IsNone(this AsyncOperationHandle handle) {
            return handle.Status == AsyncOperationStatus.None;
        }
        public static bool IsSucceeded(this AsyncOperationHandle handle) {
            return handle.Status == AsyncOperationStatus.Succeeded;
        }
        public static bool IsFailed(this AsyncOperationHandle handle) {
            return handle.Status == AsyncOperationStatus.Failed;
        }

        // Wait
        public static void Wait(this AsyncOperationHandle handle) {
            if (handle.IsFailed()) throw handle.OperationException;
            _ = handle.WaitForCompletion();
            if (handle.IsFailed()) throw handle.OperationException;
        }
        public static async ValueTask WaitAsync(this AsyncOperationHandle handle, CancellationToken cancellationToken) {
            if (handle.IsFailed()) throw handle.OperationException;
            _ = await handle.Task.WaitAsync( cancellationToken );
            cancellationToken.ThrowIfCancellationRequested();
            if (handle.IsFailed()) throw handle.OperationException;
        }

        // GetResult
        public static object? GetResult(this AsyncOperationHandle handle) {
            handle.Wait();
            return handle.Result;
        }
        public static async ValueTask<object?> GetResultAsync(this AsyncOperationHandle handle, CancellationToken cancellationToken) {
            await handle.WaitAsync( cancellationToken );
            return handle.Result;
        }

    }
    public static partial class AsyncOperationHandleExtensions {

        // IsState
        public static bool IsNone<T>(this AsyncOperationHandle<T> handle) {
            return handle.Status == AsyncOperationStatus.None;
        }
        public static bool IsSucceeded<T>(this AsyncOperationHandle<T> handle) {
            return handle.Status == AsyncOperationStatus.Succeeded;
        }
        public static bool IsFailed<T>(this AsyncOperationHandle<T> handle) {
            return handle.Status == AsyncOperationStatus.Failed;
        }

        // Wait
        public static void Wait<T>(this AsyncOperationHandle<T> handle) {
            if (handle.IsFailed()) throw handle.OperationException;
            _ = handle.WaitForCompletion();
            if (handle.IsValid() && handle.IsFailed()) throw handle.OperationException;
        }
        public static async ValueTask WaitAsync<T>(this AsyncOperationHandle<T> handle, CancellationToken cancellationToken) {
            if (handle.IsFailed()) throw handle.OperationException;
            _ = await handle.Task.WaitAsync( cancellationToken );
            cancellationToken.ThrowIfCancellationRequested();
            if (handle.IsValid() && handle.IsFailed()) throw handle.OperationException;
        }

        // GetResult
        public static T GetResult<T>(this AsyncOperationHandle<T> handle) {
            handle.Wait();
            return handle.Result;
        }
        public static async ValueTask<T> GetResultAsync<T>(this AsyncOperationHandle<T> handle, CancellationToken cancellationToken) {
            await handle.WaitAsync( cancellationToken );
            return handle.Result;
        }

        // GetResult
        public static T GetResult<T>(this AsyncOperationHandle<GameObject> handle) where T : Component {
            var result = handle.GetResult();
            return result.GetComponent<T>() ?? throw new InvalidOperationException( $"Component '{typeof( T )}' was not found" );
        }
        public static async ValueTask<T> GetResultAsync<T>(this AsyncOperationHandle<GameObject> handle, CancellationToken cancellationToken) where T : Component {
            var result = await handle.GetResultAsync( cancellationToken );
            return result.GetComponent<T>() ?? throw new InvalidOperationException( $"Component '{typeof( T )}' was not found" );
        }

    }
}
