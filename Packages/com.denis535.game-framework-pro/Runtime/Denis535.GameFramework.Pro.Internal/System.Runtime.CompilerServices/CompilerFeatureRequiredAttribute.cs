#nullable enable
namespace System.Runtime.CompilerServices {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;

    [EditorBrowsable( EditorBrowsableState.Never )]
    public class CompilerFeatureRequiredAttribute : Attribute {

        public const string RefStructs = nameof( RefStructs );
        public const string RequiredMembers = nameof( RequiredMembers );

        public string FeatureName { get; }

        public CompilerFeatureRequiredAttribute(string featureName) {
            this.FeatureName = featureName;
        }

    }
}
