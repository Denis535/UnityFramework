#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class PlayListBase2 : PlayListBase {

        // System
        protected IDependencyProvider Provider { get; }

        // Constructor
        public PlayListBase2(IDependencyProvider provider) {
            this.Provider = provider;
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
