#nullable enable
namespace UnityEngine.AddressableAssets {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.ResourceManagement.AsyncOperations;
    using UnityEngine.ResourceManagement.ResourceProviders;
    using UnityEngine.SceneManagement;

    public class SceneHandle : AddressableHandle<SceneInstance> {

        // Constructor
        public SceneHandle(string key) : base( key ) {
        }

        // Load
        public SceneHandle Load(LoadSceneMode loadMode, bool activateOnLoad) {
            this.Assert_IsNotValid();
            this.Handle = Addressables.LoadSceneAsync( this.Key, loadMode, activateOnLoad );
            return this;
        }

        // Wait
        public ValueTask WaitAsync() {
            this.Assert_IsValid();
            return this.Handle.WaitAsync( default );
        }

        // GetValue
        public async ValueTask<Scene> GetValueAsync() {
            this.Assert_IsValid();
            var value = await this.Handle.GetResultAsync( default );
            return value.Scene;
        }

        // Activate
        public async ValueTask<Scene> ActivateAsync() {
            this.Assert_IsValid();
            var value = await this.Handle.GetResultAsync( default );
            await value.ActivateAsync();
            return value.Scene;
        }

        // Unload
        public void Unload() {
            this.Assert_IsValid();
            Addressables.UnloadSceneAsync( this.Handle ).Wait();
            this.Handle = default;
        }
        public async ValueTask UnloadAsync() {
            this.Assert_IsValid();
            await Addressables.UnloadSceneAsync( this.Handle ).WaitAsync( default );
            this.Handle = default;
        }

        // Unload
        public void UnloadSafe() {
            if (this.Handle.IsValid()) {
                this.Unload();
            }
        }
        public async ValueTask UnloadSafeAsync() {
            if (this.Handle.IsValid()) {
                await this.UnloadAsync();
            }
        }

        // Utils
        public override string ToString() {
            return "SceneHandle: " + this.Key;
        }

    }
}
