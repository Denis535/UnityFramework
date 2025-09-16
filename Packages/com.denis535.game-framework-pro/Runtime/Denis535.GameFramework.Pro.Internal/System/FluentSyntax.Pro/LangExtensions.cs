#nullable enable
namespace System {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public static class LangExtensions {

        // Chain
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static T Chain<T>(this T value, Action<T> callback) {
            callback( value );
            return value;
        }
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static TResult Pipe<T, TResult>(this T value, Func<T, TResult> converter) {
            return converter( value );
        }
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static Option<T> If<T>(this T value, Func<T, bool> predicate) {
            if (predicate( value )) return Option.Create( value );
            return default;
        }

        // Chain
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static Option<T> Chain2<T>(this Option<T> value, Action<T> callback) {
            if (value.HasValue) callback( value.Value );
            return default;
        }
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static Option<TResult> Pipe2<T, TResult>(this Option<T> value, Func<T, TResult> converter) {
            if (value.HasValue) return Option.Create( converter( value.Value ) );
            return default;
        }
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static Option<T> If2<T>(this Option<T> value, Func<T, bool> predicate) {
            if (value.HasValue && predicate( value.Value )) return Option.Create( value.Value );
            return default;
        }

    }
}
