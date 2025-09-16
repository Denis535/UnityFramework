#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;

    public static class ViewExtensions {

        // Focus
        public static void InitFocus(this ViewBase view) {
            // sometimes it throws an error
            try {
                if (view.focusable) {
                    view.Focus();
                } else {
                    view.focusable = true;
                    view.delegatesFocus = true;
                    view.Focus();
                    view.delegatesFocus = false;
                    view.focusable = false;
                }
            } catch {
            }
        }
        public static bool LoadFocus(this ViewBase view) {
            // sometimes it throws an error
            try {
                var focusedElement = view.LoadFocusedElement();
                if (focusedElement != null) {
                    focusedElement.Focus();
                    return true;
                }
            } catch {
            }
            return false;
        }
        public static void SaveFocus(this ViewBase view) {
            var focusedElement = GetFocusedElement( view );
            view.SaveFocusedElement( focusedElement );
        }

        // GetFocusedElement
        public static VisualElement? GetFocusedElement(this ViewBase view) {
            var focusedElement = (VisualElement?) view.focusController?.focusedElement;
            if (focusedElement != null && (view == focusedElement || view.Contains( focusedElement ))) return focusedElement;
            return null;
        }
        public static bool HasFocusedElement(this ViewBase view) {
            var focusedElement = (VisualElement?) view.focusController?.focusedElement;
            if (focusedElement != null && (view == focusedElement || view.Contains( focusedElement ))) return true;
            return false;
        }

        // LoadFocusedElement
        public static VisualElement? LoadFocusedElement(this ViewBase view) {
            return (VisualElement) view.userData;
        }
        public static void SaveFocusedElement(this ViewBase view, VisualElement? focusedElement) {
            view.userData = focusedElement;
        }

    }
}
