#nullable enable
namespace System {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;

    public static class Assertions {
        // IAssertion
        private interface IAssertion {
            FormattableString? Message { get; }
        }
        // Argument
        public readonly struct Argument : IAssertion {

            public FormattableString? Message { get; }

            public Argument(FormattableString? message) {
                this.Message = message;
            }

            [MethodImpl( MethodImplOptions.AggressiveInlining )]
            public void Valid([DoesNotReturnIf( false )] bool isValid) {
                if (!isValid) throw Exceptions.Factory.GetException<ArgumentException>( this.Message );
            }

            [MethodImpl( MethodImplOptions.AggressiveInlining )]
            public void NotNull([DoesNotReturnIf( false )] bool isValid) {
                if (!isValid) throw Exceptions.Factory.GetException<ArgumentNullException>( this.Message );
            }

            [MethodImpl( MethodImplOptions.AggressiveInlining )]
            public void InRange([DoesNotReturnIf( false )] bool isValid) {
                if (!isValid) throw Exceptions.Factory.GetException<ArgumentOutOfRangeException>( this.Message );
            }

            public override string? ToString() {
                return Exceptions.Factory.GetMessageStringDelegate( this.Message );
            }

        }
        // Operation
        public readonly struct Operation : IAssertion {

            public FormattableString? Message { get; }

            public Operation(FormattableString? message) {
                this.Message = message;
            }

            [MethodImpl( MethodImplOptions.AggressiveInlining )]
            public void Valid([DoesNotReturnIf( false )] bool isValid) {
                if (!isValid) throw Exceptions.Factory.GetException<InvalidOperationException>( this.Message );
            }

            [MethodImpl( MethodImplOptions.AggressiveInlining )]
            public void Ready([DoesNotReturnIf( false )] bool isValid) {
                if (!isValid) throw Exceptions.Factory.GetException<ObjectNotReadyException>( this.Message );
            }

            [MethodImpl( MethodImplOptions.AggressiveInlining )]
            public void NotDisposed([DoesNotReturnIf( false )] bool isValid) {
                if (!isValid) throw Exceptions.Factory.GetException<ObjectDisposedException>( this.Message );
            }

            public override string? ToString() {
                return Exceptions.Factory.GetMessageStringDelegate( this.Message );
            }

        }
    }
}
