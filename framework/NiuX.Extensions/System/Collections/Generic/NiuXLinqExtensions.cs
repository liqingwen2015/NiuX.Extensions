using System.Linq;
using System.Threading.Tasks;

// ReSharper disable CheckNamespace

namespace System.Collections.Generic;

/// <summary>
/// Linq Extensions
/// </summary>
public static class NiuXLinqExtensions
{
    /// <summary>
    /// 对一个序列应用累加器函数。
    /// </summary>
    /// <typeparam name="TSource">中的元素的类型 <paramref name="source" />。</typeparam>
    /// <param name="source"><see cref="T:System.Collections.Generic.IEnumerable`1" /> 对其进行聚合。</param>
    /// <param name="func">要对每个元素调用的累加器函数。</param>
    /// <returns>
    /// 累加器的最终值。
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// source
    /// or
    /// func
    /// </exception>
    /// <exception cref="System.InvalidOperationException">NoElements</exception>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> 或 <paramref name="func" /> 为 <see langword="null" />。</exception>
    /// <exception cref="T:System.InvalidOperationException"><paramref name="source" /> 不包含任何元素。</exception>
    public static TSource Aggregate<TSource>(this IEnumerable<TSource> source,
        Func<TSource, TSource, int, TSource> func)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (func == null)
        {
            throw new ArgumentNullException(nameof(func));
        }

        using (var enumerator = source.GetEnumerator())
        {
            var index = 0;

            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException("NoElements");
            }

            var current = enumerator.Current;

            while (enumerator.MoveNext())
            {
                current = func(current, enumerator.Current, ++index);
            }

            return current;
        }
    }

    /// <summary>
    /// 遍历每项
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source">The source.</param>
    /// <param name="action">The action.</param>
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        if (source == null)
        {
            return;
        }

        foreach (var item in source)
        {
            action(item);
        }
    }

    /// <summary>
    /// 遍历每项
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source">The source.</param>
    /// <param name="action">The action.</param>
    public async static Task ForEach<T>(this IEnumerable<T> source, Func<T, Task> action)
    {
        if (source == null)
        {
            return;
        }

        foreach (var item in source)
        {
            await action(item);
        }
    }

    /// <summary>
    /// 遍历每项，忽略异常
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source">The source.</param>
    /// <param name="action">The action.</param>
    public static void ForEachIgnoreExcetion<T>(this IEnumerable<T> source, Action<T> action)
    {
        if (source == null)
        {
            return;
        }

        foreach (var item in source)
        {
            try
            {
                action(item);
            }
            catch
            {
                // ignored
            }
        }
    }
    //#region WhereIf

    //public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool condition, Expression<Func<T, bool>> predicate)
    //{
    //    return condition ? source.Where(predicate) : source;
    //}

    //public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool condition, Expression<Func<T, int, bool>> predicate)
    //{
    //    return condition ? source.Where(predicate) : source;
    //}

    //public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
    //{
    //    return condition ? source.Where(predicate) : source;
    //}

    //public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, int, bool> predicate)
    //{
    //    return condition ? source.Where(predicate) : source;
    //}

    //#endregion

    #region Distinct

    /// <summary>
    /// Distincts the specified key selector.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TComparisonKey">The type of the comparison key.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <returns></returns>
    public static IEnumerable<T> Distinct<T, TComparisonKey>(this IEnumerable<T> source,
        Func<T, TComparisonKey> keySelector)
    {
        return source.Distinct(new CommonEqualityComparer<T, TComparisonKey>(keySelector));
    }

    /// <summary>
    /// Distincts the specified key selector.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TComparisonKey">The type of the comparison key.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <param name="comparer">The comparer.</param>
    /// <returns></returns>
    public static IEnumerable<T> Distinct<T, TComparisonKey>(this IEnumerable<T> source,
        Func<T, TComparisonKey> keySelector, IEqualityComparer<TComparisonKey> comparer)
    {
        return source.Distinct(new CommonEqualityComparer<T, TComparisonKey>(keySelector, comparer));
    }

    /// <summary>
    /// https://www.cnblogs.com/CreateMyself/p/12863407.html
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source">The source.</param>
    /// <param name="comparer">The comparer.</param>
    /// <returns></returns>
    public static IEnumerable<T> Distinct<T>(this IEnumerable<T> source, Func<T, T, bool> comparer) where T : class
    {
        return source.Distinct(new DynamicEqualityComparer<T>(comparer));
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Collections.Generic.IEqualityComparer&lt;T&gt;" />
    private sealed class DynamicEqualityComparer<T> : IEqualityComparer<T>
        where T : class
    {
        /// <summary>
        /// The function
        /// </summary>
        private readonly Func<T, T, bool> _func;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicEqualityComparer{T}"/> class.
        /// </summary>
        /// <param name="func">The function.</param>
        public DynamicEqualityComparer(Func<T, T, bool> func)
        {
            _func = func;
        }

        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">The first object of type T to compare.</param>
        /// <param name="y">The second object of type T to compare.</param>
        /// <returns>
        /// true if the specified objects are equal; otherwise, false.
        /// </returns>
        public bool Equals(T x, T y)
        {
            return _func(x, y);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public int GetHashCode(T obj)
        {
            return 0;
        }
    }

    #endregion Distinct
}

/// <summary>
/// 通用相等比较器
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TComparisonKey">The type of the comparison key.</typeparam>
/// <seealso cref="System.Collections.Generic.IEqualityComparer&lt;T&gt;" />
public class CommonEqualityComparer<T, TComparisonKey> : IEqualityComparer<T>
{
    /// <summary>
    /// The comparer
    /// </summary>
    private readonly IEqualityComparer<TComparisonKey> _comparer;

    /// <summary>
    /// The key selector
    /// </summary>
    private readonly Func<T, TComparisonKey> _keySelector;

    /// <summary>
    /// Initializes a new instance of the <see cref="CommonEqualityComparer{T, TComparisonKey}"/> class.
    /// </summary>
    /// <param name="keySelector">The key selector.</param>
    /// <param name="comparer">The comparer.</param>
    public CommonEqualityComparer(Func<T, TComparisonKey> keySelector, IEqualityComparer<TComparisonKey> comparer)
    {
        _keySelector = keySelector;
        _comparer = comparer;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CommonEqualityComparer{T, TComparisonKey}"/> class.
    /// </summary>
    /// <param name="keySelector">The key selector.</param>
    public CommonEqualityComparer(Func<T, TComparisonKey> keySelector) : this(keySelector,
        EqualityComparer<TComparisonKey>.Default)
    {
    }

    /// <summary>
    /// Determines whether the specified objects are equal.
    /// </summary>
    /// <param name="x">The first object of type T to compare.</param>
    /// <param name="y">The second object of type T to compare.</param>
    /// <returns>
    /// true if the specified objects are equal; otherwise, false.
    /// </returns>
    public bool Equals(T x, T y)
    {
        return _comparer.Equals(_keySelector(x), _keySelector(y));
    }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
    /// </returns>
    public int GetHashCode(T obj)
    {
        return _comparer.GetHashCode(_keySelector(obj));
    }
}