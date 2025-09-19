#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class GameBase2 : GameBase {

        // System
        protected IDependencyProvider Provider { get; }

        // Constructor
        public GameBase2(IDependencyProvider provider) {
            this.Provider = provider;
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
