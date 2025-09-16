#nullable enable
namespace UnityEngine.AddressableAssets {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public abstract class AddressableHandle {

        // Key
        public string Key { get; }
        // Handle
        public abstract bool IsValid { get; }
        public abstract bool IsDone { get; }
        public abstract bool IsSucceeded { get; }
        public abstract bool IsFailed { get; }

        // Constructor
        public AddressableHandle(string key) {
            this.Key = key;
        }

        // Heleprs
        protected void Assert_IsValid() {
            if (this.IsValid) return;
            throw new InvalidOperationException( $"AddressableHandle `{this}` must be valid" );
        }
        protected void Assert_IsNotValid() {
            if (!this.IsValid) return;
            throw new InvalidOperationException( $"AddressableHandle `{this}` is already valid" );
        }

    }
    public abstract class AddressableHandle<T> : AddressableHandle where T : notnull {

        // Handle
        protected AsyncOperationHandle<T> Handle { get; set; }
        public override bool IsValid => this.Handle.IsValid();
        public override bool IsDone => this.Handle.IsValid() && this.Handle.IsDone;
        public override bool IsSucceeded => this.Handle.IsValid() && this.Handle.IsSucceeded();
        public override bool IsFailed => this.Handle.IsValid() && this.Handle.IsFailed();

        // Constructor
        public AddressableHandle(string key) : base( key ) {
        }

    }
}
