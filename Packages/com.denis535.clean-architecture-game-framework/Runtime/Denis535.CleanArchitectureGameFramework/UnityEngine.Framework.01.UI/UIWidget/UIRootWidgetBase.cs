#nullable enable
namespace UnityEngine.Framework.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UIElements;

    public abstract class UIRootWidgetBase<TView> : UIWidgetBase2<TView> where TView : UIRootWidgetViewBase {

        // Constructor
        public UIRootWidgetBase(IDependencyContainer container) : base( container ) {
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnActivate
        protected override void OnActivate(object? argument) {
        }
        protected override void OnDeactivate(object? argument) {
        }

        // OnDescendantActivate
        protected override void OnBeforeDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnBeforeDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }

        // Helpers
        protected static void OnSubmit(NavigationSubmitEvent evt) {
            var button = evt.target as Button;
            if (button != null) {
                Click( button );
                evt.StopPropagation();
            }
        }
        protected static void OnCancel(NavigationCancelEvent evt) {
            var widget = ((VisualElement) evt.target).AncestorsAndSelf().Where( i => i.enabledInHierarchy && i.resolvedStyle.display == DisplayStyle.Flex ).LastOrDefault( IsWidget );
            var button = widget?.Query<Button>().Where( i => i.enabledInHierarchy && i.resolvedStyle.display == DisplayStyle.Flex ).Where( IsCancel ).First();
            if (button != null) {
                Click( button );
                evt.StopPropagation();
            }
        }
        protected static bool IsWidget(VisualElement element) {
            return element.GetClasses().Any( i => i.Contains( "widget" ) );
        }
        protected static bool IsCancel(Button button) {
            return button.ClassListContains( "resume" ) ||
                button.ClassListContains( "cancel" ) ||
                button.ClassListContains( "back" ) ||
                button.ClassListContains( "no" ) ||
                button.ClassListContains( "exit" ) ||
                button.ClassListContains( "quit" ) ||
                button.ClassListContains( "close" );
        }
        protected static void Click(Button button) {
            using (var evt = ClickEvent.GetPooled()) {
                evt.target = button;
                button.SendEvent( evt );
            }
        }

    }
}