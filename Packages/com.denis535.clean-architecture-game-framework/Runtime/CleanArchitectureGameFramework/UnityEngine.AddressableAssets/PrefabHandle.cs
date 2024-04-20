#nullable enable
namespace UnityEngine.AddressableAssets {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public class PrefabHandle<T> : AddressableHandle where T : notnull, Component {

        // Key
        public new string Key => base.Key!;
        // Handle
        protected AsyncOperationHandle<T> Handle { get; set; }
        public override bool IsValid => Handle.IsValid();
        public override bool IsDone => Handle.IsDone;
        public override bool IsSucceeded => Handle.Status == AsyncOperationStatus.Succeeded;
        public override bool IsFailed => Handle.Status == AsyncOperationStatus.Failed;
        public override Exception? Exception => Handle.OperationException;
        public T Value {
            get {
                Assert_IsValid();
                Assert_IsSucceeded();
                return Handle.Result;
            }
        }
        public T? ValueSafe {
            get {
                return Handle.IsValid() && Handle.IsSucceeded() ? Handle.Result : default;
            }
        }

        // Constructor
        public PrefabHandle(string key) : base( key ) {
        }
        public PrefabHandle(string key, AsyncOperationHandle<T> handle) : base( key ) {
            Handle = handle;
        }

        // LoadAsync
        public ValueTask<T> LoadAsync(CancellationToken cancellationToken) {
            Assert_IsNotValid();
            Handle = AddressableHelper.LoadPrefabAsync<T>( Key );
            return Handle.GetResultAsync( cancellationToken );
        }

        // GetValueAsync
        public ValueTask<T> GetValueAsync(CancellationToken cancellationToken) {
            Assert_IsValid();
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
    public class PrefabListHandle<T> : AddressableListHandle where T : notnull, Component {

        // Keys
        public new string[] Keys => base.Keys!;
        // Handle
        protected AsyncOperationHandle<IReadOnlyList<T>> Handle { get; set; }
        public override bool IsValid => Handle.IsValid();
        public override bool IsDone => Handle.IsDone;
        public override bool IsSucceeded => Handle.Status == AsyncOperationStatus.Succeeded;
        public override bool IsFailed => Handle.Status == AsyncOperationStatus.Failed;
        public override Exception? Exception => Handle.OperationException;
        public IReadOnlyList<T> Values {
            get {
                Assert_IsValid();
                Assert_IsSucceeded();
                return Handle.Result;
            }
        }
        public IReadOnlyList<T>? ValuesSafe {
            get {
                return Handle.IsValid() && Handle.IsSucceeded() ? Handle.Result : default;
            }
        }

        // Constructor
        public PrefabListHandle(string[] keys) : base( keys ) {
        }
        public PrefabListHandle(string[] keys, AsyncOperationHandle<IReadOnlyList<T>> handle) : base( keys ) {
            Handle = handle;
        }

        // LoadAsync
        public ValueTask<IReadOnlyList<T>> LoadAsync(CancellationToken cancellationToken) {
            Assert_IsNotValid();
            Handle = AddressableHelper.LoadPrefabListAsync<T>( Keys );
            return Handle.GetResultAsync( cancellationToken );
        }

        // GetValuesAsync
        public ValueTask<IReadOnlyList<T>> GetValuesAsync(CancellationToken cancellationToken) {
            Assert_IsValid();
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
    public class DynamicPrefabHandle<T> : AddressableHandle where T : notnull, Component {

        // Handle
        protected AsyncOperationHandle<T> Handle { get; set; }
        public override bool IsValid => Handle.IsValid();
        public override bool IsDone => Handle.IsDone;
        public override bool IsSucceeded => Handle.Status == AsyncOperationStatus.Succeeded;
        public override bool IsFailed => Handle.Status == AsyncOperationStatus.Failed;
        public override Exception? Exception => Handle.OperationException;
        public T Value {
            get {
                Assert_IsValid();
                Assert_IsSucceeded();
                return Handle.Result;
            }
        }
        public T? ValueSafe {
            get {
                return Handle.IsValid() && Handle.IsSucceeded() ? Handle.Result : default;
            }
        }

        // Constructor
        public DynamicPrefabHandle() : base( null ) {
        }
        public DynamicPrefabHandle(string key, AsyncOperationHandle<T> handle) : base( key ) {
            Handle = handle;
        }

        // LoadAsync
        public ValueTask<T> LoadAsync(string key, CancellationToken cancellationToken) {
            Assert_IsNotValid();
            Handle = AddressableHelper.LoadPrefabAsync<T>( Key = key );
            return Handle.GetResultAsync( cancellationToken );
        }

        // GetValueAsync
        public ValueTask<T> GetValueAsync(CancellationToken cancellationToken) {
            Assert_IsValid();
            return Handle.GetResultAsync( cancellationToken );
        }

        // Release
        public void Release() {
            Assert_IsValid();
            Addressables.Release( Handle );
            Key = null;
            Handle = default;
        }
        public void ReleaseSafe() {
            if (Handle.IsValid()) {
                Release();
            }
        }

    }
    public class DynamicPrefabListHandle<T> : AddressableListHandle where T : notnull, Component {

        // Handle
        protected AsyncOperationHandle<IReadOnlyList<T>> Handle { get; set; }
        public override bool IsValid => Handle.IsValid();
        public override bool IsDone => Handle.IsDone;
        public override bool IsSucceeded => Handle.Status == AsyncOperationStatus.Succeeded;
        public override bool IsFailed => Handle.Status == AsyncOperationStatus.Failed;
        public override Exception? Exception => Handle.OperationException;
        public IReadOnlyList<T> Values {
            get {
                Assert_IsValid();
                Assert_IsSucceeded();
                return Handle.Result;
            }
        }
        public IReadOnlyList<T>? ValuesSafe {
            get {
                return Handle.IsValid() && Handle.IsSucceeded() ? Handle.Result : default;
            }
        }

        // Constructor
        public DynamicPrefabListHandle() : base( null ) {
        }
        public DynamicPrefabListHandle(string[] keys, AsyncOperationHandle<IReadOnlyList<T>> handle) : base( keys ) {
            Handle = handle;
        }

        // LoadAsync
        public ValueTask<IReadOnlyList<T>> LoadAsync(string[] keys, CancellationToken cancellationToken) {
            Assert_IsNotValid();
            Handle = AddressableHelper.LoadPrefabListAsync<T>( Keys = keys );
            return Handle.GetResultAsync( cancellationToken );
        }

        // GetValuesAsync
        public ValueTask<IReadOnlyList<T>> GetValuesAsync(CancellationToken cancellationToken) {
            Assert_IsValid();
            return Handle.GetResultAsync( cancellationToken );
        }

        // Release
        public void Release() {
            Assert_IsValid();
            Addressables.Release( Handle );
            Keys = null;
            Handle = default;
        }
        public void ReleaseSafe() {
            if (Handle.IsValid()) {
                Release();
            }
        }

    }
}
