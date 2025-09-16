#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Node2 : NodeBase2 {

        // Sort
        public Action<List<NodeBase>>? SortDelegate { get; init; }

        // OnAttach
        public event Action<object?>? OnAttachCallback;
        public event Action<object?>? OnDetachCallback;

        // OnActivate
        public event Action<object?>? OnActivateCallback;
        public event Action<object?>? OnDeactivateCallback;

        // OnDescendantAttach
        public event Action<NodeBase, object?>? OnBeforeDescendantAttachCallback;
        public event Action<NodeBase, object?>? OnAfterDescendantAttachCallback;
        public event Action<NodeBase, object?>? OnBeforeDescendantDetachCallback;
        public event Action<NodeBase, object?>? OnAfterDescendantDetachCallback;

        // OnDescendantActivate
        public event Action<NodeBase, object?>? OnBeforeDescendantActivateCallback;
        public event Action<NodeBase, object?>? OnAfterDescendantActivateCallback;
        public event Action<NodeBase, object?>? OnBeforeDescendantDeactivateCallback;
        public event Action<NodeBase, object?>? OnAfterDescendantDeactivateCallback;

        // Constructor
        public Node2() {
        }

        // OnAttach
        protected override void OnAttach(object? argument) {
            this.OnAttachCallback?.Invoke( argument );
        }
        protected override void OnDetach(object? argument) {
            this.OnDetachCallback?.Invoke( argument );
        }

        // OnActivate
        protected override void OnActivate(object? argument) {
            this.OnActivateCallback?.Invoke( argument );
        }
        protected override void OnDeactivate(object? argument) {
            this.OnDeactivateCallback?.Invoke( argument );
        }

        // OnDescendantAttach
        protected override void OnBeforeDescendantAttach(NodeBase descendant, object? argument) {
            this.OnBeforeDescendantAttachCallback?.Invoke( descendant, argument );
        }
        protected override void OnAfterDescendantAttach(NodeBase descendant, object? argument) {
            this.OnAfterDescendantAttachCallback?.Invoke( descendant, argument );
        }
        protected override void OnBeforeDescendantDetach(NodeBase descendant, object? argument) {
            this.OnBeforeDescendantDetachCallback?.Invoke( descendant, argument );
        }
        protected override void OnAfterDescendantDetach(NodeBase descendant, object? argument) {
            this.OnAfterDescendantDetachCallback?.Invoke( descendant, argument );
        }

        // OnDescendantActivate
        protected override void OnBeforeDescendantActivate(NodeBase descendant, object? argument) {
            this.OnBeforeDescendantActivateCallback?.Invoke( descendant, argument );
        }
        protected override void OnAfterDescendantActivate(NodeBase descendant, object? argument) {
            this.OnAfterDescendantActivateCallback?.Invoke( descendant, argument );
        }
        protected override void OnBeforeDescendantDeactivate(NodeBase descendant, object? argument) {
            this.OnBeforeDescendantDeactivateCallback?.Invoke( descendant, argument );
        }
        protected override void OnAfterDescendantDeactivate(NodeBase descendant, object? argument) {
            this.OnAfterDescendantDeactivateCallback?.Invoke( descendant, argument );
        }

        // AddChild
        public new void AddChild(NodeBase child, object? argument) {
            base.AddChild( child, argument );
        }
        public new void AddChildren(IEnumerable<NodeBase> children, object? argument) {
            base.AddChildren( children, argument );
        }

        // RemoveChild
        public new void RemoveChild(NodeBase child, object? argument, Action<NodeBase, object?>? callback) {
            base.RemoveChild( child, argument, callback );
        }
        public new bool RemoveChild(Func<NodeBase, bool> predicate, object? argument, Action<NodeBase, object?>? callback) {
            return base.RemoveChild( predicate, argument, callback );
        }
        public new int RemoveChildren(Func<NodeBase, bool> predicate, object? argument, Action<NodeBase, object?>? callback) {
            return base.RemoveChildren( predicate, argument, callback );
        }
        public new int RemoveChildren(object? argument, Action<NodeBase, object?>? callback) {
            return base.RemoveChildren( argument, callback );
        }

        // RemoveSelf
        public new void RemoveSelf(object? argument, Action<NodeBase, object?>? callback) {
            base.RemoveSelf( argument, callback );
        }

        // Sort
        protected override void Sort(List<NodeBase> children) {
            this.SortDelegate?.Invoke( children );
        }

    }
    public class Node2<TUserData> : Node2 {

        // UserData
        public TUserData UserData { get; }

        // Constructor
        public Node2(TUserData userData) {
            this.UserData = userData;
        }

    }
}
