#nullable enable
namespace UnityEngine.UIElements {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public static partial class VisualElementExtensions {

        // IsAttachedToParent
        public static bool IsAttachedToParent(this VisualElement element) {
            return element.parent != null;
        }

        // IsAttachedToPanel
        public static bool IsAttachedToPanel(this VisualElement element) {
            return element.panel != null;
        }

        // IsDisplayed
        public static bool IsDisplayedSelf(this VisualElement element) {
            return !element.ClassListContains( "unity-hidden" );
        }
        public static bool IsDisplayedInHierarchy(this VisualElement element) {
            return element.AncestorsAndSelf().All( IsDisplayedSelf );
        }
        public static void SetDisplayed(this VisualElement element, bool value) {
            if (value) {
                element.RemoveFromClassList( "unity-hidden" );
            } else {
                element.AddToClassList( "unity-hidden" );
            }
        }

        // IsValid
        public static bool IsValidSelf(this VisualElement element) {
            return !element.ClassListContains( "invalid" );
        }
        public static bool IsValidInHierarchy(this VisualElement element) {
            return element.AncestorsAndSelf().All( IsValidSelf );
        }
        public static void SetValid(this VisualElement element, bool value) {
            if (value) {
                element.RemoveFromClassList( "invalid" );
            } else {
                element.AddToClassList( "invalid" );
            }
        }

        // SetUp
        public static T Name<T>(this T element, string? name) where T : notnull, VisualElement {
            element.name = name;
            return element;
        }
        public static T Class<T>(this T element, string @class) where T : notnull, VisualElement {
            element.AddToClassList( @class );
            return element;
        }
        public static T Style<T>(this T element, Action<IStyle> callback) where T : notnull, VisualElement {
            callback( element.style );
            return element;
        }
        public static T Text<T>(this T element, string? text) where T : notnull, TextElement {
            element.text = text;
            return element;
        }
        public static T UserData<T>(this T element, object? userData) where T : notnull, VisualElement {
            element.userData = userData;
            return element;
        }
        public static T Children<T>(this T element, params VisualElement?[] children) where T : notnull, VisualElement {
            foreach (var child in children) {
                element.Add( child );
            }
            return element;
        }

        // FindElement
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static T FindElement<T>(this VisualElement element, string? name, params string[] classes) where T : notnull, VisualElement {
            return element.Query<T>( name, classes ).First();
        }
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static T[] FindElements<T>(this VisualElement element, string? name, params string[] classes) where T : notnull, VisualElement {
            return element.Query<T>( name, classes ).ToList().ToArray();
        }

        // RequireElement
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static T RequireElement<T>(this VisualElement element, string? name, params string[] classes) where T : notnull, VisualElement {
            var result = element.Query<T>( name, classes ).First();
            Assert.Operation.Message( $"Element {typeof( T )} was not found" ).Valid( result != null );
            return result;
        }
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static T[] RequireElements<T>(this VisualElement element, string? name, params string[] classes) where T : notnull, VisualElement {
            var result = element.Query<T>( name, classes ).ToList().ToArray().NullIfEmpty();
            Assert.Operation.Message( $"Elements {typeof( T )} was not found" ).Valid( result != null );
            return result;
        }

    }
}
