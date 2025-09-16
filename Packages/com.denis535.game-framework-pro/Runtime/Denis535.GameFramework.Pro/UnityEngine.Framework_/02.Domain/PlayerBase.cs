#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class PlayerBase : DisposableBase {

        // Constructor
        public PlayerBase() {
        }
        public override void Dispose() {
            Assert.Operation.Message( $"Player {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            base.Dispose();
        }

    }
}
