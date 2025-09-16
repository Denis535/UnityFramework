#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ChildrenableState : ChildrenableStateBase {

        // Sort
        public Action<List<IState>>? SortDelegate { get; init; }

        // OnAttach
        public event Action<object?>? OnAttachCallback;
        public event Action<object?>? OnDetachCallback;

        // OnActivate
        public event Action<object?>? OnActivateCallback;
        public event Action<object?>? OnDeactivateCallback;

        // Constructor
        public ChildrenableState() {
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
        public new void AddChild(IState child, object? argument) {
            base.AddChild( child, argument );
        }
        public new void AddChildren(IEnumerable<IState> children, object? argument) {
            base.AddChildren( children, argument );
        }

        // RemoveChild
        public new void RemoveChild(IState child, object? argument, Action<IState, object?>? callback) {
            base.RemoveChild( child, argument, callback );
        }
        public new bool RemoveChild(Func<IState, bool> predicate, object? argument, Action<IState, object?>? callback) {
            return base.RemoveChild( predicate, argument, callback );
        }
        public new int RemoveChildren(Func<IState, bool> predicate, object? argument, Action<IState, object?>? callback) {
            return base.RemoveChildren( predicate, argument, callback );
        }
        public new int RemoveChildren(object? argument, Action<IState, object?>? callback) {
            return base.RemoveChildren( argument, callback );
        }

        // Sort
        protected override void Sort(List<IState> children) {
            this.SortDelegate?.Invoke( children );
        }

    }
    public class ChildrenableState<TUserData> : ChildrenableState {

        // UserData
        public TUserData UserData { get; }

        // Constructor
        public ChildrenableState(TUserData userData) {
            this.UserData = userData;
        }

    }
}
