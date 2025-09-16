#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class TreeMachineBase {

        // Constructor
        internal TreeMachineBase() {
        }

    }
    public abstract class TreeMachineBase<TRoot> : TreeMachineBase where TRoot : notnull, NodeBase {

        // Root
        protected TRoot? Root { get; private set; }

        // Constructor
        public TreeMachineBase() {
        }

        // SetRoot
        protected virtual void SetRoot(TRoot? root, object? argument, Action<TRoot, object?>? callback) {
            if (this.Root != null) {
                this.RemoveRoot( this.Root, argument, callback );
            }
            if (root != null) {
                this.AddRoot( root, argument );
            }
        }

        // AddRoot
        private void AddRoot(TRoot root, object? argument) {
            Assert.Argument.Message( $"Argument 'root' must be non-null" ).NotNull( root != null );
            Assert.Argument.Message( $"Argument 'root' ({root}) must have no {root.Machine_NoRecursive} machine" ).Valid( root.Machine_NoRecursive == null );
            Assert.Argument.Message( $"Argument 'root' ({root}) must have no {root.Parent} parent" ).Valid( root.Parent == null );
            Assert.Argument.Message( $"Argument 'root' ({root}) must be inactive" ).Valid( root.Activity == Activity.Inactive );
            Assert.Operation.Message( $"TreeMachine {this} must have no {this.Root} root" ).Valid( this.Root == null );
            this.Root = root;
            this.Root.Attach( this, argument );
        }

        // RemoveRoot
        private void RemoveRoot(TRoot root, object? argument, Action<TRoot, object?>? callback) {
            Assert.Argument.Message( $"Argument 'root' must be non-null" ).NotNull( root != null );
            Assert.Argument.Message( $"Argument 'root' ({root}) must have {this} machine" ).Valid( root.Machine_NoRecursive == this );
            Assert.Argument.Message( $"Argument 'root' ({root}) must have no {root.Parent} parent" ).Valid( root.Parent == null );
            Assert.Argument.Message( $"Argument 'root' ({root}) must be active" ).Valid( root.Activity == Activity.Active );
            Assert.Operation.Message( $"TreeMachine {this} must have {root} root" ).Valid( this.Root == root );
            this.Root.Detach( this, argument );
            this.Root = null;
            callback?.Invoke( root, argument );
        }

    }
}
