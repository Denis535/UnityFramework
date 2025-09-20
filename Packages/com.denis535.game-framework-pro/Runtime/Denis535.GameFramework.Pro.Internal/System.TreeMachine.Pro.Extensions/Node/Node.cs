#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Node : NodeBase {

        // Sort
        public Action<List<INode>>? SortDelegate { get; init; }

        // OnAttach
        public event Action<object?>? OnAttachCallback;
        public event Action<object?>? OnDetachCallback;

        // OnActivate
        public event Action<object?>? OnActivateCallback;
        public event Action<object?>? OnDeactivateCallback;

        // Constructor
        public Node() {
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

        // AddChild
        public new void AddChild(INode child, object? argument) {
            base.AddChild( child, argument );
        }
        public new void AddChildren(IEnumerable<INode> children, object? argument) {
            base.AddChildren( children, argument );
        }

        // RemoveChild
        public new void RemoveChild(INode child, object? argument, Action<INode, object?>? callback) {
            base.RemoveChild( child, argument, callback );
        }
        public new bool RemoveChild(Func<INode, bool> predicate, object? argument, Action<INode, object?>? callback) {
            return base.RemoveChild( predicate, argument, callback );
        }
        public new int RemoveChildren(Func<INode, bool> predicate, object? argument, Action<INode, object?>? callback) {
            return base.RemoveChildren( predicate, argument, callback );
        }
        public new int RemoveChildren(object? argument, Action<INode, object?>? callback) {
            return base.RemoveChildren( argument, callback );
        }

        // RemoveSelf
        public new void RemoveSelf(object? argument, Action<INode, object?>? callback) {
            base.RemoveSelf( argument, callback );
        }

        // Sort
        protected override void Sort(List<INode> children) {
            this.SortDelegate?.Invoke( children );
        }

    }
    public class Node<TUserData> : Node, IUserData<TUserData> {

        // UserData
        public TUserData UserData { get; }

        // Constructor
        public Node(TUserData userData) {
            this.UserData = userData;
        }

    }
}
