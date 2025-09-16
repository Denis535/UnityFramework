#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract partial class NodeBase2 : NodeBase {

        // OnAttach
        protected override void OnBeforeAttach(object? argument) {
            foreach (var ancestor in this.Ancestors.OfType<NodeBase2>().Reverse()) {
                ancestor.OnBeforeDescendantAttach( this, argument );
            }
            base.OnBeforeAttach( argument );
        }
        protected override void OnAfterAttach(object? argument) {
            base.OnAfterAttach( argument );
            foreach (var ancestor in this.Ancestors.OfType<NodeBase2>()) {
                ancestor.OnAfterDescendantAttach( this, argument );
            }
        }

        // OnDetach
        protected override void OnBeforeDetach(object? argument) {
            foreach (var ancestor in this.Ancestors.OfType<NodeBase2>().Reverse()) {
                ancestor.OnBeforeDescendantDetach( this, argument );
            }
            base.OnBeforeDetach( argument );
        }
        protected override void OnAfterDetach(object? argument) {
            base.OnAfterDetach( argument );
            foreach (var ancestor in this.Ancestors.OfType<NodeBase2>()) {
                ancestor.OnAfterDescendantDetach( this, argument );
            }
        }

        // OnDescendantAttach
        protected abstract void OnBeforeDescendantAttach(NodeBase descendant, object? argument);
        protected abstract void OnAfterDescendantAttach(NodeBase descendant, object? argument);
        protected abstract void OnBeforeDescendantDetach(NodeBase descendant, object? argument);
        protected abstract void OnAfterDescendantDetach(NodeBase descendant, object? argument);

    }
    public abstract partial class NodeBase2 {

        // OnActivate
        protected override void OnBeforeActivate(object? argument) {
            foreach (var ancestor in this.Ancestors.OfType<NodeBase2>().Reverse()) {
                ancestor.OnBeforeDescendantActivate( this, argument );
            }
            base.OnBeforeActivate( argument );
        }
        protected override void OnAfterActivate(object? argument) {
            base.OnAfterActivate( argument );
            foreach (var ancestor in this.Ancestors.OfType<NodeBase2>()) {
                ancestor.OnAfterDescendantActivate( this, argument );
            }
        }

        // OnDeactivate
        protected override void OnBeforeDeactivate(object? argument) {
            foreach (var ancestor in this.Ancestors.OfType<NodeBase2>().Reverse()) {
                ancestor.OnBeforeDescendantDeactivate( this, argument );
            }
            base.OnBeforeDeactivate( argument );
        }
        protected override void OnAfterDeactivate(object? argument) {
            base.OnAfterDeactivate( argument );
            foreach (var ancestor in this.Ancestors.OfType<NodeBase2>()) {
                ancestor.OnAfterDescendantDeactivate( this, argument );
            }
        }

        // OnDescendantActivate
        protected abstract void OnBeforeDescendantActivate(NodeBase descendant, object? argument);
        protected abstract void OnAfterDescendantActivate(NodeBase descendant, object? argument);
        protected abstract void OnBeforeDescendantDeactivate(NodeBase descendant, object? argument);
        protected abstract void OnAfterDescendantDeactivate(NodeBase descendant, object? argument);

    }
}
