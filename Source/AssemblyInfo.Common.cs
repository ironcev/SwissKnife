using System;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]

[assembly: AssemblyCompany(AssemblyDescription.Company)]
[assembly: AssemblyTrademark(AssemblyDescription.Trademark)]
[assembly: AssemblyCopyright(AssemblyDescription.Copyright)]

[assembly: AssemblyConfiguration(AssemblyDescription.Configuration)]

[assembly: AssemblyTitle(AssemblyDescription.Title)]
[assembly: AssemblyProduct(AssemblyDescription.Product)]
[assembly: AssemblyDescription(AssemblyDescription.Product)]

[assembly: AssemblyVersion(AssemblyDescription.Version)]
// We will not set the AssemblyFileVersion explicitly. This will automatically make it same as the AssemblyVersion.

[assembly: AssemblyCulture(AssemblyDescription.Culture)]
[assembly: NeutralResourcesLanguage("en-US")]


internal static partial class AssemblyDescription
{
    public const string Company = "Igor Rončević";
    public const string Trademark = "";
    public const string Copyright = "Copyright © 2013 - 2014 " + Company;
    public const string Product = "SwissKnife";

    public const string Version = "0.4.2.*";
    public const string FileVersion = Version;

    public const string Culture = "";

    public const string Configuration =
#if DEBUG
 "Debug"
#else
 "Release"
#endif
;
}