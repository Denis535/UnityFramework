#nullable enable
namespace UnityEngine.AddressableAssets {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public abstract class AddressableListHandle {

        // Keys
        public string[] Keys { get; }
        // Handle
        public abstract bool IsValid { get; }
        public abstract bool IsDone { get; }
        public abstract bool IsSucceeded { get; }
        public abstract bool IsFailed { get; }

        // Constructor
        public AddressableListHandle(string[] keys) {
            this.Keys = keys;
        }

        // Heleprs
        protected void Assert_IsValid() {
            if (this.IsValid) return;
            throw new InvalidOperationException( $"AddressableListHandle `{this}` must be valid" );
        }
        protected void Assert_IsNotValid() {
            if (!this.IsValid) return;
            throw new InvalidOperationException( $"AddressableListHandle `{this}` is already valid" );
        }

    }
    public abstract class AddressableListHandle<T> : AddressableListHandle where T : notnull {

        // Handle
        protected AsyncOperationHandle<IReadOnlyList<T>> Handle { get; set; }
        public override bool IsValid => this.Handle.IsValid();
        public override bool IsDone => this.Handle.IsValid() && this.Handle.IsDone;
        public override bool IsSucceeded => this.Handle.IsValid() && this.Handle.IsSucceeded();
        public override bool IsFailed => this.Handle.IsValid() && this.Handle.IsFailed();

        // Constructor
        public AddressableListHandle(string[] keys) : base( keys ) {
        }

    }
}
