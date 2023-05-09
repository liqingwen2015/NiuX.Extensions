using System.Collections.Generic;

namespace NiuX.Extensions;

/// <summary>
/// List Extensions
/// </summary>
public static class ListExtensions
{
    /// <summary>
    /// Adds to list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    public static List<T> AddToList<T>(this T item)
    {
        return new List<T> { item };
    }
}