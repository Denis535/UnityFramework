#nullable enable
namespace UnityEditor.ColorfulProjectWindow {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Text;
    using UnityEditor;
    using UnityEngine;

    public abstract class ProjectWindowBase2 : ProjectWindowBase {

        // PackagePaths
        protected string[] PackagePaths { get; }
        // AssemblyPaths
        protected string[] AssemblyPaths { get; }

        // Constructor
        public ProjectWindowBase2() {
            PackagePaths = AssetDatabase.GetAllAssetPaths()
                .Where( i => Path.GetFileName( i ) is "package.json" )
                .Select( Path.GetDirectoryName )
                .Select( i => i.Replace( '\\', '/' ) )
                .OrderByDescending( i => i.StartsWith( "Assets/" ) )
                .Distinct()
                .ToArray();
            AssemblyPaths = AssetDatabase.GetAllAssetPaths()
                    .Where( i => Path.GetExtension( i ) is ".asmdef" or ".asmref" )
                    .Select( Path.GetDirectoryName )
                    .Select( i => i.Replace( '\\', '/' ) )
                    .OrderByDescending( i => i.StartsWith( "Assets/" ) )
                    .Distinct()
                    .ToArray();
        }
        public ProjectWindowBase2(string[] packagePaths, string[] assemblyPaths) {
            PackagePaths = packagePaths;
            AssemblyPaths = assemblyPaths;
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnGUI
        protected override void OnGUI(string guid, Rect rect) {
            base.OnGUI( guid, rect );
        }

        // DrawElement
        protected override void DrawElement(Rect rect, string path) {
            base.DrawElement( rect, path );
        }
        protected override void DrawPackageElement(Rect rect, string path, string package, string content) {
            base.DrawPackageElement( rect, path, package, content );
        }
        protected override void DrawAssemblyElement(Rect rect, string path, string assembly, string content) {
            base.DrawAssemblyElement( rect, path, assembly, content );
        }

        // DrawPackage
        protected override void DrawPackage(Rect rect, string path, string package) {
            base.DrawPackage( rect, path, package );
        }
        protected override void DrawAssembly(Rect rect, string path, string assembly) {
            base.DrawAssembly( rect, path, assembly );
        }
        protected override void DrawAssets(Rect rect, string path, string assembly, string content) {
            base.DrawAssets( rect, path, assembly, content );
        }
        protected override void DrawResources(Rect rect, string path, string assembly, string content) {
            base.DrawResources( rect, path, assembly, content );
        }
        protected override void DrawSources(Rect rect, string path, string assembly, string content) {
            base.DrawSources( rect, path, assembly, content );
        }

        // IsPackage
        protected override bool IsPackage(string path, [NotNullWhen( true )] out string? package, [NotNullWhen( true )] out string? content) {
            var packagePath = PackagePaths.FirstOrDefault( i => path.Equals( i ) || path.StartsWith( i + '/' ) );
            if (packagePath != null) {
                package = Path.GetFileName( packagePath );
                content = path.Substring( packagePath.Length ).TrimStart( '/' );
                return true;
            }
            package = null;
            content = null;
            return false;
        }
        protected override bool IsAssembly(string path, [NotNullWhen( true )] out string? assembly, [NotNullWhen( true )] out string? content) {
            var assemblyPath = AssemblyPaths.FirstOrDefault( i => path.Equals( i ) || path.StartsWith( i + '/' ) );
            if (assemblyPath != null) {
                assembly = Path.GetFileName( assemblyPath );
                content = path.Substring( assemblyPath.Length ).TrimStart( '/' );
                return true;
            }
            assembly = null;
            content = null;
            return false;
        }
        protected override bool IsAssets(string path, string assembly, string content) {
            return base.IsAssets( path, assembly, content );
        }
        protected override bool IsResources(string path, string assembly, string content) {
            return base.IsResources( path, assembly, content );
        }
        protected override bool IsSources(string path, string assembly, string content) {
            return base.IsSources( path, assembly, content );
        }

    }
}
