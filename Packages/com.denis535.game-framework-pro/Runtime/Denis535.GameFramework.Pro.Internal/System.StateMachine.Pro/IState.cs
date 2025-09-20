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
        internal void Detach(IState parent, object? argument);

        // Activate
        internal void Activate(object? argument);

        // Deactivate
        internal void Deactivate(object? argument);

        // OnAttach
        internal void OnAttach(object? argument);
        internal void OnBeforeAttach(object? argument);
        internal void OnAfterAttach(object? argument);

        // OnDetach
        internal void OnDetach(object? argument);
        internal void OnBeforeDetach(object? argument);
        internal void OnAfterDetach(object? argument);

        // OnActivate
        internal void OnActivate(object? argument);
        internal void OnBeforeActivate(object? argument);
        internal void OnAfterActivate(object? argument);

        // OnDeactivate
        internal void OnDeactivate(object? argument);
        internal void OnBeforeDeactivate(object? argument);
        internal void OnAfterDeactivate(object? argument);

    }
}
