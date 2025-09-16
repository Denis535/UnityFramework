#nullable enable
namespace UnityEngine {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public static class GameObject2 {

        // Require/GameObject
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static GameObject Require(string name) {
            var result = GameObject.Find( name );
            Assert.Operation.Message( $"GameObject {name} was not found" ).Valid( result != null );
            return result;
        }
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static GameObject RequireGameObjectWithTag(string tag) {
            var result = GameObject.FindGameObjectWithTag( tag );
            Assert.Operation.Message( $"GameObject (with tag {tag}) was not found" ).Valid( result != null );
            return result;
        }

        // Require/GameObjects
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static GameObject[] RequireGameObjectsWithTag<T>(string tag) {
            var result = GameObject.FindGameObjectsWithTag( tag ).NullIfEmpty();
            Assert.Operation.Message( $"GameObjects (with tag {tag}) was not found" ).Valid( result != null );
            return result;
        }

    }
}
