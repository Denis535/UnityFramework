#nullable enable
namespace System.Text {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    internal static partial class StringBuilderExtensions {

        public static int IndentSize { get; set; } = 4;

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

        public static StringBuilder AppendIndent(this StringBuilder builder, int indent) {
            return builder.Append( ' ', indent * IndentSize );
        }

        public static StringBuilder AppendLineFormat(this StringBuilder builder, string format, params object?[] args) {
            return builder.AppendFormat( format, args ).AppendLine();
        }

    }
    internal static partial class StringBuilderExtensions {

        public static void AppendCompilationUnit(this StringBuilder builder, string @namespace, string @class, (string[] Path, string Value)[] list) {
            builder.AppendCompilationUnit( @namespace, @class, GetTreeList( list ) );
        }
        public static void AppendCompilationUnit(this StringBuilder builder, string @namespace, string @class, KeyValueTreeList<string> treeList) {
            _ = builder.AppendLine( $"namespace {@namespace} {{" );
            {
                builder.AppendClass( 1, @class, treeList.Items.ToArray() );
            }
            _ = builder.AppendLine( "}" );
        }

        private static void AppendClass(this StringBuilder builder, int indent, string name, KeyValueTreeList<string>.Item[] list) {
            _ = builder.AppendIndent( indent ).AppendLine( $"public static class @{GetClassIdentifier( name )} {{" );
            foreach (var item in Sort( list )) {
                if (item is KeyValueTreeList<string>.ValueItem item_value) {
                    builder.AppendConst( indent + 1, item_value.Key, item_value.Value );
                } else
                if (item is KeyValueTreeList<string>.ListItem item_list) {
                    builder.AppendClass( indent + 1, item_list.Key, item_list.Items.ToArray() );
                }
            }
            _ = builder.AppendIndent( indent ).AppendLine( "}" );
        }

        private static void AppendConst(this StringBuilder builder, int indent, string name, string value) {
            _ = builder.AppendIndent( indent ).AppendLine( $"public const string @{GetConstIdentifier( name )} = \"{value}\";" );
        }

        // Helpers
        private static KeyValueTreeList<string> GetTreeList(IEnumerable<(string[] Path, string Value)> list) {
            var treeList = new KeyValueTreeList<string>();
            foreach (var (path, value) in list) {
                treeList.AddValue( path.SkipLast( 1 ), path.Last(), value );
            }
            return treeList;
        }
        private static IEnumerable<KeyValueTreeList<string>.Item> Sort(IEnumerable<KeyValueTreeList<string>.Item> list) {
            return list
                .OrderByDescending( i => i is KeyValueTreeList<string>.ValueItem )

                .ThenByDescending( i => i.Key.Equals( "UnityEngine" ) )
                .ThenByDescending( i => i.Key.Equals( "UnityEditor" ) )

                .ThenByDescending( i => i.Key.Equals( "Resources" ) )
                .ThenByDescending( i => i.Key.Equals( "EditorSceneList" ) )

                .ThenByDescending( i => i.Key.Equals( "Project" ) )
                .ThenByDescending( i => i.Key.Equals( "Presentation" ) )
                .ThenByDescending( i => i.Key.Equals( "UI" ) )
                .ThenByDescending( i => i.Key.Equals( "App" ) )
                .ThenByDescending( i => i.Key.Equals( "Application" ) )
                .ThenByDescending( i => i.Key.Equals( "Domain" ) )
                .ThenByDescending( i => i.Key.Equals( "Game" ) )
                .ThenByDescending( i => i.Key.Equals( "Entities" ) )
                .ThenByDescending( i => i.Key.Equals( "Core" ) )
                .ThenByDescending( i => i.Key.Equals( "Internal" ) )

                .ThenByDescending( i => i.Key.Equals( "Launcher" ) )
                .ThenByDescending( i => i.Key.Equals( "Startup" ) )
                .ThenByDescending( i => i.Key.Equals( "Main" ) )
                .ThenByDescending( i => i.Key.Equals( "Game" ) )
                .ThenByDescending( i => i.Key.Equals( "World" ) )
                .ThenByDescending( i => i.Key.Equals( "Level" ) )

                .ThenByDescending( i => i.Key.Equals( "LauncherScene" ) )
                .ThenByDescending( i => i.Key.Equals( "StartupScene" ) )
                .ThenByDescending( i => i.Key.Equals( "MainScene" ) )
                .ThenByDescending( i => i.Key.Equals( "GameScene" ) )
                .ThenByDescending( i => i.Key.Equals( "WorldScene" ) )
                .ThenByDescending( i => i.Key.Equals( "LevelScene" ) )

                .ThenByDescending( i => i.Key.Equals( "MainScreen" ) )
                .ThenByDescending( i => i.Key.Equals( "GameScreen" ) )
                .ThenByDescending( i => i.Key.Equals( "DebugScreen" ) )

                .ThenByDescending( i => i.Key.Equals( "Actors" ) )
                .ThenByDescending( i => i.Key.Equals( "Things" ) )
                .ThenByDescending( i => i.Key.Equals( "Vehicles" ) )
                .ThenByDescending( i => i.Key.Equals( "Transports" ) )
                .ThenByDescending( i => i.Key.Equals( "Worlds" ) )
                .ThenByDescending( i => i.Key.Equals( "Levels" ) )

                .ThenByDescending( i => i.Key.Equals( "Common" ) )
                .ThenBy( i => i.Key );
        }
        internal static string GetClassIdentifier(string key) {
            key = key.Replace( ' ', '_' ).Replace( '-', '_' ).Replace( '@', '_' );
            key = key.TrimStart( ' ', '-', '_', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' );
            key = key.TrimEnd( ' ', '-', '_' );
            return key;
        }
        internal static string GetConstIdentifier(string key) {
            key = key.Replace( ' ', '_' ).Replace( '-', '_' ).Replace( '@', '_' );
            key = key.TrimStart( ' ', '-', '_', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' );
            key = key.TrimEnd( ' ', '-', '_' );
            return "Value_" + key;
        }

    }
}
