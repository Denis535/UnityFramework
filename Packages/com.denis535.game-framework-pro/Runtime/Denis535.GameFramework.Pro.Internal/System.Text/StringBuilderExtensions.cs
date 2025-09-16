#nullable enable
namespace System.Text {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static class StringBuilderExtensions {

        private const int IndentSize = 4;

        public static StringBuilder AppendIndent(this StringBuilder builder, int indent) {
            return builder.Append( ' ', indent * IndentSize );
        }

        public static StringBuilder AppendLineFormat(this StringBuilder builder, string format, params object?[] args) {
            return builder.AppendFormat( format, args ).AppendLine();
        }

        public static StringBuilder AppendHierarchy<T>(this StringBuilder builder, T @object, Func<T, string?> textSelector, Func<T, IEnumerable<T>?> childrenSelector) {
            return builder.AppendHierarchy( 0, @object, textSelector, childrenSelector );
        }
        private static StringBuilder AppendHierarchy<T>(this StringBuilder builder, int indent, T @object, Func<T, string?> textSelector, Func<T, IEnumerable<T>?> childrenSelector) {
            var text = textSelector( @object );
            var children = childrenSelector( @object );
            _ = builder.AppendIndent( indent ).Append( text );
            if (children != null) {
                foreach (var child in children) {
                    _ = builder.AppendLine();
                    _ = builder.AppendHierarchy( indent + 1, child, textSelector, childrenSelector );
                }
            }
            return builder;
        }

    }
}
