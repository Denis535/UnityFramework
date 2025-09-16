#nullable enable
namespace System {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public static class Exceptions {
        // Argument
        public static class Argument {
            public static ArgumentException ArgumentException(FormattableString? message) {
                return Factory.GetException<ArgumentException>( message );
            }
            public static ArgumentNullException ArgumentNullException(FormattableString? message) {
                return Factory.GetException<ArgumentNullException>( message );
            }
            public static ArgumentOutOfRangeException ArgumentOutOfRangeException(FormattableString? message) {
                return Factory.GetException<ArgumentOutOfRangeException>( message );
            }
        }
        // Operation
        public static class Operation {
            public static InvalidOperationException InvalidOperationException(FormattableString? message) {
                return Factory.GetException<InvalidOperationException>( message );
            }
            public static ObjectNotReadyException ObjectNotReadyException(FormattableString? message) {
                return Factory.GetException<ObjectNotReadyException>( message );
            }
            public static ObjectDisposedException ObjectDisposedException(FormattableString? message) {
                return Factory.GetException<ObjectDisposedException>( message );
            }
        }
        // Internal
        public static class Internal {
            public static Exception Exception(FormattableString? message) {
                return Factory.GetException<Exception>( message );
            }
            public static NullReferenceException NullReference(FormattableString? message) {
                return Factory.GetException<NullReferenceException>( message );
            }
            public static NotSupportedException NotSupported(FormattableString? message) {
                return Factory.GetException<NotSupportedException>( message );
            }
            public static NotImplementedException NotImplemented(FormattableString? message) {
                return Factory.GetException<NotImplementedException>( message );
            }
        }
        // Factory
        public static class Factory {

            public static Func<FormattableString?, string?> GetMessageStringDelegate = GetMessageString;
            public static Func<object?, string> GetArgumentStringDelegate = GetArgumentString;
            public static Func<Type, string?, Exception> GetExceptionDelegate = GetException;

            // GetException
            public static T GetException<T>(FormattableString? message) where T : Exception {
                var message2 = GetMessageStringDelegate( message );
                return (T) GetExceptionDelegate( typeof( T ), message2 );
            }

            // Helpers
            private static string? GetMessageString(FormattableString? message) {
                if (message != null) {
                    var format = message.Format.Replace( "{", "'{" ).Replace( "}", "}'" );
                    var arguments = message.GetArguments();
                    for (var i = 0; i < arguments.Length; i++) {
                        arguments[ i ] = GetArgumentStringDelegate( arguments[ i ] );
                    }
                    return string.Format( format, arguments );
                }
                return null;
            }
            private static string GetArgumentString(object? argument) {
                if (argument is ICollection collection) {
                    return string.Join( ", ", collection.Cast<object?>().Select( GetArgumentString ) );
                }
                if (argument is IList list) {
                    return string.Join( ", ", list.Cast<object?>().Select( GetArgumentString ) );
                }
                if (argument is not null) {
                    return argument.ToString();
                }
                return "Null";
            }
            private static Exception GetException(Type type, string? message) {
                var constructor = type.GetConstructor( new[] { typeof( string ), typeof( Exception ) } );
                return (Exception) constructor.Invoke( new[] { message, null } );
            }

        }
    }
    // ObjectNotReadyException
    public class ObjectNotReadyException : InvalidOperationException {
        public ObjectNotReadyException(string message) : base( message ) {
        }
        public ObjectNotReadyException(string message, Exception? innerException) : base( message, innerException ) {
        }
    }
}
