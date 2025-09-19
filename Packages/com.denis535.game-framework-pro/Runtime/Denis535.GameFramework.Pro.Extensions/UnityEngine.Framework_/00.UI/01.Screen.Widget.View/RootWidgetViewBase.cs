#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UIElements;

    public abstract class RootWidgetViewBase : ViewBase {

        // Views
        public IEnumerable<ViewBase> Views => this.Children().Cast<ViewBase>();
        // OnSubmit
        public event EventCallback<NavigationSubmitEvent> OnSubmitEvent {
            add => this.RegisterCallback( value, TrickleDown.TrickleDown );
            remove => this.UnregisterCallback( value, TrickleDown.TrickleDown );
        }
        public event EventCallback<NavigationCancelEvent> OnCancelEvent {
            add => this.RegisterCallback( value, TrickleDown.TrickleDown );
            remove => this.UnregisterCallback( value, TrickleDown.TrickleDown );
        }

        // Constructor
        public RootWidgetViewBase() {
            this.name = "root-widget-view";
            this.AddToClassList( "root-widget-view" );
            this.pickingMode = PickingMode.Ignore;
        }
        public override void Dispose() {
            base.Dispose();
        }

        // TryAddView
        public override bool TryAddView(ViewBase view) {
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-disposed" ).Valid( !view.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-attached to parent" ).Valid( !view.IsAttachedToParent );
            Assert.Operation.Message( $"View {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            this.Add( view );
            this.Sort();
            this.SetVisibility( (IReadOnlyList<VisualElement>) this.Children() );
            return true;
        }
        public override bool TryRemoveView(ViewBase view) {
            Assert.Argument.Message( $"Argument 'view' ({view}) must be non-disposed" ).Valid( !view.IsDisposed );
            Assert.Argument.Message( $"Argument 'view' ({view}) must be attached to parent" ).Valid( view.IsAttachedToParent );
            Assert.Operation.Message( $"View {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            this.Remove( view );
            this.SetVisibility( (IReadOnlyList<VisualElement>) this.Children() );
            return true;
        }

        // Sort
        protected virtual void Sort() {
            this.Sort( (a, b) => Comparer<int>.Default.Compare( this.GetOrderOf( (ViewBase) a ), this.GetOrderOf( (ViewBase) b ) ) );
        }
        protected virtual int GetOrderOf(ViewBase view) {
            return 0;
        }

        // SetVisibility
        protected virtual void SetVisibility(IReadOnlyList<VisualElement> views) {
            SaveFocus( views );
            foreach (var view in views) {
                view.SetEnabled( true );
                view.SetDisplayed( true );
            }
            LoadFocus( views );
        }

        // Helpers
        protected static void SaveFocus(IReadOnlyList<VisualElement> views) {
            foreach (var view in views.SkipLast( 1 ).Cast<ViewBase>()) {
                if (view.HasFocusedElement()) {
                    view.SaveFocus();
                }
            }
        }
        protected static void LoadFocus(IReadOnlyList<VisualElement> views) {
            var view = (ViewBase?) views.LastOrDefault();
            if (view != null) {
                if (!view.HasFocusedElement()) {
                    if (!view.LoadFocus()) {
                        view.InitFocus();
                    }
                }
            }
        }

    }
}
