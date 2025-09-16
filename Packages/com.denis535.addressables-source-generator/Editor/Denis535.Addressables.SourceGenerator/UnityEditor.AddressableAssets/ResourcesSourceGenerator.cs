#nullable enable
namespace UnityEditor.AddressableAssets {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using UnityEditor;
    using UnityEditor.AddressableAssets.Settings;
    using UnityEngine;

    public class ResourcesSourceGenerator {

        // Generate
        public virtual void Generate(string path, string @namespace, string @class, AddressableAssetSettings settings) {
            var addresses = GetEntries( settings ).Where( this.IsSupported ).Select( i => (GetPath( i ), i.address) ).ToArray();
            this.Generate( path, @namespace, @class, addresses );
            static List<AddressableAssetEntry> GetEntries(AddressableAssetSettings settings) {
                var entries = new List<AddressableAssetEntry>();
                settings.GetAllAssets( entries, true );
                return entries;
            }
            static string[] GetPath(AddressableAssetEntry entry) {
                if (IsAsset( entry )) {
                    if (IsMainAsset( entry )) {
                        var path = GetAddressWithoutExtension( entry ).Split( '/', '\\', '.' );
                        var dir = path.SkipLast( 1 );
                        var name = path.Last();
                        if (name.Contains( " #" )) name = name.Substring( 0, name.IndexOf( " #" ) );
                        return dir.Append( name ).ToArray();
                    } else {
                        var path = GetPath( entry.ParentEntry );
                        var dir = path.SkipLast( 1 );
                        var name = path.Last();
                        return dir.Append( name ).Append( entry.TargetAsset.name ).ToArray();
                    }
                } else {
                    throw new NotSupportedException( $"Entry {entry} is not supported" );
                }
            }
        }
        public virtual void Generate(string path, string @namespace, string @class, (string[] Path, string Value)[] addresses) {
            var builder = new StringBuilder();
            builder.AppendCompilationUnit( @namespace, @class, addresses );
            WriteText( path, builder.ToString() );
        }

        // IsSupported
        protected virtual bool IsSupported(AddressableAssetEntry entry) {
            return entry.MainAssetType != typeof( DefaultAsset );
        }

        // Helpers
        private static bool IsAsset(AddressableAssetEntry entry) {
            return !entry.IsFolder;
        }
        private static bool IsMainAsset(AddressableAssetEntry entry) {
            if (!entry.IsFolder) {
                return entry.ParentEntry == null || entry.ParentEntry.IsFolder;
            }
            return false;
        }
        private static bool IsSubAsset(AddressableAssetEntry entry) {
            if (!entry.IsFolder) {
                return entry.ParentEntry != null && !entry.ParentEntry.IsFolder;
            }
            return false;
        }
        private static bool IsFolder(AddressableAssetEntry entry) {
            return entry.IsFolder;
        }
        private static string GetAddressWithoutExtension(AddressableAssetEntry entry) {
            if (Path.GetExtension( entry.address ) == Path.GetExtension( entry.AssetPath )) {
                return Path.ChangeExtension( entry.address, null );
            }
            return entry.address;
        }
        // Helpers
        private static void WriteText(string path, string text) {
            if (!File.Exists( path ) || File.ReadAllText( path ) != text) {
                File.WriteAllText( path, text );
                AssetDatabase.ImportAsset( path, ImportAssetOptions.Default );
            }
        }

    }
}
