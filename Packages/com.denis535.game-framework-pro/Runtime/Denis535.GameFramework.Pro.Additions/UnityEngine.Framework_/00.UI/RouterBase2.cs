#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class RouterBase2<TTheme, TScreen, TApplication> : RouterBase
        where TTheme : notnull, ThemeBase
        where TScreen : notnull, ScreenBase
        where TApplication : notnull, ApplicationBase {

        // System
        protected IDependencyContainer Container { get; }
        // Framework
        protected TTheme Theme => this.Container.RequireDependency<TTheme>();
        protected TScreen Screen => this.Container.RequireDependency<TScreen>();
        protected TApplication Application => this.Container.RequireDependency<TApplication>();

        // Constructor
        public RouterBase2(IDependencyContainer container) {
            this.Container = container;
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
