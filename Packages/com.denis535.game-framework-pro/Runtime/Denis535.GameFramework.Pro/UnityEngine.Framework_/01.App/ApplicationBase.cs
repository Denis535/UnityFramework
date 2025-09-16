#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class ApplicationBase : DisposableBase {

        // Constructor
        public ApplicationBase() {
        }
        public override void Dispose() {
            Assert.Operation.Message( $"Application {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            base.Dispose();
        }

    }
}
