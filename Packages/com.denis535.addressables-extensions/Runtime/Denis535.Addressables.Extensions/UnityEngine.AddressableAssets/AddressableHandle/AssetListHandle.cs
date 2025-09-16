#nullable enable
namespace UnityEngine.AddressableAssets {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public class AssetListHandle<T> : AddressableListHandle<T> where T : notnull, UnityEngine.Object {

        // Constructor
        public AssetListHandle(params string[] keys) : base( keys ) {
        }

        // Load
        public AssetListHandle<T> Load() {
            this.Assert_IsNotValid();
            this.Handle = Addressables2.LoadAssetListAsync<T>( this.Keys );
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

        // GetValues
        public IReadOnlyList<T> GetValues() {
            this.Assert_IsValid();
            return this.Handle.GetResult();
        }
        public ValueTask<IReadOnlyList<T>> GetValuesAsync(CancellationToken cancellationToken) {
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
            return "AssetListHandle: " + string.Join( ", ", this.Keys );
        }

    }
}
