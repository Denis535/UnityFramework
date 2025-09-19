#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class StateMachineBase {

        // Root
        protected IState? Root { get; private set; }

        // Constructor
        public StateMachineBase() {
        }

        // SetRoot
        protected virtual void SetRoot(IState? root, object? argument, Action<IState, object?>? callback) {
            if (this.Root != null) {
                this.RemoveRoot( this.Root, argument, callback );
            }
            if (root != null) {
                this.AddRoot( root, argument );
            }
        }

        // AddRoot
        private void AddRoot(IState root, object? argument) {
            Assert.Argument.Message( $"Argument 'root' must be non-null" ).NotNull( root != null );
            Assert.Argument.Message( $"Argument 'root' ({root}) must have no {root.Machine} machine" ).Valid( root.Machine == null );
            Assert.Argument.Message( $"Argument 'root' ({root}) must be inactive" ).Valid( root.Activity == Activity.Inactive );
            Assert.Operation.Message( $"StateMachine {this} must have no {this.Root} root" ).Valid( this.Root == null );
            this.Root = root;
            this.Root.Attach( this, argument );
        }

        // RemoveRoot
        private void RemoveRoot(IState root, object? argument, Action<IState, object?>? callback) {
            Assert.Argument.Message( $"Argument 'root' must be non-null" ).NotNull( root != null );
            Assert.Argument.Message( $"Argument 'root' ({root}) must have {this} machine" ).Valid( root.Machine == this );
            Assert.Argument.Message( $"Argument 'root' ({root}) must be active" ).Valid( root.Activity == Activity.Active );
            Assert.Operation.Message( $"StateMachine {this} must have {root} root" ).Valid( this.Root == root );
            this.Root.Detach( this, argument );
            this.Root = null;
            callback?.Invoke( root, argument );
        }

    }
}
