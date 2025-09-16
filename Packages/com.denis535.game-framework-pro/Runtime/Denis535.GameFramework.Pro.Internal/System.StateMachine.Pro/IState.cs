#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    public interface IState {

        // Machine
        public StateMachineBase? Machine { get; }
        internal StateMachineBase? Machine_NoRecursive { get; }

        // Root
        [MemberNotNullWhen( false, nameof( Parent ) )] public bool IsRoot { get; }
        public IState Root { get; }

        // Parent
        public IState? Parent { get; }
        public IEnumerable<IState> Ancestors { get; }
        public IEnumerable<IState> AncestorsAndSelf { get; }

        // Activity
        public Activity Activity { get; }

        // Children
        public IEnumerable<IState> Children { get; }
        public IEnumerable<IState> Descendants { get; }
        public IEnumerable<IState> DescendantsAndSelf { get; }

        // Attach
        internal void Attach(StateMachineBase machine, object? argument);
        internal void Attach(IState parent, object? argument);

        // Detach
        internal void Detach(StateMachineBase machine, object? argument);
        internal void Detach(IState state, object? argument);

        // Activate
        internal void Activate(object? argument);

        // Deactivate
        internal void Deactivate(object? argument);

    }
}
