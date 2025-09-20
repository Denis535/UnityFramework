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

    public abstract class ProgramBase2 : ProgramBase, IDependencyProvider {

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
        Option<object?> IDependencyProvider.GetValue(Type type, object? argument) {
            return this.GetValue( type, argument );
        }
        protected abstract Option<object?> GetValue(Type type, object? argument);

    }
    public abstract partial class ProgramBase2<TTheme, TScreen, TRouter, TApplication, TGame> : ProgramBase2
        where TTheme : notnull, ThemeBase
        where TScreen : notnull, ScreenBase
        where TRouter : notnull, RouterBase
        where TApplication : notnull, ApplicationBase
        where TGame : notnull, GameBase {

        // Framework
        protected abstract TTheme Theme { get; }
        protected abstract TScreen Screen { get; }
        protected abstract TRouter Router { get; }
        protected abstract TApplication Application { get; }
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
            this.ThrowIfInvalid();
            if (typeof( TTheme ).IsAssignableFrom( type )) {
                Assert.Operation.Message( $"Theme must be non-null" ).Valid( this.Theme != null );
                Assert.Operation.Message( $"Theme must be non-disposed" ).NotDisposed( !this.Theme.IsDisposed );
                return Option.Create<object?>( this.Theme );
            }
            if (typeof( TScreen ).IsAssignableFrom( type )) {
                Assert.Operation.Message( $"Screen must be non-null" ).Valid( this.Screen != null );
                Assert.Operation.Message( $"Screen must be non-disposed" ).NotDisposed( !this.Screen.IsDisposed );
                return Option.Create<object?>( this.Screen );
            }
            if (typeof( TRouter ).IsAssignableFrom( type )) {
                Assert.Operation.Message( $"Router must be non-null" ).Valid( this.Router != null );
                Assert.Operation.Message( $"Router must be non-disposed" ).NotDisposed( !this.Router.IsDisposed );
                return Option.Create<object?>( this.Router );
            }
            if (typeof( TApplication ).IsAssignableFrom( type )) {
                Assert.Operation.Message( $"Application must be non-null" ).Valid( this.Application != null );
                Assert.Operation.Message( $"Application must be non-disposed" ).NotDisposed( !this.Application.IsDisposed );
                return Option.Create<object?>( this.Application );
            }
            if (typeof( TGame ).IsAssignableFrom( type )) {
                Assert.Operation.Message( $"Game must be non-null" ).Valid( this.Game != null );
                Assert.Operation.Message( $"Game must be non-disposed" ).NotDisposed( !this.Game.IsDisposed );
                return Option.Create<object?>( this.Game );
            }
            return default;
        }

#if UNITY_EDITOR
        // OnInspectorGUI
        protected internal override void OnInspectorGUI() {
            if (this.didAwake && this) {
                this.OnInspectorGUI( this.Theme );
                this.OnInspectorGUI( this.Screen );
                this.OnInspectorGUI( this.Router );
                this.OnInspectorGUI( this.Application );
                this.OnInspectorGUI( this.Game );
            } else {
                HelpBox.Draw();
            }
        }
        protected virtual void OnInspectorGUI(ThemeBase theme) {
            using (new GUILayout.VerticalScope( EditorStyles.helpBox )) {
                LabelField( "Theme", theme.ToString() );
                LabelField( "PlayList", theme.Machine.Root?.PlayList().Pipe( GetDisplayString ) ?? "Null" );
            }
        }
        protected virtual void OnInspectorGUI(ScreenBase screen) {
            using (new GUILayout.VerticalScope( EditorStyles.helpBox )) {
                LabelField( "Screen", screen.ToString() );
                LabelField( "Widget", screen.Machine.Root?.Widget().Pipe( GetDisplayString ) ?? "Null" );
            }
        }
        protected virtual void OnInspectorGUI(RouterBase router) {
            using (new GUILayout.VerticalScope( EditorStyles.helpBox )) {
                LabelField( "Router", router.ToString() );
            }
        }
        protected virtual void OnInspectorGUI(ApplicationBase application) {
            using (new GUILayout.VerticalScope( EditorStyles.helpBox )) {
                LabelField( "Application", application.ToString() );
            }
        }
        protected virtual void OnInspectorGUI(GameBase? game) {
            using (new GUILayout.VerticalScope( EditorStyles.helpBox )) {
                LabelField( "Game", game?.ToString() ?? "Null" );
            }
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
            _ = builder.AppendHierarchy( widget, i => i.ToString(), i => i.NodeMutable.Children.Select( i => i.Widget() ) );
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
