#nullable enable
namespace System {
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public static class Assert {
        // Argument
        public static class Argument {
            public static Assertions.Argument Message(FormattableString? value) {
                return new Assertions.Argument( value );
            }
        }
        // Operation
        public static class Operation {
            public static Assertions.Operation Message(FormattableString? value) {
                return new Assertions.Operation( value );
            }
        }
    }
}
