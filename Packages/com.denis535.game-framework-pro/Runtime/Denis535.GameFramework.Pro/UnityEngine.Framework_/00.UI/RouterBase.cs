#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class RouterBase : DisposableBase {

        // Constructor
        public RouterBase() {
        }
        public override void Dispose() {
            Assert.Operation.Message( $"Router {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            base.Dispose();
        }

    }
}
