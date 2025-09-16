#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.TreeMachine.Pro;
    using UnityEngine;

    public static class WidgetExtensions {

        // Widget
        public static WidgetBase Widget(this NodeBase node) {
            return ((Node2<WidgetBase>) node).UserData;
        }
        public static T Widget<T>(this NodeBase node) where T : WidgetBase {
            return (T) ((Node2<WidgetBase>) node).UserData;
        }

        // GetCancellationToken
        public static CancellationToken GetCancellationToken_OnDetachCallback(this WidgetBase widget) {
            var cts = new CancellationTokenSource();
            widget.Node.OnDetachCallback += Callback;
            void Callback(object? argument) {
                cts.Cancel();
                widget.Node.OnDetachCallback -= Callback;
            }
            return cts.Token;
        }
        public static CancellationToken GetCancellationToken_OnDeactivateCallback(this WidgetBase widget) {
            var cts = new CancellationTokenSource();
            widget.Node.OnDeactivateCallback += Callback;
            void Callback(object? argument) {
                cts.Cancel();
                widget.Node.OnDeactivateCallback -= Callback;
            }
            return cts.Token;
        }

    }
}
