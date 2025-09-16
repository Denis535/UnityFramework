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
        protected IDependencyContainer Container { get; }
        // Framework
        protected TRouter Router => this.Container.RequireDependency<TRouter>();
        protected TApplication Application => this.Container.RequireDependency<TApplication>();

        // Constructor
        public ThemeBase2(IDependencyContainer container, AudioSource audioSource) : base( audioSource ) {
            this.Container = container;
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
