using JetBrains.Annotations;
using NiuX;

namespace System.IO;

/// <summary>
/// DirectoryInfo Extensions
/// </summary>
public static class NiuXDirectoryInfoExtensions
{
    /// <summary>
    /// Creates if not exists.
    /// </summary>
    /// <param name="directory">The directory.</param>
    public static void CreateIfNotExists(this DirectoryInfo directory)
    {
        if (!directory.Exists)
        {
            directory.Create();
        }
    }

    /// <summary>
    /// Determines whether [is sub directory of] [the specified child directory].
    /// </summary>
    /// <param name="parentDirectory">The parent directory.</param>
    /// <param name="childDirectory">The child directory.</param>
    /// <returns>
    ///   <c>true</c> if [is sub directory of] [the specified child directory]; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsSubDirectoryOf([NotNull] this DirectoryInfo parentDirectory,
        [NotNull] DirectoryInfo childDirectory)
    {
        Checker.NotNull(parentDirectory, nameof(parentDirectory));
        Checker.NotNull(childDirectory, nameof(childDirectory));

        if (parentDirectory.FullName == childDirectory.FullName)
        {
            return true;
        }

        var parentOfChild = childDirectory.Parent;
        if (parentOfChild == null)
        {
            return false;
        }

        return IsSubDirectoryOf(parentDirectory, parentOfChild);
    }
}