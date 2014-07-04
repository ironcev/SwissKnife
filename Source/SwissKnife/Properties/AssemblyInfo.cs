using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SwissKnife.Tests.Unit")]

// ReSharper disable CheckNamespace
internal static partial class AssemblyDescription
{
    public const string Title = "The Swiss Army knife for .NET developers.";
    public const string Description = "SwissKnife is a lightweight, well-documented and well-tested general purpose .NET class library. " +
                                      "It is a natural extension of the .NET framework designed to be used on all types of .NET projects. " +
                                      "SwissKnife simplifies common programming tasks like: argument validation (using code contracts), " +
                                      "safe string conversions, date and time manipulation, cryptography, reflection and many more.";
}
// ReSharper restore CheckNamespace