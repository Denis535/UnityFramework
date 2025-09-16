#nullable enable
namespace UnityEngine.AddressableAssets {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public class AssetHandle<T> : AddressableHandle<T> where T : notnull, UnityEngine.Object {

        // Constructor
        public AssetHandle(string key) : base( key ) {
        }

        // Load
        public AssetHandle<T> Load() {
            this.Assert_IsNotValid();
            this.Handle = Addressables2.LoadAssetAsync<T>( this.Key );
            return this;
        }

        // Wait
        public void Wait() {
            this.Assert_IsValid();
            this.Handle.Wait();
        }
        public ValueTask WaitAsync(CancellationToken cancellationToken) {
            this.Assert_IsValid();
            return this.Handle.WaitAsync( cancellationToken );
        }

        // GetValue
        public T GetValue() {
            this.Assert_IsValid();
            return this.Handle.GetResult();
        }
        public ValueTask<T> GetValueAsync(CancellationToken cancellationToken) {
            this.Assert_IsValid();
            return this.Handle.GetResultAsync( cancellationToken );
        }

        // Release
        public void Release() {
            this.Assert_IsValid();
            Addressables.Release( this.Handle );
            this.Handle = default;
        }
        public void ReleaseSafe() {
            if (this.IsValid) {
                this.Release();
            }
        }

        // Utils
        public override string ToString() {
            return "AssetHandle: " + this.Key;
        }

    }
}
