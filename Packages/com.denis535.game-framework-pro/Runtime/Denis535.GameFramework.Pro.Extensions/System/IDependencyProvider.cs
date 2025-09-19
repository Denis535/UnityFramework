#nullable enable
namespace System {
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface IDependencyProvider {

        // GetDependency
        public sealed T? GetDependency<T>(object? argument = null) {
            var value = this.GetValue( typeof( T ), argument );
            return (T?) value.ValueOrDefault;
        }
        public sealed T? GetDependency<T>(Type type, object? argument = null) {
            var value = this.GetValue( type, argument );
            return (T?) value.ValueOrDefault;
        }

        // RequireDependency
        public sealed T RequireDependency<T>(object? argument = null) {
            var value = this.GetValue( typeof( T ), argument );
            Assert.Operation.Message( $"Dependency {typeof( T )} ({argument}) was not found" ).Valid( value.HasValue );
            return (T) value.Value!;
        }
        public sealed T RequireDependency<T>(Type type, object? argument = null) {
            var value = this.GetValue( type, argument );
            Assert.Operation.Message( $"Dependency {type} ({argument}) was not found" ).Valid( value.HasValue );
            return (T) value.Value!;
        }

        // GetValue
        protected Option<object?> GetValue(Type type, object? argument);

    }
}
