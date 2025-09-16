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
        protected IDependencyContainer Container { get; }
        // Framework
        protected TRouter Router => this.Container.RequireDependency<TRouter>();
        protected TApplication Application => this.Container.RequireDependency<TApplication>();

        // Constructor
        public ScreenBase2(IDependencyContainer container, UIDocument document, AudioSource audioSource) : base( document, audioSource ) {
            this.Container = container;
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
