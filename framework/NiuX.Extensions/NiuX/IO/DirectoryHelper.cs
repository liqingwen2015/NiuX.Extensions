using System.IO;
using JetBrains.Annotations;

namespace NiuX.IO;

/// <summary>
/// A helper class for Directory operations.
/// </summary>
public static class DirectoryHelper
{
    /// <summary>
    /// Creates if not exists.
    /// </summary>
    /// <param name="directory">The directory.</param>
    public static void CreateIfNotExists(string directory)
    {
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }

    /// <summary>
    /// Deletes if exists.
    /// </summary>
    /// <param name="directory">The directory.</param>
    public static void DeleteIfExists(string directory)
    {
        if (Directory.Exists(directory))
        {
            Directory.Delete(directory);
        }
    }

    /// <summary>
    /// Deletes if exists.
    /// </summary>
    /// <param name="directory">The directory.</param>
    /// <param name="recursive">if set to <c>true</c> [recursive].</param>
    public static void DeleteIfExists(string directory, bool recursive)
    {
        if (Directory.Exists(directory))
        {
            Directory.Delete(directory, recursive);
        }
    }

    /// <summary>
    /// Creates if not exists.
    /// </summary>
    /// <param name="directory">The directory.</param>
    public static void CreateIfNotExists(DirectoryInfo directory)
    {
        if (!directory.Exists)
        {
            directory.Create();
        }
    }

    /// <summary>
    /// Determines whether [is sub directory of] [the specified parent directory path].
    /// </summary>
    /// <param name="parentDirectoryPath">The parent directory path.</param>
    /// <param name="childDirectoryPath">The child directory path.</param>
    /// <returns>
    ///   <c>true</c> if [is sub directory of] [the specified parent directory path]; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsSubDirectoryOf([NotNull] string parentDirectoryPath, [NotNull] string childDirectoryPath)
    {
        Checker.NotNull(parentDirectoryPath, nameof(parentDirectoryPath));
        Checker.NotNull(childDirectoryPath, nameof(childDirectoryPath));

        return IsSubDirectoryOf(
            new DirectoryInfo(parentDirectoryPath),
            new DirectoryInfo(childDirectoryPath)
        );
    }

    /// <summary>
    /// Determines whether [is sub directory of] [the specified parent directory].
    /// </summary>
    /// <param name="parentDirectory">The parent directory.</param>
    /// <param name="childDirectory">The child directory.</param>
    /// <returns>
    ///   <c>true</c> if [is sub directory of] [the specified parent directory]; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsSubDirectoryOf([NotNull] DirectoryInfo parentDirectory,
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