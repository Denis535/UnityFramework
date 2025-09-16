#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.TreeMachine.Pro;
    using UnityEngine;
    using UnityEngine.UIElements;

    public abstract class WidgetBase : DisposableBase {

        // Node
        public Node2<WidgetBase> Node { get; }
        // Screen
        protected ScreenBase Screen {
            get {
                Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
                Assert.Operation.Message( $"Widget {this} must be active or activating or deactivating" ).Valid( this.Node.Activity is Activity.Active or Activity.Activating or Activity.Deactivating );
                return ((TreeMachine<Node2<WidgetBase>, ScreenBase>?) this.Node.Machine)!.UserData;
            }
        }

        // Document
        protected UIDocument Document => this.Screen.Document;
        // AudioSource
        protected AudioSource AudioSource => this.Screen.AudioSource;

        // Constructor
        public WidgetBase() {
            this.Node = new Node2<WidgetBase>( this ) {
                SortDelegate = this.Sort
            };
            this.Node.OnActivateCallback += this.OnActivate;
            this.Node.OnDeactivateCallback += this.OnDeactivate;
            this.Node.OnBeforeDescendantActivateCallback += this.OnBeforeDescendantActivate;
            this.Node.OnAfterDescendantActivateCallback += this.OnAfterDescendantActivate;
            this.Node.OnBeforeDescendantDeactivateCallback += this.OnBeforeDescendantDeactivate;
            this.Node.OnAfterDescendantDeactivateCallback += this.OnAfterDescendantDeactivate;
        }
        public override void Dispose() {
            Assert.Operation.Message( $"Widget {this} must be inactive" ).Valid( this.Node.Activity is Activity.Inactive );
            Assert.Operation.Message( $"Widget/Children {this} must be disposed" ).Valid( this.Node.Children.All( i => i.Widget().IsDisposed ) );
            base.Dispose();
        }

        // OnActivate
        protected abstract void OnActivate(object? argument);
        protected abstract void OnDeactivate(object? argument);

        // OnDescendantActivate
        protected virtual void OnBeforeDescendantActivate(NodeBase descendant, object? argument) {
        }
        protected virtual void OnAfterDescendantActivate(NodeBase descendant, object? argument) {
        }
        protected virtual void OnBeforeDescendantDeactivate(NodeBase descendant, object? argument) {
        }
        protected virtual void OnAfterDescendantDeactivate(NodeBase descendant, object? argument) {
        }

        // Sort
        protected virtual void Sort(List<NodeBase> children) {
        }

    }
    public abstract class ViewableWidgetBase : WidgetBase {

        private ViewBase view = default!;

        // View
        public ViewBase View {
            get {
                Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
                return view;
            }
            init {
                Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
                view = value;
            }
        }

        // Constructor
        public ViewableWidgetBase() {
        }
        public override void Dispose() {
            Assert.Operation.Message( $"Widget/View {this} must be disposed" ).Valid( this.View.IsDisposed );
            base.Dispose();
        }

        // ShowSelf
        protected virtual void ShowSelf() {
            Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            Assert.Operation.Message( $"Widget {this} must be non-root" ).NotDisposed( !this.Node.IsRoot );
            this.ShowViewRecursive( this.View );
        }
        protected virtual void HideSelf() {
            Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            Assert.Operation.Message( $"Widget {this} must be non-root" ).NotDisposed( !this.Node.IsRoot );
            this.HideViewRecursive( this.View );
        }

        // ShowView
        protected virtual void ShowView(ViewBase view) {
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-disposed" ).Valid( !view.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-attached to parent" ).Valid( !view.IsAttachedToParent );
            Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            var wasShown = TryShowView( this, view );
            Assert.Operation.Message( $"View {view} was not shown" ).Valid( wasShown );
        }
        protected virtual void HideView(ViewBase view) {
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-disposed" ).Valid( !view.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be attached to parent" ).Valid( view.IsAttachedToParent );
            Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            var wasHidden = TryHideView( this, view );
            Assert.Operation.Message( $"View {view} was not hidden" ).Valid( wasHidden );
        }

        // ShowViewRecursive
        protected virtual void ShowViewRecursive(ViewBase view) {
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-disposed" ).Valid( !view.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-attached to parent" ).Valid( !view.IsAttachedToParent );
            Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            var wasShown = TryShowViewRecursive( this, view );
            Assert.Operation.Message( $"View {view} was not shown" ).Valid( wasShown );
        }
        protected virtual void HideViewRecursive(ViewBase view) {
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-disposed" ).Valid( !view.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be attached to parent" ).Valid( view.IsAttachedToParent );
            Assert.Operation.Message( $"Widget {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            var wasHidden = TryHideViewRecursive( this, view );
            Assert.Operation.Message( $"View {view} was not hidden" ).Valid( wasHidden );
        }

        // Helpers
        private static bool TryShowView(WidgetBase widget, ViewBase view) {
            Assert.Operation.Message( $"Argument 'widget' ({widget}) must be non-disposed" ).NotDisposed( !widget.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-disposed" ).Valid( !view.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-attached to parent" ).Valid( !view.IsAttachedToParent );
            if (widget is ViewableWidgetBase widget_ && widget_.View.TryAddView( view )) {
                return true;
            }
            return false;
        }
        private static bool TryHideView(WidgetBase widget, ViewBase view) {
            Assert.Operation.Message( $"Argument 'widget' ({widget}) must be non-disposed" ).NotDisposed( !widget.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-disposed" ).Valid( !view.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be attached to parent" ).Valid( view.IsAttachedToParent );
            if (widget is ViewableWidgetBase widget_ && widget_.View.TryRemoveView( view )) {
                return true;
            }
            return false;
        }
        // Helpers
        private static bool TryShowViewRecursive(WidgetBase widget, ViewBase view) {
            Assert.Operation.Message( $"Argument 'widget' ({widget}) must be non-disposed" ).NotDisposed( !widget.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-disposed" ).Valid( !view.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-attached to parent" ).Valid( !view.IsAttachedToParent );
            if (widget is ViewableWidgetBase widget_ && widget_.View.TryAddView( view )) {
                return true;
            }
            if (widget.Node.Parent != null) {
                return TryShowViewRecursive( widget.Node.Parent.Widget(), view );
            }
            return false;
        }
        private static bool TryHideViewRecursive(WidgetBase widget, ViewBase view) {
            Assert.Operation.Message( $"Argument 'widget' ({widget}) must be non-disposed" ).NotDisposed( !widget.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-disposed" ).Valid( !view.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be attached to parent" ).Valid( view.IsAttachedToParent );
            if (widget is ViewableWidgetBase widget_ && widget_.View.TryRemoveView( view )) {
                return true;
            }
            if (widget.Node.Parent != null) {
                return TryHideViewRecursive( widget.Node.Parent.Widget(), view );
            }
            return false;
        }

    }
    public abstract class ViewableWidgetBase<TView> : ViewableWidgetBase where TView : notnull, ViewBase {

        // View
        protected internal new TView View {
            get => (TView) base.View;
            init => base.View = value;
        }

        // Constructor
        public ViewableWidgetBase() {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
