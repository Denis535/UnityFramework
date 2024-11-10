#nullable enable
namespace UnityEditor.ColorfulProjectWindow {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using UnityEditor;
    using UnityEngine;

    public abstract class ProjectWindowBase : IDisposable {

        // Constructor
        public ProjectWindowBase() {
            EditorApplication.projectWindowItemOnGUI += OnGUI;
        }
        public virtual void Dispose() {
            EditorApplication.projectWindowItemOnGUI -= OnGUI;
        }

        // OnGUI
        private void OnGUI(string guid, Rect rect) {
            DrawElement( rect, AssetDatabase.GUIDToAssetPath( guid ) );
        }

        // DrawElement
        protected virtual void DrawElement(Rect rect, string path) {
            if (IsAssembly( path, out var assembly, out var content )) {
                DrawAssembly( rect, path, assembly, content );
            } else
            if (IsPackage( path, out var package, out content )) {
                DrawPackage( rect, path, package, content );
            }
        }
        protected abstract void DrawPackage(Rect rect, string path, string package, string content);
        protected abstract void DrawAssembly(Rect rect, string path, string assembly, string content);

        // IsPackage
        protected abstract bool IsPackage(string path, [NotNullWhen( true )] out string? package, [NotNullWhen( true )] out string? content);
        protected abstract bool IsAssembly(string path, [NotNullWhen( true )] out string? assembly, [NotNullWhen( true )] out string? content);

        // Helpers
        protected static Color HSVA(int h, float s, float v, float a) {
            var color = Color.HSVToRGB( h / 360f, s, v );
            color.a = a;
            return color;
        }
        protected static Color Lighten(Color color, float factor) {
            Color.RGBToHSV( color, out var h, out var s, out var v );
            var result = Color.HSVToRGB( h, s, v * factor );
            result.a = color.a;
            return result;
        }
        protected static Color Darken(Color color, float factor) {
            Color.RGBToHSV( color, out var h, out var s, out var v );
            var result = Color.HSVToRGB( h, s, v / factor );
            result.a = color.a;
            return result;
        }
        protected static void DrawRect(Rect rect, Color color) {
            var prev = GUI.color;
            GUI.color = color;
            GUI.DrawTexture( rect, Texture2D.whiteTexture );
            GUI.color = prev;
        }

    }
}
