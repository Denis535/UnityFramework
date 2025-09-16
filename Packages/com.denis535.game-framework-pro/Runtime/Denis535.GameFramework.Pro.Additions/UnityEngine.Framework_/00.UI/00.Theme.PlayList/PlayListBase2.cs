#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class PlayListBase2 : PlayListBase {

        // System
        protected IDependencyContainer Container { get; }

        // Constructor
        public PlayListBase2(IDependencyContainer container) {
            this.Container = container;
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
