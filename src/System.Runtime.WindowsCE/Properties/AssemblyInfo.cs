using System;
using System.Reflection;
using System.Resources;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: NeutralResourcesLanguage("en-us")]
[assembly: AssemblyCompany("Fabrício Godoy")]
[assembly: AssemblyCopyright("© Fabrício Godoy. All rights reserved.")]
[assembly: AssemblyProduct("System.Runtime")]
[assembly: AssemblyDescription("Provides the fundamental primitives, classes and base classes")]
[assembly: CLSCompliant(true)]

[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyInformationalVersion("1.0.0")]
[assembly: AssemblyFileVersion("1.0.0")]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Retail")]
#endif

#if CLASSIC
[assembly: AssemblyKeyFile(@"..\..\..\tools\keypair.snk")]
[assembly: AssemblyDelaySign(true)]
#endif

