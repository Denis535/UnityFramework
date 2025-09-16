#nullable enable
namespace UnityEditor.ColorfulProjectWindow {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Settings {

        private static readonly Color DefaultPackageColor = HSVA( 240, 1.0f, 1.0f, 0.3f );
        private static readonly Color DefaultAssemblyColor = HSVA( 000, 1.0f, 1.0f, 0.3f );
        private static readonly Color DefaultAssetsColor = HSVA( 060, 1.0f, 1.0f, 0.3f );
        private static readonly Color DefaultResourcesColor = HSVA( 060, 1.0f, 1.0f, 0.3f );
        private static readonly Color DefaultSourcesColor = HSVA( 120, 1.0f, 1.0f, 0.3f );
        private static Settings? instance;

        // Instance
        public static Settings Instance => instance ??= new Settings();

        // Colors
        public Color PackageColor { get; internal set; }
        public Color AssemblyColor { get; internal set; }
        public Color AssetsColor { get; internal set; }
        public Color ResourcesColor { get; internal set; }
        public Color SourcesColor { get; internal set; }

        // Constructor
        private Settings() {
            this.Load();
        }

        // Load
        public void Load() {
            this.PackageColor = GetColor( "ColorfulProjectWindow.Settings.PackageColor", DefaultPackageColor );
            this.AssemblyColor = GetColor( "ColorfulProjectWindow.Settings.AssemblyColor", DefaultAssemblyColor );
            this.AssetsColor = GetColor( "ColorfulProjectWindow.Settings.AssetsColor", DefaultAssetsColor );
            this.ResourcesColor = GetColor( "ColorfulProjectWindow.Settings.ResourcesColor", DefaultResourcesColor );
            this.SourcesColor = GetColor( "ColorfulProjectWindow.Settings.SourcesColor", DefaultSourcesColor );
        }

        // Save
        public void Save() {
            SetColor( "ColorfulProjectWindow.Settings.PackageColor", this.PackageColor );
            SetColor( "ColorfulProjectWindow.Settings.AssemblyColor", this.AssemblyColor );
            SetColor( "ColorfulProjectWindow.Settings.AssetsColor", this.AssetsColor );
            SetColor( "ColorfulProjectWindow.Settings.ResourcesColor", this.ResourcesColor );
            SetColor( "ColorfulProjectWindow.Settings.SourcesColor", this.SourcesColor );
        }

        // Reset
        public void Reset() {
            this.PackageColor = DefaultPackageColor;
            this.AssemblyColor = DefaultAssemblyColor;
            this.AssetsColor = DefaultAssetsColor;
            this.ResourcesColor = DefaultResourcesColor;
            this.SourcesColor = DefaultSourcesColor;
        }

        // Helpers
        private static Color GetColor(string key, Color @default) {
            var result = EditorPrefs.GetString( key, ColorUtility.ToHtmlStringRGBA( @default ) );
            if (ColorUtility.TryParseHtmlString( $"#{result}", out var result2 )) {
                return result2;
            }
            return @default;
        }
        private static void SetColor(string key, Color value) {
            EditorPrefs.SetString( key, ColorUtility.ToHtmlStringRGBA( value ) );
        }
        // Helpers
        private static Color HSVA(int h, float s, float v, float a) {
            var color = Color.HSVToRGB( h / 360f, s, v );
            color.a = a;
            return color;
        }

    }
}
