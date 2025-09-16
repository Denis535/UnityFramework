#nullable enable
namespace System.Diagnostics.CodeAnalysis {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;

    // https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis

    // Input:  field/property setter/method argument
    // Output: field/property getter/method result (or out argument)

    // AllowNull            - Allows null input
    // DisallowNull         - Disallows null input

    // MaybeNull            - Output maybe null
    // MaybeNull/When       - Output maybe null (when result == true/false)

    // NotNull              - Output is not null
    // NotNull/When         - Output is not null (when result == true/false)
    // NotNull/If/NotNull   - Output is not null (if argument != null)

    // MemberNotNull        - Member output is not null
    // MemberNotNull/When   - Member output is not null (when result == true/false)

    // DoesNotReturn        - Throws exception
    // DoesNotReturn/If     - Throws exception (if argument == true/false)

    // AllowNull
    //[AttributeUsage( AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, Inherited = false )]
    //internal sealed class AllowNullAttribute : Attribute {
    //}
    //[AttributeUsage( AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, Inherited = false )]
    //internal sealed class DisallowNullAttribute : Attribute {
    //}

    // MaybeNull
    //[AttributeUsage( AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue, Inherited = false )]
    //internal sealed class MaybeNullAttribute : Attribute {
    //}
    //[AttributeUsage( AttributeTargets.Parameter, Inherited = false )]
    //internal sealed class MaybeNullWhenAttribute : Attribute {
    //    public bool ReturnValue { get; }
    //    public MaybeNullWhenAttribute(bool returnValue) {
    //        ReturnValue = returnValue;
    //    }
    //}

    // NotNull
    //[AttributeUsage( AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue, Inherited = false )]
    //internal sealed class NotNullAttribute : Attribute {
    //}
    //[AttributeUsage( AttributeTargets.Parameter, Inherited = false )]
    //internal sealed class NotNullWhenAttribute : Attribute {
    //    public bool ReturnValue { get; }
    //    public NotNullWhenAttribute(bool returnValue) {
    //        ReturnValue = returnValue;
    //    }
    //}
    //[AttributeUsage( AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue, AllowMultiple = true, Inherited = false )]
    //internal sealed class NotNullIfNotNullAttribute : Attribute {
    //    public string ParameterName { get; }
    //    public NotNullIfNotNullAttribute(string parameterName) {
    //        ParameterName = parameterName;
    //    }
    //}

    // MemberNotNull
    [EditorBrowsable( EditorBrowsableState.Never )]
    [AttributeUsage( AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true )]
    public sealed class MemberNotNullAttribute : Attribute {
        public string[] Members { get; }
        public MemberNotNullAttribute(string member) {
            this.Members = new[] { member };
        }
        public MemberNotNullAttribute(params string[] members) {
            this.Members = members;
        }
    }
    [EditorBrowsable( EditorBrowsableState.Never )]
    [AttributeUsage( AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true )]
    public sealed class MemberNotNullWhenAttribute : Attribute {
        public bool ReturnValue { get; }
        public string[] Members { get; }
        public MemberNotNullWhenAttribute(bool returnValue, string member) {
            this.ReturnValue = returnValue;
            this.Members = new[] { member };
        }
        public MemberNotNullWhenAttribute(bool returnValue, params string[] members) {
            this.ReturnValue = returnValue;
            this.Members = members;
        }
    }

    // DoesNotReturn
    //[AttributeUsage( AttributeTargets.Method, Inherited = false )]
    //internal sealed class DoesNotReturnAttribute : Attribute {
    //}
    //[AttributeUsage( AttributeTargets.Parameter, Inherited = false )]
    //internal sealed class DoesNotReturnIfAttribute : Attribute {
    //    public bool ParameterValue { get; }
    //    public DoesNotReturnIfAttribute(bool parameterValue) {
    //        ParameterValue = parameterValue;
    //    }
    //}
}
