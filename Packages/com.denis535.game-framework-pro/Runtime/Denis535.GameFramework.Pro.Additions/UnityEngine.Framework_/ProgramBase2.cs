#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UIElements;

    public abstract class ProgramBase2 : ProgramBase, IDependencyContainer {

        // Awake
        protected override void Awake() {
            base.Awake();
        }
        protected override void OnDestroy() {
            base.OnDestroy();
        }

        // Start
        protected override void Start() {
        }
        protected override void FixedUpdate() {
        }
        protected override void Update() {
        }
        protected override void LateUpdate() {
        }

        // OnQuit
        protected override bool OnQuit() {
            return true;
        }

        // GetValue
        Option<object?> IDependencyContainer.GetValue(Type type, object? argument) {
            return this.GetValue( type, argument );
        }
        protected virtual Option<object?> GetValue(Type type, object? argument) {
            return default;
        }

    }
    public abstract partial class ProgramBase2<TTheme, TScreen, TRouter, TApplication, TGame> : ProgramBase2
        where TTheme : notnull, ThemeBase
        where TScreen : notnull, ScreenBase
        where TRouter : notnull, RouterBase
        where TApplication : notnull, ApplicationBase
        where TGame : notnull, GameBase {

        // Framework
        protected abstract TTheme Theme { get; set; }
        protected abstract TScreen Screen { get; set; }
        protected abstract TRouter Router { get; set; }
        protected abstract TApplication Application { get; set; }
        protected abstract TGame? Game { get; }

        // Awake
        protected override void Awake() {
            base.Awake();
        }
        protected override void OnDestroy() {
            base.OnDestroy();
        }

        // Start
        protected override void Start() {
        }
        protected override void FixedUpdate() {
        }
        protected override void Update() {
        }
        protected override void LateUpdate() {
        }

        // OnQuit
        protected override bool OnQuit() {
            return true;
        }

        // GetValue
        protected override Option<object?> GetValue(Type type, object? argument) {
            return base.GetValue( type, argument );
        }

#if UNITY_EDITOR
        // OnInspectorGUI
        protected internal override void OnInspectorGUI() {
            if (this.didAwake && this) {
                this.OnInspectorGUI( this.Theme, this.Screen, this.Router, this.Application, this.Game );
            } else {
                HelpBox.Draw();
            }
        }
        protected virtual void OnInspectorGUI(ThemeBase theme, ScreenBase screen, RouterBase router, ApplicationBase application, GameBase? game) {
            LabelField( "Theme", theme.ToString() );
            LabelField( "PlayList", theme.Machine.Root?.PlayList().Pipe( GetDisplayString ) ?? "Null" );
            GUILayout.Space( 2 );
            LabelField( "Screen", screen.ToString() );
            LabelField( "Widget", screen.Machine.Root?.Widget().Pipe( GetDisplayString ) ?? "Null" );
            GUILayout.Space( 2 );
            LabelField( "Router", router.ToString() );
            LabelField( "Application", application.ToString() );
            LabelField( "Game", game?.ToString() ?? "Null" );
        }

        // Helpers
        protected static void LabelField(string label, string? text) {
            using (new EditorGUILayout.HorizontalScope()) {
                EditorGUILayout.PrefixLabel( label );
                EditorGUI.SelectableLabel( GUILayoutUtility.GetRect( new GUIContent( text ), GUI.skin.textField ), text, GUI.skin.textField );
            }
        }
        // Helpers
        protected static string? GetDisplayString(PlayListBase playList) {
            return playList.ToString();
        }
        protected static string? GetDisplayString(WidgetBase widget) {
            var builder = new StringBuilder();
            _ = builder.AppendHierarchy( widget, i => i.ToString(), i => i.Node.Children.Select( i => i.Widget() ) );
            return builder.ToString();
        }
        protected static string? GetDisplayString(ViewBase view) {
            var builder = new StringBuilder();
            _ = builder.AppendHierarchy( (VisualElement) view, i => $"{i.GetType().FullName} ({i.name})", i => i.Children() );
            return builder.ToString();
        }
#endif

    }
}
