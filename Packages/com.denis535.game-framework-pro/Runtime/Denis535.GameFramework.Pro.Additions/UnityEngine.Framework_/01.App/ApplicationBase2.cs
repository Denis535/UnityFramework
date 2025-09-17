#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class ApplicationBase2 : ApplicationBase {

        // System
        protected IDependencyProvider Provider { get; }

        // Constructor
        public ApplicationBase2(IDependencyProvider provider) {
            this.Provider = provider;
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
