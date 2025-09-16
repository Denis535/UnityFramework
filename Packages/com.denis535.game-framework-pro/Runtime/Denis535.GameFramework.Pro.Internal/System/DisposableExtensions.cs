#nullable enable
namespace System {
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public static class DisposableExtensions {

        // DisposeAll
        public static void DisposeAll(this IEnumerable<IDisposable> disposables) {
            foreach (var disposable in disposables) {
                disposable.Dispose();
            }
        }

    }
}
