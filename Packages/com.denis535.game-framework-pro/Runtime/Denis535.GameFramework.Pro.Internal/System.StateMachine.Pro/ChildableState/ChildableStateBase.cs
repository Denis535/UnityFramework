#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;

    public abstract partial class ChildableStateBase : IState {

        // Owner
        private object? Owner { get; set; }

        // Machine
        StateMachineBase? IState.Machine => this.Machine;
        StateMachineBase? IState.Machine_NoRecursive => this.Machine_NoRecursive;
        public StateMachineBase? Machine => (this.Owner as StateMachineBase) ?? (this.Owner as IState)?.Machine;
        internal StateMachineBase? Machine_NoRecursive => this.Owner as StateMachineBase;

        // Root
        bool IState.IsRoot => this.IsRoot;
        IState IState.Root => this.Root;
        [MemberNotNullWhen( false, nameof( Parent ) )] public bool IsRoot => this.Parent == null;
        public IState Root => this.Parent?.Root ?? this;

        // Parent
        IState? IState.Parent => this.Parent;
        IEnumerable<IState> IState.Ancestors => this.Ancestors;
        IEnumerable<IState> IState.AncestorsAndSelf => this.AncestorsAndSelf;
        public IState? Parent => this.Owner as IState;
        public IEnumerable<IState> Ancestors {
            get {
                if (this.Parent != null) {
                    yield return this.Parent;
                    foreach (var i in this.Parent.Ancestors) yield return i;
                }
            }
        }
        public IEnumerable<IState> AncestorsAndSelf => this.Ancestors.Prepend( this );

        // Activity
        Activity IState.Activity => this.Activity;
        public Activity Activity { get; private set; } = Activity.Inactive;

        // Children
        IEnumerable<IState> IState.Children {
            get {
                if (this.Child != null) {
                    return Enumerable.Empty<IState>().Append( this.Child );
                } else {
                    return Enumerable.Empty<IState>();
                }
            }
        }
        IEnumerable<IState> IState.Descendants => this.Descendants;
        IEnumerable<IState> IState.DescendantsAndSelf => this.DescendantsAndSelf;
        public IState? Child { get; private set; }
        public IEnumerable<IState> Descendants {
            get {
                if (this.Child != null) {
                    yield return this.Child;
                    foreach (var i in this.Child.Descendants) yield return i;
                }
            }
        }
        public IEnumerable<IState> DescendantsAndSelf => this.Descendants.Prepend( this );

        // Constructor
        public ChildableStateBase() {
        }

    }
    public abstract partial class ChildableStateBase {

        // Attach
        void IState.Attach(StateMachineBase machine, object? argument) {
            this.Attach( machine, argument );
        }
        void IState.Attach(IState parent, object? argument) {
            this.Attach( parent, argument );
        }
        internal void Attach(StateMachineBase machine, object? argument) {
            Assert.Argument.Message( $"Argument 'machine' must be non-null" ).NotNull( machine != null );
            Assert.Operation.Message( $"State {this} must have no {this.Machine_NoRecursive} machine" ).Valid( this.Machine_NoRecursive == null );
            Assert.Operation.Message( $"State {this} must have no {this.Parent} parent" ).Valid( this.Parent == null );
            Assert.Operation.Message( $"State {this} must be inactive" ).Valid( this.Activity == Activity.Inactive );
            {
                this.Owner = machine;
                this.OnBeforeAttach( argument );
                this.OnAttach( argument );
                this.OnAfterAttach( argument );
            }
            {
                this.Activate( argument );
            }
        }
        private void Attach(IState parent, object? argument) {
            Assert.Argument.Message( $"Argument 'parent' must be non-null" ).NotNull( parent != null );
            Assert.Operation.Message( $"State {this} must have no {this.Machine_NoRecursive} machine" ).Valid( this.Machine_NoRecursive == null );
            Assert.Operation.Message( $"State {this} must have no {this.Parent} parent" ).Valid( this.Parent == null );
            Assert.Operation.Message( $"State {this} must be inactive" ).Valid( this.Activity == Activity.Inactive );
            {
                this.Owner = parent;
                this.OnBeforeAttach( argument );
                this.OnAttach( argument );
                this.OnAfterAttach( argument );
            }
            if (parent.Activity == Activity.Active) {
                this.Activate( argument );
            } else {
            }
        }

        // Detach
        void IState.Detach(StateMachineBase machine, object? argument) {
            this.Detach( machine, argument );
        }
        void IState.Detach(IState parent, object? argument) {
            this.Detach( parent, argument );
        }
        internal void Detach(StateMachineBase machine, object? argument) {
            Assert.Argument.Message( $"Argument 'machine' must be non-null" ).NotNull( machine != null );
            Assert.Operation.Message( $"State {this} must have {machine} machine" ).Valid( this.Machine_NoRecursive == machine );
            Assert.Operation.Message( $"State {this} must be active" ).Valid( this.Activity == Activity.Active );
            {
                this.Deactivate( argument );
            }
            {
                this.OnBeforeDetach( argument );
                this.OnDetach( argument );
                this.OnAfterDetach( argument );
                this.Owner = null;
            }
        }
        private void Detach(IState parent, object? argument) {
            Assert.Argument.Message( $"Argument 'parent' must be non-null" ).NotNull( parent != null );
            Assert.Operation.Message( $"State {this} must have {parent} parent" ).Valid( this.Parent == parent );
            if (parent.Activity == Activity.Active) {
                Assert.Operation.Message( $"State {this} must be active" ).Valid( this.Activity == Activity.Active );
                this.Deactivate( argument );
            } else {
                Assert.Operation.Message( $"State {this} must be inactive" ).Valid( this.Activity == Activity.Inactive );
            }
            {
                this.OnBeforeDetach( argument );
                this.OnDetach( argument );
                this.OnAfterDetach( argument );
                this.Owner = null;
            }
        }

        // OnAttach
        protected abstract void OnAttach(object? argument);
        protected virtual void OnBeforeAttach(object? argument) {
        }
        protected virtual void OnAfterAttach(object? argument) {
        }

        // OnDetach
        protected abstract void OnDetach(object? argument);
        protected virtual void OnBeforeDetach(object? argument) {
        }
        protected virtual void OnAfterDetach(object? argument) {
        }

    }
    public abstract partial class ChildableStateBase {

        // Activate
        void IState.Activate(object? argument) {
            this.Activate( argument );
        }
        private void Activate(object? argument) {
            Assert.Operation.Message( $"State {this} must have owner" ).Valid( this.Machine_NoRecursive != null || this.Parent != null );
            Assert.Operation.Message( $"State {this} must have owner with valid activity" ).Valid( this.Machine_NoRecursive != null || this.Parent!.Activity is Activity.Active or Activity.Activating );
            Assert.Operation.Message( $"State {this} must be inactive" ).Valid( this.Activity == Activity.Inactive );
            {
                this.OnBeforeActivate( argument );
                this.Activity = Activity.Activating;
                this.OnActivate( argument );
                if (this.Child != null) {
                    this.Child.Activate( argument );
                }
                this.Activity = Activity.Active;
                this.OnAfterActivate( argument );
            }
        }

        // Deactivate
        void IState.Deactivate(object? argument) {
            this.Deactivate( argument );
        }
        private void Deactivate(object? argument) {
            Assert.Operation.Message( $"State {this} must have owner" ).Valid( this.Machine_NoRecursive != null || this.Parent != null );
            Assert.Operation.Message( $"State {this} must have owner with valid activity" ).Valid( this.Machine_NoRecursive != null || this.Parent!.Activity is Activity.Active or Activity.Deactivating );
            Assert.Operation.Message( $"State {this} must be active" ).Valid( this.Activity == Activity.Active );
            {
                this.OnBeforeDeactivate( argument );
                this.Activity = Activity.Deactivating;
                if (this.Child != null) {
                    this.Child.Deactivate( argument );
                }
                this.OnDeactivate( argument );
                this.Activity = Activity.Inactive;
                this.OnAfterDeactivate( argument );
            }
        }

        // OnActivate
        protected abstract void OnActivate(object? argument);
        protected virtual void OnBeforeActivate(object? argument) {
        }
        protected virtual void OnAfterActivate(object? argument) {
        }

        // OnDeactivate
        protected abstract void OnDeactivate(object? argument);
        protected virtual void OnBeforeDeactivate(object? argument) {
        }
        protected virtual void OnAfterDeactivate(object? argument) {
        }

    }
    public abstract partial class ChildableStateBase {

        // SetChild
        protected virtual void SetChild(IState? child, object? argument, Action<IState, object?>? callback) {
            if (this.Child != null) {
                this.RemoveChild( this.Child, argument, callback );
            }
            if (child != null) {
                this.AddChild( child, argument );
            }
        }

        // AddChild
        private void AddChild(IState child, object? argument) {
            Assert.Argument.Message( $"Argument 'child' must be non-null" ).NotNull( child != null );
            Assert.Argument.Message( $"Argument 'child' ({child}) must have no {child.Machine_NoRecursive} machine" ).Valid( child.Machine_NoRecursive == null );
            Assert.Argument.Message( $"Argument 'child' ({child}) must have no {child.Parent} parent" ).Valid( child.Parent == null );
            Assert.Argument.Message( $"Argument 'child' ({child}) must be inactive" ).Valid( child.Activity == Activity.Inactive );
            Assert.Operation.Message( $"State {this} must have no {this.Child} child" ).Valid( this.Child == null );
            this.Child = child;
            this.Child.Attach( this, argument );
        }

        // RemoveChild
        private void RemoveChild(IState child, object? argument, Action<IState, object?>? callback) {
            Assert.Argument.Message( $"Argument 'child' must be non-null" ).NotNull( child != null );
            Assert.Argument.Message( $"Argument 'child' ({child}) must have {this} parent" ).Valid( child.Parent == this );
            if (this.Activity == Activity.Active) {
                Assert.Argument.Message( $"Argument 'child' ({child}) must be active" ).Valid( child.Activity == Activity.Active );
            } else {
                Assert.Argument.Message( $"Argument 'child' ({child}) must be inactive" ).Valid( child.Activity == Activity.Inactive );
            }
            Assert.Operation.Message( $"State {this} must have {child} child" ).Valid( this.Child == child );
            this.Child.Detach( this, argument );
            this.Child = null;
            callback?.Invoke( child, argument );
        }

    }
}
