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
        public static WidgetBase Widget(this INode node) {
            return ((Node2<WidgetBase>) node).UserData;
        }
        public static T Widget<T>(this INode node) where T : notnull, WidgetBase {
            return (T) ((Node2<WidgetBase>) node).UserData;
        }

        // GetCancellationToken
        public static CancellationToken GetCancellationToken_OnDetachCallback(this WidgetBase widget) {
            var cts = new CancellationTokenSource();
            widget.NodeMutable.OnDetachCallback += Callback;
            void Callback(object? argument) {
                cts.Cancel();
                widget.NodeMutable.OnDetachCallback -= Callback;
            }
            return cts.Token;
        }
        public static CancellationToken GetCancellationToken_OnDeactivateCallback(this WidgetBase widget) {
            var cts = new CancellationTokenSource();
            widget.NodeMutable.OnDeactivateCallback += Callback;
            void Callback(object? argument) {
                cts.Cancel();
                widget.NodeMutable.OnDeactivateCallback -= Callback;
            }
            return cts.Token;
        }

    }
}
