#nullable enable
namespace UnityEngine.Framework.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class EntityBase : MonoBehaviour {

        // Awake
        protected abstract void Awake();
        protected abstract void OnDestroy();

    }
    public abstract class EntityBase<TBody, TView> : EntityBase where TBody : notnull, EntityBodyBase where TView : notnull, EntityViewBase {

        // Body
        protected TBody Body { get; init; } = default!;
        // View
        protected TView View { get; init; } = default!;

    }
    // EntityBodyBase
    public abstract class EntityBodyBase : Disposable {

        // GameObject
        protected GameObject GameObject { get; }
        protected Transform Transform => GameObject.transform;

        // Constructor
        public EntityBodyBase(GameObject gameObject) {
            GameObject = gameObject;
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    // EntityViewBase
    public abstract class EntityViewBase : Disposable {

        // GameObject
        protected GameObject GameObject { get; }
        protected Transform Transform => GameObject.transform;

        // Constructor
        public EntityViewBase(GameObject gameObject) {
            GameObject = gameObject;
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
