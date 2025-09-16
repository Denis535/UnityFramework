#nullable enable
namespace GameFramework.Pro {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.Framework;

    public class AboutPackageWindow : EditorWindow {

        // Constructor
        public AboutPackageWindow() {
            this.titleContent = new GUIContent( "About Game Framework Pro package" );
            this.minSize = this.maxSize = new Vector2( 1200, 800 );
        }

        // OnEnable
        public void OnEnable() {
            this.ShowUtility();
            this.Focus();
        }
        public void OnDisable() {
        }

        // OnGUI
        public void OnGUI() {
            HelpBox.Draw();
        }

    }
}
