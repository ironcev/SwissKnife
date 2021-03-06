/*
 The Code Contracts attributes defined in this source file are not defined in mscorlib.dll 4.0.
 In order to use them, we had to add this file to the project.
 The file is slightly changed in order to satisfy ReSharper.
 The original file can be found on this location (if Code Contracts are installed, of course):
   C:\Program Files (x86)\Microsoft\Contracts\Languages\CSharp\
*/

// ReSharper disable CheckNamespace
namespace System.Diagnostics.Contracts
// ReSharper restore CheckNamespace
{
    /// <summary>
    /// Enables factoring if-then-throw contracts into separate methods.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    [Conditional("CONTRACTS_FULL")]
    public sealed class ContractArgumentValidatorAttribute : Attribute
    {
    }

    /// <summary>
    /// Enables writing abbreviations for contracts that get copied to other methods.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    [Conditional("CONTRACTS_FULL")]
    public sealed class ContractAbbreviatorAttribute : Attribute
    {
    }

    /// <summary>
    /// Allows setting contract and tool options at assembly, type, or method granularity.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
    [Conditional("CONTRACTS_FULL")]
    public sealed class ContractOptionAttribute : Attribute
    {
        // ReSharper disable UnusedParameter.Local
        // ReSharper disable CSharpWarnings::CS1591
#pragma warning disable 1591 // warning CS1591: Missing XML comment for publicly visible type or member 'System.Diagnostics.Contracts.ContractOptionAttribute.ContractOptionAttribute(string, string, bool)'
        [CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "category", Justification = "Build-time only attribute")]
        [CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "setting", Justification = "Build-time only attribute")]
        [CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "toggle", Justification = "Build-time only attribute")]
        public ContractOptionAttribute(string category, string setting, bool toggle) { }
        [CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "category", Justification = "Build-time only attribute")]
        [CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "setting", Justification = "Build-time only attribute")]
        [CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value", Justification = "Build-time only attribute")]
        public ContractOptionAttribute(string category, string setting, string value) { }
#pragma warning disable 1591 // warning CS1591: Missing XML comment for publicly visible type or member 'System.Diagnostics.Contracts.ContractOptionAttribute.ContractOptionAttribute(string, string, bool)'
        // ReSharper restore CSharpWarnings::CS1591
        // ReSharper restore UnusedParameter.Local
    }
}

