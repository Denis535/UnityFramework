#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract partial class NodeBase2 : NodeBase, INode2 {

        // Constructor
        public NodeBase2() {
        }

    }
    public abstract partial class NodeBase2 {

        // OnDescendantAttach
        void INode2.OnBeforeDescendantAttach(INode2 descendant, object? argument) {
            this.OnBeforeDescendantAttach( descendant, argument );
        }
        void INode2.OnAfterDescendantAttach(INode2 descendant, object? argument) {
            this.OnAfterDescendantAttach( descendant, argument );
        }
        void INode2.OnBeforeDescendantDetach(INode2 descendant, object? argument) {
            this.OnBeforeDescendantDetach( descendant, argument );
        }
        void INode2.OnAfterDescendantDetach(INode2 descendant, object? argument) {
            this.OnAfterDescendantDetach( descendant, argument );
        }

        // OnDescendantActivate
        void INode2.OnBeforeDescendantActivate(INode2 descendant, object? argument) {
            this.OnBeforeDescendantActivate( descendant, argument );
        }
        void INode2.OnAfterDescendantActivate(INode2 descendant, object? argument) {
            this.OnAfterDescendantActivate( descendant, argument );
        }
        void INode2.OnBeforeDescendantDeactivate(INode2 descendant, object? argument) {
            this.OnBeforeDescendantDeactivate( descendant, argument );
        }
        void INode2.OnAfterDescendantDeactivate(INode2 descendant, object? argument) {
            this.OnAfterDescendantDeactivate( descendant, argument );
        }

    }
    public abstract partial class NodeBase2 {

        // OnAttach
        protected override void OnBeforeAttach(object? argument) {
            foreach (var ancestor in this.Ancestors.OfType<INode2>().Reverse()) {
                ancestor.OnBeforeDescendantAttach( this, argument );
            }
            base.OnBeforeAttach( argument );
        }
        protected override void OnAfterAttach(object? argument) {
            base.OnAfterAttach( argument );
            foreach (var ancestor in this.Ancestors.OfType<INode2>()) {
                ancestor.OnAfterDescendantAttach( this, argument );
            }
        }

        // OnDetach
        protected override void OnBeforeDetach(object? argument) {
            foreach (var ancestor in this.Ancestors.OfType<INode2>().Reverse()) {
                ancestor.OnBeforeDescendantDetach( this, argument );
            }
            base.OnBeforeDetach( argument );
        }
        protected override void OnAfterDetach(object? argument) {
            base.OnAfterDetach( argument );
            foreach (var ancestor in this.Ancestors.OfType<INode2>()) {
                ancestor.OnAfterDescendantDetach( this, argument );
            }
        }

        // OnDescendantAttach
        protected abstract void OnBeforeDescendantAttach(INode2 descendant, object? argument);
        protected abstract void OnAfterDescendantAttach(INode2 descendant, object? argument);
        protected abstract void OnBeforeDescendantDetach(INode2 descendant, object? argument);
        protected abstract void OnAfterDescendantDetach(INode2 descendant, object? argument);

    }
    public abstract partial class NodeBase2 {

        // OnActivate
        protected override void OnBeforeActivate(object? argument) {
            foreach (var ancestor in this.Ancestors.OfType<INode2>().Reverse()) {
                ancestor.OnBeforeDescendantActivate( this, argument );
            }
            base.OnBeforeActivate( argument );
        }
        protected override void OnAfterActivate(object? argument) {
            base.OnAfterActivate( argument );
            foreach (var ancestor in this.Ancestors.OfType<INode2>()) {
                ancestor.OnAfterDescendantActivate( this, argument );
            }
        }

        // OnDeactivate
        protected override void OnBeforeDeactivate(object? argument) {
            foreach (var ancestor in this.Ancestors.OfType<INode2>().Reverse()) {
                ancestor.OnBeforeDescendantDeactivate( this, argument );
            }
            base.OnBeforeDeactivate( argument );
        }
        protected override void OnAfterDeactivate(object? argument) {
            base.OnAfterDeactivate( argument );
            foreach (var ancestor in this.Ancestors.OfType<INode2>()) {
                ancestor.OnAfterDescendantDeactivate( this, argument );
            }
        }

        // OnDescendantActivate
        protected abstract void OnBeforeDescendantActivate(INode2 descendant, object? argument);
        protected abstract void OnAfterDescendantActivate(INode2 descendant, object? argument);
        protected abstract void OnBeforeDescendantDeactivate(INode2 descendant, object? argument);
        protected abstract void OnAfterDescendantDeactivate(INode2 descendant, object? argument);

    }
}
