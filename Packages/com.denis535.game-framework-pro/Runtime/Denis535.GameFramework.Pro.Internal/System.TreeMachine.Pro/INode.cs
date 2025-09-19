#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    public interface INode {

        // Machine
        public TreeMachineBase? Machine { get; }
        internal TreeMachineBase? Machine_NoRecursive { get; }

        // Root
        [MemberNotNullWhen( false, nameof( Parent ) )] public bool IsRoot { get; }
        public INode Root { get; }

        // Parent
        public INode? Parent { get; }
        public IEnumerable<INode> Ancestors { get; }
        public IEnumerable<INode> AncestorsAndSelf { get; }

        // Activity
        public Activity Activity { get; }

        // Children
        public IEnumerable<INode> Children { get; }
        public IEnumerable<INode> Descendants { get; }
        public IEnumerable<INode> DescendantsAndSelf { get; }

        // Attach
        internal void Attach(TreeMachineBase machine, object? argument);
        internal void Attach(INode parent, object? argument);

        // Detach
        internal void Detach(TreeMachineBase machine, object? argument);
        internal void Detach(INode parent, object? argument);

        // Activate
        internal void Activate(object? argument);

        // Deactivate
        internal void Deactivate(object? argument);

    }
}
