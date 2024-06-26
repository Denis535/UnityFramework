#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.Framework.App;
    using UnityEngine.Framework.Entities;

    public abstract class ProgramBase2 : ProgramBase, IDependencyContainer {

        // IDependencyContainer
        Option<object?> IDependencyContainer.GetValue(Type type, object? argument) {
            return GetValue( type, argument );
        }
        protected abstract Option<object?> GetValue(Type type, object? argument);

    }
    public abstract class ProgramBase2<TTheme, TScreen, TRouter, TApplication, TGame> : ProgramBase2
        where TTheme : notnull, UIThemeBase
        where TScreen : notnull, UIScreenBase
        where TRouter : notnull, UIRouterBase
        where TApplication : notnull, ApplicationBase
        where TGame : notnull, GameBase {

        // Framework
        protected abstract TTheme Theme { get; set; }
        protected abstract TScreen Screen { get; set; }
        protected abstract TRouter Router { get; set; }
        protected abstract TApplication Application { get; set; }
        protected abstract TGame Game { get; }

#if UNITY_EDITOR
        // OnInspectorGUI
        protected internal override void OnInspectorGUI() {
            OnInspectorGUI( Theme );
            OnInspectorGUI( Screen );
            OnInspectorGUI( Router );
            OnInspectorGUI( Application );
            OnInspectorGUI( Game );
        }
#endif

    }
}
