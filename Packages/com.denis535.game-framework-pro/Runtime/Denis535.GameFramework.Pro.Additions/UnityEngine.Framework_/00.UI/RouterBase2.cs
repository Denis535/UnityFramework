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
        protected IDependencyProvider Provider { get; }
        // Framework
        protected TTheme Theme => this.Provider.RequireDependency<TTheme>();
        protected TScreen Screen => this.Provider.RequireDependency<TScreen>();
        protected TApplication Application { get; }

        // Constructor
        public RouterBase2(IDependencyProvider provider) {
            this.Provider = provider;
            this.Application = this.Provider.RequireDependency<TApplication>();
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
