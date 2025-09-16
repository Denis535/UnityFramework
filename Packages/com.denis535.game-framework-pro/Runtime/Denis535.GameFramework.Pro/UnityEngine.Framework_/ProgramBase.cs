#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;

    [DefaultExecutionOrder( ExecutionOrder )]
    public abstract partial class ProgramBase : MonoBehaviour {

        public const int ExecutionOrder = 10;

        // Awake
        protected virtual void Awake() {
            Application.wantsToQuit += this.OnQuit;
        }
        protected virtual void OnDestroy() {
        }

        // Start
        protected virtual void Start() {
        }
        protected virtual void FixedUpdate() {
        }
        protected virtual void Update() {
        }
        protected virtual void LateUpdate() {
        }

        // OnQuit
        protected virtual bool OnQuit() {
            return true;
        }

#if UNITY_EDITOR
        // OnInspectorGUI
        protected internal virtual void OnInspectorGUI() {
            HelpBox.Draw();
        }
#endif

    }
}
