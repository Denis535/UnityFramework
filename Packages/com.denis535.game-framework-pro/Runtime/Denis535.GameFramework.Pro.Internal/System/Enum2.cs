#nullable enable
namespace System {
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public static class Enum2 {

        // Values
        public static T[] GetValues<T>() where T : notnull, Enum {
            return (T[]) Enum.GetValues( typeof( T ) );
        }

    }
}
