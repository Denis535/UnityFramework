#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class PlayerBase2 : PlayerBase {

        // System
        protected IDependencyProvider Provider { get; }

        // Constructor
        public PlayerBase2(IDependencyProvider provider) {
            this.Provider = provider;
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
