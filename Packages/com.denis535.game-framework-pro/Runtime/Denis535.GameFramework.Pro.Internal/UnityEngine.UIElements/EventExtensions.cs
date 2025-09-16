#nullable enable
namespace UnityEngine.UIElements {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using UnityEngine;

    public static class EventExtensions {

        // GetTarget
        public static VisualElement GetTarget(this EventBase @event) {
            return (VisualElement) @event.target;
        }
        public static VisualElement GetTarget<T>(this EventBase @event) where T : notnull, VisualElement {
            return (T) @event.target;
        }

        // SendEvent
        public static void SendEventImmediate(this VisualElement element, EventBase @event) {
            Assert.Operation.Message( $"Element {element} must be attached" ).Valid( element.IsAttachedToPanel() );
            var type_VisualElement = typeof( VisualElement );
            var type_EventBase = typeof( EventBase );
            var type_DispatchMode = typeof( VisualElement ).Assembly.GetType( "UnityEngine.UIElements.DispatchMode" ) ?? throw Exceptions.Internal.Exception( $"Can not find 'DispatchMode' type" );
            var method_SendEvent = type_VisualElement.GetMethod( "SendEvent", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly, null, new Type[] { type_EventBase, type_DispatchMode }, null ) ?? throw Exceptions.Internal.Exception( $"Can not find 'SendEvent' method" );
            _ = method_SendEvent.Invoke( element, new object[] { @event, 2 } );
        }

    }
}
