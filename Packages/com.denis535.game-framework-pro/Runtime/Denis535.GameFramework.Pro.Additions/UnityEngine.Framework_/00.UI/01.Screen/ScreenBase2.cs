#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;

    public abstract class ScreenBase2<TRouter, TApplication> : ScreenBase
        where TRouter : notnull, RouterBase
        where TApplication : notnull, ApplicationBase {

        // System
        protected IDependencyProvider Provider { get; }
        // Framework
        protected TRouter Router { get; }
        protected TApplication Application { get; }

        // Constructor
        public ScreenBase2(IDependencyProvider provider, UIDocument document, AudioSource audioSource) : base( document, audioSource ) {
            this.Provider = provider;
            this.Router = this.Provider.RequireDependency<TRouter>();
            this.Application = this.Provider.RequireDependency<TApplication>();
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
