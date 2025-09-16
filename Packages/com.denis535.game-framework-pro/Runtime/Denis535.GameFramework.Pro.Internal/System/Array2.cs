#nullable enable
namespace System {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public static class Array2 {

        // Create
        public static T[] Create<T>(params T[] array) {
            return array;
        }

        // NullIfEmpty
        public static T[]? NullIfEmpty<T>(this T[] array) {
            return array.Any() ? array : null;
        }

    }
}
