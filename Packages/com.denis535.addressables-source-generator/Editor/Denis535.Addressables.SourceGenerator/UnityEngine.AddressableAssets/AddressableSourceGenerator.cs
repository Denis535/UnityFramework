#nullable enable
namespace UnityEngine.AddressableAssets {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using UnityEditor;
    using UnityEditor.AddressableAssets;
    using UnityEngine;

    [CreateAssetMenu( fileName = "AddressableSourceGenerator", menuName = "Addressables/AddressableSourceGenerator" )]
    public class AddressableSourceGenerator : ScriptableObject {

        // Directory
        private string Directory => Path.GetDirectoryName( AssetDatabase.GetAssetPath( this ) );
        // Path
        public string ResourcesPath => Path.Combine( this.Directory, this.ResourcesClassName + ".cs" );
        public string LabelsPath => Path.Combine( this.Directory, this.LabelsClassName + ".cs" );
        // Namespace
        public string ResourcesClassNamespace => new DirectoryInfo( this.Directory ).Name;
        public string LabelsClassNamespace => new DirectoryInfo( this.Directory ).Name;
        // Name
        public string ResourcesClassName => "R";
        public string LabelsClassName => "L";

        // Generate
        public void Generate() {
            var settings = AddressableAssetSettingsDefaultObject.Settings;
            new ResourcesSourceGenerator().Generate( this.ResourcesPath, this.ResourcesClassNamespace, this.ResourcesClassName, settings );
            new LabelsSourceGenerator().Generate( this.LabelsPath, this.LabelsClassNamespace, this.LabelsClassName, settings );
        }

    }
}
