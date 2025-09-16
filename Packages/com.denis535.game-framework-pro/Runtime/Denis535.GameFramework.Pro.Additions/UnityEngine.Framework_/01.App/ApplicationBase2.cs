#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class ApplicationBase2 : ApplicationBase {

        // System
        protected IDependencyContainer Container { get; }

        // Constructor
        public ApplicationBase2(IDependencyContainer container) {
            this.Container = container;
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
