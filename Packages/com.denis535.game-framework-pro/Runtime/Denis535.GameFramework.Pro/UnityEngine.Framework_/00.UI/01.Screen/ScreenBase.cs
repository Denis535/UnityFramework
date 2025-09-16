#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.TreeMachine.Pro;
    using UnityEngine;
    using UnityEngine.UIElements;

    public abstract class ScreenBase : DisposableBase {

        // Machine
        protected internal TreeMachine<Node2<WidgetBase>, ScreenBase> Machine { get; }

        // Document
        protected internal UIDocument Document { get; }
        // AudioSource
        protected internal AudioSource AudioSource { get; }

        // Constructor
        public ScreenBase(UIDocument document, AudioSource audioSource) {
            this.Machine = new TreeMachine<Node2<WidgetBase>, ScreenBase>( this );
            this.Document = document;
            this.AudioSource = audioSource;
        }
        public override void Dispose() {
            Assert.Operation.Message( $"Screen {this} must have no {this.Machine.Root} root" ).Valid( this.Machine.Root == null );
            Assert.Operation.Message( $"Screen/Document {this} must be released" ).Valid( !this.Document || this.Document.rootVisualElement == null || this.Document.rootVisualElement.childCount == 0 );
            Assert.Operation.Message( $"Screen/AudioSource {this} must be released" ).Valid( !this.AudioSource || this.AudioSource.clip == null );
            base.Dispose();
        }

    }
}
