#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class TreeMachine : TreeMachineBase {

        // Root
        public new INode? Root => base.Root;

        // Constructor
        public TreeMachine() {
        }

        // SetRoot
        public new void SetRoot(INode? root, object? argument, Action<INode, object?>? callback) {
            base.SetRoot( root, argument, callback );
        }

    }
    public class TreeMachine<TUserData> : TreeMachine, IUserData<TUserData> {

        // UserData
        public TUserData UserData { get; }

        // Constructor
        public TreeMachine(TUserData userData) {
            this.UserData = userData;
        }

    }
}
