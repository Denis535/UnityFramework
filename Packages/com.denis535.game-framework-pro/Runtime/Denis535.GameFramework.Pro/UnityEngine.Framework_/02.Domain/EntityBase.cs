#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [DefaultExecutionOrder( ExecutionOrder )]
    public abstract class EntityBase : MonoBehaviour {

        public const int ExecutionOrder = 100;

        // Awake
        protected abstract void Awake();
        protected abstract void OnDestroy();

        // Start
        //protected abstract void Start();
        //protected abstract void FixedUpdate();
        //protected abstract void Update();
        //protected abstract void LateUpdate();

    }
    // CharacterBase
    public abstract class CharacterBase : EntityBase {
    }
    // MachineBase
    public abstract class MachineBase : EntityBase {
    }
    // InteractiveBase
    public abstract class InteractiveBase : EntityBase {
    }
    // CameraBase
    public abstract class CameraBase : EntityBase {
    }
    // WorldBase
    public abstract class WorldBase : EntityBase {
    }
}
