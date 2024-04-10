#nullable enable
namespace UnityEngine.AddressableAssets {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;
    using UnityEngine;

    public class AssetHandle<T> : AddressableAssetHandle<T> where T : notnull, UnityEngine.Object {

        public string Key { get; }

        // Constructor
        public AssetHandle(string key) {
            Key = key;
        }

        // LoadAsync
        public Task<T> LoadAsync(CancellationToken cancellationToken) {
            Assert_IsNotValid();
            Handle = AddressableHandleHelper.LoadAssetAsync<T>( Key );
            return Handle.GetResultAsync( cancellationToken );
        }

    }
    public class AssetListHandle<T> : AddressableAssetHandle<IReadOnlyList<T>> where T : notnull, UnityEngine.Object {

        public string[] Keys { get; }

        // Constructor
        public AssetListHandle(string[] keys) {
            Keys = keys;
        }

        // LoadAsync
        public Task<IReadOnlyList<T>> LoadAsync(CancellationToken cancellationToken) {
            Assert_IsNotValid();
            Handle = AddressableHandleHelper.LoadAssetListAsync<T>( Keys );
            return GetResultAsync( cancellationToken );
        }

    }
    public class DynamicAssetHandle<T> : AddressableAssetHandle<T> where T : notnull, UnityEngine.Object {

        private string? key;

        [AllowNull]
        public string Key {
            get {
                Assert_IsValid();
                return key!;
            }
            protected set {
                key = value;
            }
        }

        // Constructor
        public DynamicAssetHandle() {
        }

        // LoadAsync
        public Task<T> LoadAsync(string key, CancellationToken cancellationToken) {
            Assert_IsNotValid();
            Handle = AddressableHandleHelper.LoadAssetAsync<T>( Key = key );
            return Handle.GetResultAsync( cancellationToken );
        }

    }
    public class DynamicAssetListHandle<T> : AddressableAssetHandle<IReadOnlyList<T>> where T : notnull, UnityEngine.Object {

        private string[]? keys;

        [AllowNull]
        public string[] Keys {
            get {
                Assert_IsValid();
                return keys!;
            }
            protected set {
                keys = value;
            }
        }

        // Constructor
        public DynamicAssetListHandle() {
        }

        // LoadAsync
        public Task<IReadOnlyList<T>> LoadAsync(string[] keys, CancellationToken cancellationToken) {
            Assert_IsNotValid();
            Handle = AddressableHandleHelper.LoadAssetListAsync<T>( Keys = keys );
            return Handle.GetResultAsync( cancellationToken );
        }

    }
}