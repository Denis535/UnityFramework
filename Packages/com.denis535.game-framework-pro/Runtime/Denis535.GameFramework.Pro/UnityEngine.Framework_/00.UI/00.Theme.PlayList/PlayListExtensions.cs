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
        public static PlayListBase PlayList(this StateBase state) {
            return ((State<PlayListBase>) state).UserData;
        }
        public static T PlayList<T>(this StateBase state) where T : PlayListBase {
            return (T) ((State<PlayListBase>) state).UserData;
        }

        // GetCancellationToken
        public static CancellationToken GetCancellationToken_OnDetachCallback(this PlayListBase playList) {
            var cts = new CancellationTokenSource();
            playList.State.OnDetachCallback += Callback;
            void Callback(object? argument) {
                cts.Cancel();
                playList.State.OnDetachCallback -= Callback;
            }
            return cts.Token;
        }
        public static CancellationToken GetCancellationToken_OnDeactivateCallback(this PlayListBase playList) {
            var cts = new CancellationTokenSource();
            playList.State.OnDeactivateCallback += Callback;
            void Callback(object? argument) {
                cts.Cancel();
                playList.State.OnDeactivateCallback -= Callback;
            }
            return cts.Token;
        }

    }
}
