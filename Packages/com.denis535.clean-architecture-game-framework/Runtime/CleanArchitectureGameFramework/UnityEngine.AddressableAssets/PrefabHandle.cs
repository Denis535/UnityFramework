#nullable enable
namespace UnityEngine.AddressableAssets {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public class PrefabHandle<T> : AddressableHandleBase3<T> where T : notnull, Component {

        // Constructor
        public PrefabHandle(string key) : base( key ) {
        }
        public PrefabHandle(string key, AsyncOperationHandle<T> handle) : base( key, handle ) {
        }

        // LoadAsync
        public ValueTask<T> LoadAsync(CancellationToken cancellationToken) {
            Assert_IsNotValid();
            Handle = AddressableHelper.LoadPrefabAsync<T>( Key );
            return Handle.GetResultAsync( cancellationToken );
        }

        // Release
        public void Release() {
            Assert_IsValid();
            Addressables.Release( Handle );
            Handle = default;
        }
        public void ReleaseSafe() {
            if (Handle.IsValid()) {
                Release();
            }
        }

    }
    public class PrefabListHandle<T> : AddressableListHandleBase3<T> where T : notnull, Component {

        // Constructor
        public PrefabListHandle(string[] keys) : base( keys ) {
        }
        public PrefabListHandle(string[] keys, AsyncOperationHandle<IReadOnlyList<T>> handle) : base( keys, handle ) {
        }

        // LoadAsync
        public ValueTask<IReadOnlyList<T>> LoadAsync(CancellationToken cancellationToken) {
            Assert_IsNotValid();
            Handle = AddressableHelper.LoadPrefabListAsync<T>( Keys );
            return Handle.GetResultAsync( cancellationToken );
        }

        // Release
        public void Release() {
            Assert_IsValid();
            Addressables.Release( Handle );
            Handle = default;
        }
        public void ReleaseSafe() {
            if (Handle.IsValid()) {
                Release();
            }
        }

    }
    public class DynamicPrefabHandle<T> : DynamicAddressableHandleBase3<T> where T : notnull, Component {

        // Constructor
        public DynamicPrefabHandle() {
        }
        public DynamicPrefabHandle(string key, AsyncOperationHandle<T> handle) : base( key, handle ) {
        }

        // LoadAsync
        public ValueTask<T> LoadAsync(string key, CancellationToken cancellationToken) {
            Assert_IsNotValid();
            Handle = AddressableHelper.LoadPrefabAsync<T>( Key = key );
            return Handle.GetResultAsync( cancellationToken );
        }

        // Release
        public void Release() {
            Assert_IsValid();
            Addressables.Release( Handle );
            Handle = default;
        }
        public void ReleaseSafe() {
            if (Handle.IsValid()) {
                Release();
            }
        }

    }
    public class DynamicPrefabListHandle<T> : DynamicAddressableListHandleBase3<T> where T : notnull, Component {

        // Constructor
        public DynamicPrefabListHandle() {
        }
        public DynamicPrefabListHandle(string[] keys, AsyncOperationHandle<IReadOnlyList<T>> handle) : base( keys, handle ) {
        }

        // LoadAsync
        public ValueTask<IReadOnlyList<T>> LoadAsync(string[] keys, CancellationToken cancellationToken) {
            Assert_IsNotValid();
            Handle = AddressableHelper.LoadPrefabListAsync<T>( Keys = keys );
            return Handle.GetResultAsync( cancellationToken );
        }

        // Release
        public void Release() {
            Assert_IsValid();
            Addressables.Release( Handle );
            Handle = default;
        }
        public void ReleaseSafe() {
            if (Handle.IsValid()) {
                Release();
            }
        }

    }
}
