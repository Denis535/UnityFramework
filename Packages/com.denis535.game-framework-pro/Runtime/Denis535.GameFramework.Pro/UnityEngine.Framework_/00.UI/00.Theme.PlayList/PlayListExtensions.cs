#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.StateMachine.Pro;
    using System.Threading;
    using UnityEngine;

    public static class PlayListExtensions {

        // PlayList
        public static PlayListBase PlayList(this IState state) {
            return ((IUserData<PlayListBase>) state).UserData;
        }
        public static T PlayList<T>(this IState state) where T : notnull, PlayListBase {
            return (T) ((IUserData<PlayListBase>) state).UserData;
        }

        // GetCancellationToken
        public static CancellationToken GetCancellationToken_OnDetachCallback(this State state) {
            var cts = new CancellationTokenSource();
            state.OnDetachCallback += Callback;
            void Callback(object? argument) {
                cts.Cancel();
                state.OnDetachCallback -= Callback;
            }
            return cts.Token;
        }
        public static CancellationToken GetCancellationToken_OnDeactivateCallback(this State state) {
            var cts = new CancellationTokenSource();
            state.OnDeactivateCallback += Callback;
            void Callback(object? argument) {
                cts.Cancel();
                state.OnDeactivateCallback -= Callback;
            }
            return cts.Token;
        }

    }
}
