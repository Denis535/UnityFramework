#nullable enable
namespace UnityEngine.UIElements {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public static class VisualTreeAssetExtensions {

        public static T Instantiate<T>(this VisualTreeAsset asset) where T : notnull, VisualElement, new() {
            var view = asset.Instantiate().Children().OfType<T>().FirstOrDefault();
            Assert.Operation.Message( $"VisualTreeAsset {asset.name} ({typeof( T )}) can not be instantiated" ).Valid( view != null );
            return view;
        }

    }
}
