using System;
using System.Collections.Generic;
using System.Linq;

namespace NiuX.Extensions;

/// <summary>
/// Dictionary Extensions
/// </summary>
public static class DictionaryExtensions
{
    /// <summary>
    /// The default ignore case comparer
    /// </summary>
    public static StringComparer DefaultIgnoreCaseComparer = StringComparer.OrdinalIgnoreCase;

    /// <summary>
    /// Converts to dictionarywithignorecase.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <returns></returns>
    public static Dictionary<TKey, TSource> ToDictionaryWithIgnoreCase<TSource, TKey>(this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector) where TKey : notnull
    {
        return source.ToDictionary(keySelector, DefaultIgnoreCaseComparer);
    }

    /// <summary>
    /// Converts to dictionarywithignorecase.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TElement">The type of the element.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <param name="elementSelector">The element selector.</param>
    /// <returns></returns>
    public static Dictionary<TKey, TElement> ToDictionaryWithIgnoreCase<TSource, TKey, TElement>(
        this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        where TKey : notnull
    {
        return source.ToDictionary(keySelector, elementSelector, DefaultIgnoreCaseComparer);
    }

    /// <summary>
    /// Converts to dictionarywithordinalignorecase.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <returns></returns>
    public static Dictionary<TKey, TSource> ToDictionaryWithOrdinalIgnoreCase<TSource, TKey>(
        this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) where TKey : notnull
    {
        return source.ToDictionary(keySelector, StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Converts to dictionarywithordinalignorecase.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TElement">The type of the element.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <param name="elementSelector">The element selector.</param>
    /// <returns></returns>
    public static Dictionary<TKey, TElement> ToDictionaryWithOrdinalIgnoreCase<TSource, TKey, TElement>(
        this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        where TKey : notnull
    {
        return source.ToDictionary(keySelector, elementSelector, StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Converts to dictionarywithcurrentcultureignorecase.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <returns></returns>
    public static Dictionary<TKey, TSource> ToDictionaryWithCurrentCultureIgnoreCase<TSource, TKey>(
        this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) where TKey : notnull
    {
        return source.ToDictionary(keySelector, StringComparer.CurrentCultureIgnoreCase);
    }

    /// <summary>
    /// Converts to dictionarywithcurrentcultureignorecase.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TElement">The type of the element.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <param name="elementSelector">The element selector.</param>
    /// <returns></returns>
    public static Dictionary<TKey, TElement> ToDictionaryWithCurrentCultureIgnoreCase<TSource, TKey, TElement>(
        this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        where TKey : notnull
    {
        return source.ToDictionary(keySelector, elementSelector, StringComparer.CurrentCultureIgnoreCase);
    }

    /// <summary>
    /// Converts to dictionarywithinvariantcultureignorecase.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <returns></returns>
    public static Dictionary<TKey, TSource> ToDictionaryWithInvariantCultureIgnoreCase<TSource, TKey>(
        this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) where TKey : notnull
    {
        return source.ToDictionary(keySelector, StringComparer.InvariantCultureIgnoreCase);
    }

    /// <summary>
    /// Converts to dictionarywithinvariantcultureignorecase.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TElement">The type of the element.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <param name="elementSelector">The element selector.</param>
    /// <returns></returns>
    public static Dictionary<TKey, TElement> ToDictionaryWithInvariantCultureIgnoreCase<TSource, TKey, TElement>(
        this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        where TKey : notnull
    {
        return source.ToDictionary(keySelector, elementSelector, StringComparer.InvariantCultureIgnoreCase);
    }

    /// <summary>
    /// Converts to dictionary.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <param name="stringComparer">The string comparer.</param>
    /// <returns></returns>
    public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector, StringComparer stringComparer) where TKey : notnull
    {
        return source.ToDictionary(keySelector, (IEqualityComparer<TKey>)stringComparer);
    }

    /// <summary>
    /// Converts to dictionary.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TElement">The type of the element.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <param name="elementSelector">The element selector.</param>
    /// <param name="stringComparer">The string comparer.</param>
    /// <returns></returns>
    public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, StringComparer stringComparer)
        where TKey : notnull
    {
        return source.ToDictionary(keySelector, elementSelector, (IEqualityComparer<TKey>)stringComparer);
    }
}