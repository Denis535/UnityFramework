#nullable enable
namespace UnityEditor.ColorfulProjectWindow {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UIElements;

    public class SettingsProvider : UnityEditor.SettingsProvider {

        // Settings
        private Settings Settings => Settings.Instance;

        // GetSettingsProvider
        [SettingsProvider]
        public static UnityEditor.SettingsProvider? GetSettingsProvider() {
            return new SettingsProvider();
        }

        // Constructor
        public SettingsProvider() : base( "Preferences/Colorful Project Window", SettingsScope.User, new[] { "Colorful Project Window" } ) {
        }

        // OnActivate
        public override void OnActivate(string searchContext, VisualElement rootElement) {
        }
        public override void OnDeactivate() {
        }

        // OnGUI
        public override void OnTitleBarGUI() {
        }
        public override void OnGUI(string searchContext) {
            using (var scope = new EditorGUI.ChangeCheckScope()) {
                using (new EditorGUILayout.VerticalScope( GUI.skin.box )) {
                    this.Settings.PackageColor = EditorGUILayout.ColorField( "Package Color", this.Settings.PackageColor );
                    this.Settings.AssemblyColor = EditorGUILayout.ColorField( "Assembly Color", this.Settings.AssemblyColor );
                    this.Settings.AssetsColor = EditorGUILayout.ColorField( "Assets Color", this.Settings.AssetsColor );
                    this.Settings.ResourcesColor = EditorGUILayout.ColorField( "Resources Color", this.Settings.ResourcesColor );
                    this.Settings.SourcesColor = EditorGUILayout.ColorField( "Sources Color", this.Settings.SourcesColor );
                    if (GUILayout.Button( "Reset", GUILayout.ExpandWidth( false ) )) {
                        this.Settings.Reset();
                    }
                }
                if (scope.changed) {
                    var window = (EditorWindow?) Resources.FindObjectsOfTypeAll( Type.GetType( "UnityEditor.ProjectBrowser, UnityEditor.CoreModule" ) ).FirstOrDefault();
                    window?.Repaint();
                    this.Settings.Save();
                }
            }
        }
        public override void OnFooterBarGUI() {
        }

        // OnUpdate
        public override void OnInspectorUpdate() {
        }

    }
}
