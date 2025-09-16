#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class TreeMachine<TRoot> : TreeMachineBase<TRoot> where TRoot : notnull, NodeBase {

        // Root
        public new TRoot? Root => base.Root;

        // Constructor
        public TreeMachine() {
        }

        // SetRoot
        public new void SetRoot(TRoot? root, object? argument, Action<TRoot, object?>? callback) {
            base.SetRoot( root, argument, callback );
        }

    }
    public class TreeMachine<TRoot, TUserData> : TreeMachine<TRoot> where TRoot : notnull, NodeBase {

        // UserData
        public TUserData UserData { get; }

        // Constructor
        public TreeMachine(TUserData userData) {
            this.UserData = userData;
        }

    }
}
