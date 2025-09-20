#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface INode2 : INode {

        // OnDescendantAttach
        internal void OnBeforeDescendantAttach(INode2 descendant, object? argument);
        internal void OnAfterDescendantAttach(INode2 descendant, object? argument);
        internal void OnBeforeDescendantDetach(INode2 descendant, object? argument);
        internal void OnAfterDescendantDetach(INode2 descendant, object? argument);

        // OnDescendantActivate
        internal void OnBeforeDescendantActivate(INode2 descendant, object? argument);
        internal void OnAfterDescendantActivate(INode2 descendant, object? argument);
        internal void OnBeforeDescendantDeactivate(INode2 descendant, object? argument);
        internal void OnAfterDescendantDeactivate(INode2 descendant, object? argument);

    }
}
