#nullable enable
namespace UnityEngine.UIElements {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class ViewAttachEvent : EventBase<ViewAttachEvent> {

        public UIViewBase? View { get; private set; }

        public ViewAttachEvent() {
            tricklesDown = true;
            bubbles = true;
        }

        protected override void Init() {
            base.Init();
            tricklesDown = true;
            bubbles = true;
        }
        protected override void PreDispatch(IPanel panel) {
            base.PreDispatch( panel );
        }
        protected override void PostDispatch(IPanel panel) {
            base.PostDispatch( panel );
            View = null;
        }

        public static void Dispatch(VisualElement target, UIViewBase view) {
            Assert.Operation.Message( $"Target {target} must be attached" ).Valid( target.IsAttached() );
            using (var evt = GetPooled()) {
                evt.target = target;
                evt.View = view;
                target.SendEventImmediate( evt );
            }
        }

    }
    public class ViewDetachEvent : EventBase<ViewDetachEvent> {

        public UIViewBase? View { get; private set; }

        public ViewDetachEvent() {
            tricklesDown = true;
            bubbles = true;
        }

        protected override void Init() {
            base.Init();
            tricklesDown = true;
            bubbles = true;
        }
        protected override void PreDispatch(IPanel panel) {
            base.PreDispatch( panel );
        }
        protected override void PostDispatch(IPanel panel) {
            base.PostDispatch( panel );
            View = null;
        }

        public static void Dispatch(VisualElement target, UIViewBase view) {
            Assert.Operation.Message( $"Target {target} must be attached" ).Valid( target.IsAttached() );
            using (var evt = GetPooled()) {
                evt.target = target;
                evt.View = view;
                target.SendEventImmediate( evt );
            }
        }

    }
}
