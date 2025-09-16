#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class GameBase : DisposableBase {

        // Constructor
        public GameBase() {
        }
        public override void Dispose() {
            Assert.Operation.Message( $"Game {this} must be non-disposed" ).NotDisposed( !this.IsDisposed );
            base.Dispose();
        }

    }
}
