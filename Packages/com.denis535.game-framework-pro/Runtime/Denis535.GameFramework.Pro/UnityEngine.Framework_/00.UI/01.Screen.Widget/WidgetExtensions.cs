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
            return ((IUserData<WidgetBase>) node).UserData;
        }
        public static T Widget<T>(this INode node) where T : notnull, WidgetBase {
            return (T) ((IUserData<WidgetBase>) node).UserData;
        }

        // GetCancellationToken
        public static CancellationToken GetCancellationToken_OnDetachCallback(this Node node) {
            var cts = new CancellationTokenSource();
            node.OnDetachCallback += Callback;
            void Callback(object? argument) {
                cts.Cancel();
                node.OnDetachCallback -= Callback;
            }
            return cts.Token;
        }
        public static CancellationToken GetCancellationToken_OnDeactivateCallback(this Node node) {
            var cts = new CancellationTokenSource();
            node.OnDeactivateCallback += Callback;
            void Callback(object? argument) {
                cts.Cancel();
                node.OnDeactivateCallback -= Callback;
            }
            return cts.Token;
        }

        // GetCancellationToken
        public static CancellationToken GetCancellationToken_OnDetachCallback(this Node2 node) {
            var cts = new CancellationTokenSource();
            node.OnDetachCallback += Callback;
            void Callback(object? argument) {
                cts.Cancel();
                node.OnDetachCallback -= Callback;
            }
            return cts.Token;
        }
        public static CancellationToken GetCancellationToken_OnDeactivateCallback(this Node2 node) {
            var cts = new CancellationTokenSource();
            node.OnDeactivateCallback += Callback;
            void Callback(object? argument) {
                cts.Cancel();
                node.OnDeactivateCallback -= Callback;
            }
            return cts.Token;
        }

    }
}
