#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.StateMachine.Pro;
    using UnityEngine;

    public abstract class ThemeBase : DisposableBase {

        // Machine
        protected internal StateMachine<ThemeBase> Machine { get; }

        // AudioSource
        protected internal AudioSource AudioSource { get; }

        // Constructor
        public ThemeBase(AudioSource audioSource) {
            this.Machine = new StateMachine<ThemeBase>( this );
            this.AudioSource = audioSource;
        }
        public override void Dispose() {
            Assert.Operation.Message( $"Theme {this} must have no {this.Machine.Root} root" ).Valid( this.Machine.Root == null );
            Assert.Operation.Message( $"Theme/AudioSource {this} must be released" ).Valid( !this.AudioSource || this.AudioSource.clip == null );
            base.Dispose();
        }

    }
}
