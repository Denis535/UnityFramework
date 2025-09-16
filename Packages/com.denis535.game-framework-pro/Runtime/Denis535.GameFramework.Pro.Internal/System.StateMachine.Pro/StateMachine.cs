#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class StateMachine<TRoot> : StateMachineBase<TRoot> where TRoot : class, IState {

        // Root
        public new TRoot? Root => base.Root;

        // Constructor
        public StateMachine() {
        }

        // SetRoot
        public new void SetRoot(TRoot? root, object? argument, Action<TRoot, object?>? callback) {
            base.SetRoot( root, argument, callback );
        }

    }
    public class StateMachine<TRoot, TUserData> : StateMachine<TRoot> where TRoot : class, IState {

        // UserData
        public TUserData UserData { get; }

        // Constructor
        public StateMachine(TUserData userData) {
            this.UserData = userData;
        }

    }
}
