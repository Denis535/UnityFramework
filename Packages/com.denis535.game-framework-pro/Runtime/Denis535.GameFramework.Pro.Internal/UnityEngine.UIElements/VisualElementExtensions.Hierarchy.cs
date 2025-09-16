#nullable enable
namespace UnityEngine.UIElements {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static partial class VisualElementExtensions {

        // IsAncestorOf
        public static bool IsAncestorOf(this VisualElement element, VisualElement descendant) {
            return element.Contains( descendant );
        }

        // IsDescendantOf
        public static bool IsDescendantOf(this VisualElement element, VisualElement ancestor) {
            return ancestor.Contains( element );
        }

        // Parent
        //public static VisualElement Parent(this VisualElement element) {
        //    return element.parent;
        //}

        // Ancestors
        public static IEnumerable<VisualElement> Ancestors(this VisualElement element) {
            while (element.parent != null) {
                yield return element.parent;
                element = element.parent;
            }
        }
        public static IEnumerable<VisualElement> AncestorsAndSelf(this VisualElement element) {
            yield return element;
            foreach (var i in element.Ancestors()) yield return i;
        }

        // Children
        //public static VisualElement[] Children(this VisualElement element) {
        //    return element.Children().ToArray();
        //}

        // Descendants
        public static IEnumerable<VisualElement> Descendants(this VisualElement element) {
            foreach (var child in element.Children()) {
                yield return child;
                foreach (var i in child.Descendants()) yield return i;
            }
        }
        public static IEnumerable<VisualElement> DescendantsAndSelf(this VisualElement element) {
            yield return element;
            foreach (var i in element.Descendants()) yield return i;
        }

        // Add
        //public static void Add(this VisualElement element, VisualElement child) {
        //    element.Add( child );
        //}
        //public static void Insert(this VisualElement element, int index, VisualElement child) {
        //    element.Insert( index, child );
        //}
        public static void InsertBefore(this VisualElement element, VisualElement index, VisualElement child) {
            var index_ = element.IndexOf( index );
            element.Insert( index_, child );
        }
        public static void InsertAfter(this VisualElement element, VisualElement index, VisualElement child) {
            var index_ = element.IndexOf( index ) + 1;
            element.Insert( index_, child );
        }
        //public static void Remove(this VisualElement element, VisualElement child) {
        //    element.Remove( child );
        //}
        //public static void RemoveAt(this VisualElement element, int index) {
        //    element.RemoveAt( index );
        //}
        public static void Replace(this VisualElement element, VisualElement oldChild, VisualElement newChild) {
            var index = element.IndexOf( oldChild );
            element.RemoveAt( index );
            element.Insert( index, newChild );
        }

        // Add
        public static void Add(this VisualElement element, params VisualElement[] children) {
            foreach (var child in children) {
                element.Add( child );
            }
        }
        public static void Insert(this VisualElement element, int index, params VisualElement[] children) {
            foreach (var child in children) {
                element.Insert( index++, child );
            }
        }
        public static void InsertBefore(this VisualElement element, VisualElement index, params VisualElement[] children) {
            var index_ = element.IndexOf( index );
            foreach (var child in children) {
                element.Insert( index_++, child );
            }
        }
        public static void InsertAfter(this VisualElement element, VisualElement index, params VisualElement[] children) {
            var index_ = element.IndexOf( index ) + 1;
            foreach (var child in children) {
                element.Insert( index_++, child );
            }
        }
        public static void Remove(this VisualElement element, params VisualElement[] children) {
            foreach (var child in children) {
                element.Remove( child );
            }
        }
        public static void RemoveAt(this VisualElement element, int index, int count) {
            for (var i = 0; i < count; i++) {
                element.RemoveAt( index );
            }
        }
        public static void Replace(this VisualElement element, VisualElement oldChild, params VisualElement[] newChildren) {
            var index = element.IndexOf( oldChild );
            element.RemoveAt( index );
            foreach (var child in newChildren) {
                element.Insert( index++, child );
            }
        }

    }
}
