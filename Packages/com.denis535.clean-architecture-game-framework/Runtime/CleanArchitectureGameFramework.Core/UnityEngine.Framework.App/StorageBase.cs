#nullable enable
namespace UnityEngine.Framework.App {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class StorageBase : Disposable {

        // Constructor
        public StorageBase() {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
