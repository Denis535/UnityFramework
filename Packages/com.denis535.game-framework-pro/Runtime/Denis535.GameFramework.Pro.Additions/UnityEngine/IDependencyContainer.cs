#nullable enable
namespace UnityEngine {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public interface IDependencyContainer {

        // GetDependency
        public sealed object? GetDependency(Type type, object? argument = null) {
            var option = this.GetValue( type, argument );
            return option.ValueOrDefault;
        }
        public sealed T? GetDependency<T>(object? argument = null) {
            var option = this.GetValue<T>( argument );
            return (T?) option.ValueOrDefault;
        }

        // RequireDependency
        public sealed object? RequireDependency(Type type, object? argument = null) {
            var option = this.GetValue( type, argument );
            Assert.Operation.Message( $"Dependency {type} ({argument}) was not found" ).Valid( option.HasValue );
            return option.Value;
        }
        public sealed T RequireDependency<T>(object? argument = null) {
            var option = this.GetValue<T>( argument );
            Assert.Operation.Message( $"Dependency {typeof( T )} ({argument}) was not found" ).Valid( option.HasValue );
            return (T?) option.Value!;
        }

        // GetValue
        protected Option<object?> GetValue(Type type, object? argument);
        protected sealed Option<object?> GetValue<T>(object? argument) {
            return this.GetValue( typeof( T ), argument );
        }

    }
}
