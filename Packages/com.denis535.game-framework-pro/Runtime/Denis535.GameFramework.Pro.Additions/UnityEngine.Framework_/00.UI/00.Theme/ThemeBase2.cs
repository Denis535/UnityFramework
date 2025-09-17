#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class ThemeBase2<TRouter, TApplication> : ThemeBase
        where TRouter : notnull, RouterBase
        where TApplication : notnull, ApplicationBase {

        // System
        protected IDependencyProvider Provider { get; }
        // Framework
        protected TRouter Router { get; }
        protected TApplication Application { get; }

        // Constructor
        public ThemeBase2(IDependencyProvider provider, AudioSource audioSource) : base( audioSource ) {
            this.Provider = provider;
            this.Router = this.Provider.RequireDependency<TRouter>();
            this.Application = this.Provider.RequireDependency<TApplication>();
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
