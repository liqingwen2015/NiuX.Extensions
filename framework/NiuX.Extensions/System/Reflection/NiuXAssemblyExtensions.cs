using System.Diagnostics;

namespace System.Reflection;

/// <summary>
/// Assembly Extensions
/// </summary>
public static class NiuXAssemblyExtensions
{
    /// <summary>
    /// Gets the file version.
    /// </summary>
    /// <param name="assembly">The assembly.</param>
    /// <returns></returns>
    public static string GetFileVersion(this Assembly assembly)
    {
        return FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion;
    }
}