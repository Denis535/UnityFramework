#nullable enable
namespace System {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [Serializable]
    public readonly struct Option<T> : IEquatable<Option<T>>, IEquatable<T>, IComparable<Option<T>>, IComparable<T> {

        private readonly bool hasValue;
        private readonly T value; // always is null/default when Option has no value

        // Value
        public bool HasValue => hasValue;
        public T Value => hasValue ? value : throw new InvalidOperationException( "Option has no value" );
        public T? ValueOrDefault => hasValue ? value : default; // always is null/default when Option has no value

        // Constructor
        //public Option() {
        //    this.hasValue = false;
        //    this.value = default!;
        //}
        internal Option(T value) {
            this.hasValue = true;
            this.value = value;
        }

        // TryGetValue
        public bool TryGetValue([MaybeNullWhen( false )] out T value) {
            if (this.HasValue) {
                value = this.Value;
                return true;
            }
            value = default;
            return false;
        }

        // Equals
        bool IEquatable<Option<T>>.Equals(Option<T> other) {
            return this.IsEqualTo( other );
        }
        bool IEquatable<T>.Equals(T other) {
            return this.IsEqualTo( other );
        }

        // IsEqualTo
        public bool IsEqualTo(Option<T> other) {
            if (this.HasValue && other.HasValue) return EqualityComparer<T>.Default.Equals( this.Value, other.Value );
            return EqualityComparer<bool>.Default.Equals( this.HasValue, other.HasValue );
        }
        public bool IsEqualTo(T other) {
            if (this.HasValue && true) return EqualityComparer<T>.Default.Equals( this.Value, other );
            return EqualityComparer<bool>.Default.Equals( this.HasValue, true );
        }

        // CompareTo
        public int CompareTo(Option<T> other) {
            if (this.HasValue && other.HasValue) return Comparer<T>.Default.Compare( this.Value, other.Value );
            return Comparer<bool>.Default.Compare( this.HasValue, other.HasValue );
        }
        public int CompareTo(T other) {
            if (this.HasValue && true) return Comparer<T>.Default.Compare( this.Value, other );
            return Comparer<bool>.Default.Compare( this.HasValue, true );
        }

        // Utils
        public override string ToString() {
            if (this.HasValue) return this.Value?.ToString() ?? "Null";
            return "Nothing";
        }
        public override bool Equals(object? other) {
            if (other is Option<T> other_) return this.IsEqualTo( other_ );
            if (other is T other__) return this.IsEqualTo( other__ );
            return false;
        }
        public override int GetHashCode() {
            if (this.HasValue) return this.Value?.GetHashCode() ?? 0;
            return 0;
        }

        // Utils
        public static bool operator ==(Option<T> left, Option<T> right) {
            return left.IsEqualTo( right );
        }
        public static bool operator ==(Option<T> left, T right) {
            return left.IsEqualTo( right );
        }
        public static bool operator ==(T left, Option<T> right) {
            return right.IsEqualTo( left );
        }

        // Utils
        public static bool operator !=(Option<T> left, Option<T> right) {
            return !left.IsEqualTo( right );
        }
        public static bool operator !=(Option<T> left, T right) {
            return !left.IsEqualTo( right );
        }
        public static bool operator !=(T left, Option<T> right) {
            return !right.IsEqualTo( left );
        }

        // Utils
        //public static explicit operator Option<T>(T value) {
        //    return new Option<T>( value );
        //}
        //public static explicit operator Option<object?>(Option<T> value) {
        //    // Todo:    
        //    // How to write explicit cast operator with its own generic type???
        //    // Now I can cast only to non-generic types!!!
        //    // https://github.com/dotnet/csharplang/issues/813
        //    return new Option<object?>( value.hasValue, value.value );
        //}
        //public static explicit operator Option<T>(Option<object?> value) {
        //    return new Option<T>( value.hasValue, (T) value.value );
        //}

    }
    public static class Option {

        // Create
        public static Option<T> Create<T>() {
            return default;
        }
        public static Option<T> Create<T>(T value) {
            return new Option<T>( value );
        }
        public static Option<T> Create<T>(T? value) where T : struct {
            if (value.HasValue) return new Option<T>( value.Value );
            return default;
        }

        // AreEqual
        public static bool AreEqual<T>(Option<T> value, Option<T> other) {
            if (value.HasValue && other.HasValue) return EqualityComparer<T>.Default.Equals( value.Value, other.Value );
            return EqualityComparer<bool>.Default.Equals( value.HasValue, other.HasValue );
        }
        public static bool AreEqual<T>(Option<T> value, T other) {
            if (value.HasValue && true) return EqualityComparer<T>.Default.Equals( value.Value, other );
            return EqualityComparer<bool>.Default.Equals( false, true );
        }
        public static bool AreEqual<T>(T value, Option<T> other) {
            if (true && other.HasValue) return EqualityComparer<T>.Default.Equals( value, other.Value );
            return EqualityComparer<bool>.Default.Equals( true, false );
        }

        // Compare
        public static int Compare<T>(Option<T> value, Option<T> other) {
            if (value.HasValue && other.HasValue) return Comparer<T>.Default.Compare( value.Value, other.Value );
            return Comparer<bool>.Default.Compare( value.HasValue, other.HasValue );
        }
        public static int Compare<T>(Option<T> value, T other) {
            if (value.HasValue && true) return Comparer<T>.Default.Compare( value.Value, other );
            return Comparer<bool>.Default.Compare( false, true );
        }
        public static int Compare<T>(T value, Option<T> other) {
            if (true && other.HasValue) return Comparer<T>.Default.Compare( value, other.Value );
            return Comparer<bool>.Default.Compare( true, false );
        }

        // GetUnderlyingType
        public static Type GetUnderlyingType(Type type) {
            if (type == null) throw new ArgumentNullException( "type" );
            if (!type.IsGenericType) throw new ArgumentException( "Argument 'type' is invalid" );
            if (type.IsGenericTypeDefinition) throw new ArgumentException( "Argument 'type' is invalid" );
            if (type.GetGenericTypeDefinition() != typeof( Option<> )) throw new ArgumentException( "Argument 'type' is invalid" );
            return type.GetGenericArguments()[ 0 ];
        }

    }
}
