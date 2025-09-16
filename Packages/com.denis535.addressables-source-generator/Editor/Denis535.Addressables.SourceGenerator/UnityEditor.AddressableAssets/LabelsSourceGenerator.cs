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

    public class LabelsSourceGenerator {

        // Generate
        public virtual void Generate(string path, string @namespace, string @class, AddressableAssetSettings settings) {
            var labels = settings.GetLabels().Where( this.IsSupported ).Select( i => (GetPath( i ), i) ).ToArray();
            this.Generate( path, @namespace, @class, labels );
            static string[] GetPath(string label) {
                return label.Split( '/', '\\', '.' );
            }
        }
        public virtual void Generate(string path, string @namespace, string @class, (string[] Path, string Value)[] labels) {
            var builder = new StringBuilder();
            builder.AppendCompilationUnit( @namespace, @class, labels );
            WriteText( path, builder.ToString() );
        }

        // IsSupported
        protected virtual bool IsSupported(string label) {
            return true;
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
