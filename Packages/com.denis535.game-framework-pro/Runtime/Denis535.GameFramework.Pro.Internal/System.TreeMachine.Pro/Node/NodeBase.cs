#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;

    public abstract partial class NodeBase : INode {

        // Owner
        private object? Owner { get; set; }

        // Machine
        public TreeMachineBase? Machine => (this.Owner as TreeMachineBase) ?? (this.Owner as INode)?.Machine;
        internal TreeMachineBase? Machine_NoRecursive => this.Owner as TreeMachineBase;

        // Root
        [MemberNotNullWhen( false, nameof( Parent ) )] public bool IsRoot => this.Parent == null;
        public INode Root => this.Parent?.Root ?? this;

        // Parent
        public INode? Parent => this.Owner as INode;
        public IEnumerable<INode> Ancestors {
            get {
                if (this.Parent != null) {
                    yield return this.Parent;
                    foreach (var i in this.Parent.Ancestors) yield return i;
                }
            }
        }
        public IEnumerable<INode> AncestorsAndSelf => this.Ancestors.Prepend( this );

        // Activity
        public Activity Activity { get; private set; } = Activity.Inactive;

        // Children
        public IReadOnlyList<INode> Children => this.ChildrenMutable;
        private List<INode> ChildrenMutable { get; } = new List<INode>( 0 );
        public IEnumerable<INode> Descendants {
            get {
                foreach (var child in this.Children) {
                    yield return child;
                    foreach (var i in child.Descendants) yield return i;
                }
            }
        }
        public IEnumerable<INode> DescendantsAndSelf => this.Descendants.Prepend( this );

        // Constructor
        public NodeBase() {
        }

    }
    public abstract partial class NodeBase {

        // Machine
        TreeMachineBase? INode.Machine => this.Machine;
        TreeMachineBase? INode.Machine_NoRecursive => this.Machine_NoRecursive;

        // Root
        [MemberNotNullWhen( false, nameof( INode.Parent ) )] bool INode.IsRoot => this.IsRoot;
        INode INode.Root => this.Root;

        // Parent
        INode? INode.Parent => this.Parent;
        IEnumerable<INode> INode.Ancestors => this.Ancestors;
        IEnumerable<INode> INode.AncestorsAndSelf => this.AncestorsAndSelf;

        // Activity
        Activity INode.Activity => this.Activity;

        // Children
        IEnumerable<INode> INode.Children => this.Children;
        IEnumerable<INode> INode.Descendants => this.Descendants;
        IEnumerable<INode> INode.DescendantsAndSelf => this.DescendantsAndSelf;

        // Attach
        void INode.Attach(TreeMachineBase machine, object? argument) {
            this.Attach( machine, argument );
        }
        void INode.Attach(INode parent, object? argument) {
            this.Attach( parent, argument );
        }

        // Detach
        void INode.Detach(TreeMachineBase machine, object? argument) {
            this.Detach( machine, argument );
        }
        void INode.Detach(INode parent, object? argument) {
            this.Detach( parent, argument );
        }

        // Activate
        void INode.Activate(object? argument) {
            this.Activate( argument );
        }

        // Deactivate
        void INode.Deactivate(object? argument) {
            this.Deactivate( argument );
        }

        // OnAttach
        void INode.OnAttach(object? argument) {
            this.OnAttach( argument );
        }
        void INode.OnBeforeAttach(object? argument) {
            this.OnBeforeAttach( argument );
        }
        void INode.OnAfterAttach(object? argument) {
            this.OnAfterAttach( argument );
        }

        // OnDetach
        void INode.OnDetach(object? argument) {
            this.OnDetach( argument );
        }
        void INode.OnBeforeDetach(object? argument) {
            this.OnBeforeDetach( argument );
        }
        void INode.OnAfterDetach(object? argument) {
            this.OnAfterDetach( argument );
        }

        // OnActivate
        void INode.OnActivate(object? argument) {
            this.OnActivate( argument );
        }
        void INode.OnBeforeActivate(object? argument) {
            this.OnBeforeActivate( argument );
        }
        void INode.OnAfterActivate(object? argument) {
            this.OnAfterActivate( argument );
        }

        // OnDeactivate
        void INode.OnDeactivate(object? argument) {
            this.OnDeactivate( argument );
        }
        void INode.OnBeforeDeactivate(object? argument) {
            this.OnBeforeDeactivate( argument );
        }
        void INode.OnAfterDeactivate(object? argument) {
            this.OnAfterDeactivate( argument );
        }

    }
    public abstract partial class NodeBase {

        // Attach
        internal void Attach(TreeMachineBase machine, object? argument) {
            Assert.Argument.Message( $"Argument 'machine' must be non-null" ).NotNull( machine != null );
            Assert.Operation.Message( $"Node {this} must have no {this.Machine_NoRecursive} machine" ).Valid( this.Machine_NoRecursive == null );
            Assert.Operation.Message( $"Node {this} must have no {this.Parent} parent" ).Valid( this.Parent == null );
            Assert.Operation.Message( $"Node {this} must be inactive" ).Valid( this.Activity == Activity.Inactive );
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
        private void Attach(INode parent, object? argument) {
            Assert.Argument.Message( $"Argument 'parent' must be non-null" ).NotNull( parent != null );
            Assert.Operation.Message( $"Node {this} must have no {this.Machine_NoRecursive} machine" ).Valid( this.Machine_NoRecursive == null );
            Assert.Operation.Message( $"Node {this} must have no {this.Parent} parent" ).Valid( this.Parent == null );
            Assert.Operation.Message( $"Node {this} must be inactive" ).Valid( this.Activity == Activity.Inactive );
            {
                this.Owner = parent;
                this.OnBeforeAttach( argument );
                this.OnAttach( argument );
                this.OnAfterAttach( argument );
            }
            if (parent.Activity == Activity.Active) {
                this.Activate( argument );
            }
        }

        // Detach
        internal void Detach(TreeMachineBase machine, object? argument) {
            Assert.Argument.Message( $"Argument 'machine' must be non-null" ).NotNull( machine != null );
            Assert.Operation.Message( $"Node {this} must have {machine} machine" ).Valid( this.Machine_NoRecursive == machine );
            Assert.Operation.Message( $"Node {this} must be active" ).Valid( this.Activity == Activity.Active );
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
        private void Detach(INode parent, object? argument) {
            Assert.Argument.Message( $"Argument 'parent' must be non-null" ).NotNull( parent != null );
            Assert.Operation.Message( $"Node {this} must have {parent} parent" ).Valid( this.Parent == parent );
            if (parent.Activity == Activity.Active) {
                Assert.Operation.Message( $"Node {this} must be active" ).Valid( this.Activity == Activity.Active );
                this.Deactivate( argument );
            } else {
                Assert.Operation.Message( $"Node {this} must be inactive" ).Valid( this.Activity == Activity.Inactive );
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
    public abstract partial class NodeBase {

        // Activate
        private void Activate(object? argument) {
            Assert.Operation.Message( $"Node {this} must have owner" ).Valid( this.Machine_NoRecursive != null || this.Parent != null );
            Assert.Operation.Message( $"Node {this} must have valid owner" ).Valid( this.Machine_NoRecursive != null || this.Parent!.Activity is Activity.Active or Activity.Activating );
            Assert.Operation.Message( $"Node {this} must be inactive" ).Valid( this.Activity == Activity.Inactive );
            {
                this.OnBeforeActivate( argument );
                this.Activity = Activity.Activating;
                this.OnActivate( argument );
                foreach (var child in this.Children) {
                    child.Activate( argument );
                }
                this.Activity = Activity.Active;
                this.OnAfterActivate( argument );
            }
        }

        // Deactivate
        private void Deactivate(object? argument) {
            Assert.Operation.Message( $"Node {this} must have owner" ).Valid( this.Machine_NoRecursive != null || this.Parent != null );
            Assert.Operation.Message( $"Node {this} must have valid owner" ).Valid( this.Machine_NoRecursive != null || this.Parent!.Activity is Activity.Active or Activity.Deactivating );
            Assert.Operation.Message( $"Node {this} must be active" ).Valid( this.Activity == Activity.Active );
            {
                this.OnBeforeDeactivate( argument );
                this.Activity = Activity.Deactivating;
                foreach (var child in this.Children.Reverse()) {
                    child.Deactivate( argument );
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
    public abstract partial class NodeBase {

        // AddChild
        protected virtual void AddChild(INode child, object? argument) {
            Assert.Argument.Message( $"Argument 'child' must be non-null" ).NotNull( child != null );
            Assert.Argument.Message( $"Argument 'child' ({child}) must have no {child.Machine_NoRecursive} machine" ).Valid( child.Machine_NoRecursive == null );
            Assert.Argument.Message( $"Argument 'child' ({child}) must have no {child.Parent} parent" ).Valid( child.Parent == null );
            Assert.Argument.Message( $"Argument 'child' ({child}) must be inactive" ).Valid( child.Activity == Activity.Inactive );
            Assert.Operation.Message( $"Node {this} must have no {child} child" ).Valid( !this.Children.Contains( child ) );
            this.ChildrenMutable.Add( child );
            this.Sort( this.ChildrenMutable );
            child.Attach( this, argument );
        }
        protected void AddChildren(IEnumerable<INode> children, object? argument) {
            Assert.Argument.Message( $"Argument 'children' must be non-null" ).NotNull( children != null );
            foreach (var child in children) {
                this.AddChild( child, argument );
            }
        }

        // RemoveChild
        protected virtual void RemoveChild(INode child, object? argument, Action<INode, object?>? callback) {
            Assert.Argument.Message( $"Argument 'child' must be non-null" ).NotNull( child != null );
            Assert.Argument.Message( $"Argument 'child' ({child}) must have {this} parent" ).Valid( child.Parent == this );
            if (this.Activity == Activity.Active) {
                Assert.Argument.Message( $"Argument 'child' ({child}) must be active" ).Valid( child.Activity == Activity.Active );
            } else {
                Assert.Argument.Message( $"Argument 'child' ({child}) must be inactive" ).Valid( child.Activity == Activity.Inactive );
            }
            Assert.Operation.Message( $"Node {this} must have {child} child" ).Valid( this.Children.Contains( child ) );
            child.Detach( this, argument );
            _ = this.ChildrenMutable.Remove( child );
            callback?.Invoke( child, argument );
        }
        protected bool RemoveChild(Func<INode, bool> predicate, object? argument, Action<INode, object?>? callback) {
            var child = this.Children.LastOrDefault( predicate );
            if (child != null) {
                this.RemoveChild( child, argument, callback );
                return true;
            }
            return false;
        }
        protected int RemoveChildren(Func<INode, bool> predicate, object? argument, Action<INode, object?>? callback) {
            var children = this.Children.Reverse().Where( predicate ).ToList();
            foreach (var child in children) {
                this.RemoveChild( child, argument, callback );
            }
            return children.Count;
        }
        protected int RemoveChildren(object? argument, Action<INode, object?>? callback) {
            var children = this.Children.Reverse().ToList();
            foreach (var child in children) {
                this.RemoveChild( child, argument, callback );
            }
            return children.Count;
        }

        // RemoveSelf
        protected void RemoveSelf(object? argument, Action<INode, object?>? callback) {
            Assert.Operation.Message( $"Node {this} must have parent" ).Valid( this.Parent != null );
            ((NodeBase) this.Parent).RemoveChild( this, argument, callback );
        }

        // Sort
        protected virtual void Sort(List<INode> children) {
            //children.Sort( (a, b) => Comparer<int>.Default.Compare( GetOrderOf( a ), GetOrderOf( b ) ) );
        }

    }
}
