#nullable enable
namespace UnityEngine.Framework.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UIElements;

    public static class UIWidgetExtensions {

        // IsEnabled
        public static bool IsEnabledInHierarchy(this UIWidgetBase widget) {
            return widget.View!.VisualElement.enabledInHierarchy;
        }
        public static bool IsEnabledSelf(this UIWidgetBase widget) {
            return widget.View!.VisualElement.enabledSelf;
        }
        public static void SetEnabled(this UIWidgetBase widget, bool value) {
            widget.View!.VisualElement.SetEnabled( value );
        }

        // IsDisplayed
        public static bool IsDisplayed(this UIWidgetBase widget) {
            return widget.View!.VisualElement.IsDisplayed();
        }
        public static void SetDisplayed(this UIWidgetBase widget, bool value) {
            widget.View!.VisualElement.SetDisplayed( value );
        }

        // AttachChild
        public static void AttachChild(this UIWidgetBase widget, UIWidgetBase child, object? argument = null) {
            Assert.Argument.Message( $"Argument 'child' must be non-null" ).NotNull( child != null );
            Assert.Operation.Message( $"Widget {widget} must have no child {child} widget" ).Valid( !widget.Children.Contains( child ) );
            widget.__AttachChild__( child, argument );
        }

        // DetachSelf
        public static void DetachSelf(this UIWidgetBase widget, object? argument = null) {
            Assert.Operation.Message( $"Widget {widget} must have parent or must be attached" ).Valid( widget.Parent != null || widget.IsAttached );
            if (widget.Parent != null) {
                widget.Parent.DetachChild( widget, argument );
            } else {
                widget.Screen!.DetachWidget( widget, argument );
            }
        }

        // DetachChild
        public static void DetachChild<T>(this UIWidgetBase widget, object? argument = null) where T : UIWidgetBase {
            Assert.Operation.Message( $"Widget {widget} must have child {typeof( T )} widget" ).Valid( widget.Children.OfType<T>().Any() );
            widget.__DetachChild__( widget.Children.OfType<T>().Last(), argument );
        }
        public static void DetachChild(this UIWidgetBase widget, UIWidgetBase child, object? argument = null) {
            Assert.Argument.Message( $"Argument 'child' must be non-null" ).NotNull( child != null );
            Assert.Operation.Message( $"Widget {widget} must have child {child} widget" ).Valid( widget.Children.Contains( child ) );
            widget.__DetachChild__( child, argument );
        }

        // DetachChildren
        public static void DetachChildren(this UIWidgetBase widget, object? argument = null) {
            foreach (var child in widget.Children.Reverse()) {
                widget.__DetachChild__( child, argument );
            }
        }

        // OnAttach
        public static void OnBeforeAttach(this UIWidgetBase widget, Action? callback) {
            widget.OnBeforeAttachEvent += callback;
        }
        public static void OnAfterAttach(this UIWidgetBase widget, Action? callback) {
            widget.OnAfterAttachEvent += callback;
        }
        public static void OnBeforeDetach(this UIWidgetBase widget, Action? callback) {
            widget.OnBeforeDetachEvent += callback;
        }
        public static void OnAfterDetach(this UIWidgetBase widget, Action? callback) {
            widget.OnAfterDetachEvent += callback;
        }

        // OnDescendantAttach
        public static void OnBeforeDescendantAttach(this UIWidgetBase widget, Action<UIWidgetBase>? callback) {
            widget.OnBeforeDescendantAttachEvent += callback;
        }
        public static void OnAfterDescendantAttach(this UIWidgetBase widget, Action<UIWidgetBase>? callback) {
            widget.OnAfterDescendantAttachEvent += callback;
        }
        public static void OnBeforeDescendantDetach(this UIWidgetBase widget, Action<UIWidgetBase>? callback) {
            widget.OnBeforeDescendantDetachEvent += callback;
        }
        public static void OnAfterDescendantDetach(this UIWidgetBase widget, Action<UIWidgetBase>? callback) {
            widget.OnAfterDescendantDetachEvent += callback;
        }

        // GetView
        public static UIViewBase? __GetView__(this UIWidgetBase widget) {
            // try not to use it
            return widget?.View;
        }
        public static UIViewBase __GetView__<T>(this UIWidgetBase<T> widget) where T : UIViewBase {
            // try not to use it
            return widget.View;
        }

        // GetVisualElement
        public static VisualElement? __GetVisualElement__(this UIWidgetBase widget) {
            // try not to use it
            return widget?.View?.VisualElement;
        }
        public static VisualElement __GetVisualElement__<T>(this UIWidgetBase<T> widget) where T : UIViewBase {
            // try not to use it
            return widget.View.VisualElement;
        }

    }
}
