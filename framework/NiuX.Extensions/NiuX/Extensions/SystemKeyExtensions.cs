using System;

namespace NiuX.Extensions;

/// <summary>
/// 系统关键字扩展
/// </summary>
public static class SystemKeyExtensions
{
    /// <summary>
    /// Ifs the specified condition.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t">The t.</param>
    /// <param name="condition">if set to <c>true</c> [condition].</param>
    /// <param name="action">The action.</param>
    /// <returns></returns>
    public static T If<T>(this T t, bool condition, Action<T> action) where T : class
    {
        if (condition)
        {
            action(t);
        }

        return t;
    }

    /// <summary>
    /// Ifs the specified predicate.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t">The t.</param>
    /// <param name="predicate">The predicate.</param>
    /// <param name="action">The action.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException"></exception>
    public static T If<T>(this T t, Predicate<T> predicate, Action<T> action) where T : class
    {
        if (t == null)
        {
            throw new ArgumentNullException();
        }

        if (predicate(t))
        {
            action(t);
        }

        return t;
    }

    /// <summary>
    /// Ifs the specified predicate.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t">The t.</param>
    /// <param name="predicate">The predicate.</param>
    /// <param name="func">The function.</param>
    /// <returns></returns>
    public static T If<T>(this T t, Predicate<T> predicate, Func<T, T> func) where T : struct
    {
        return predicate(t) ? func(t) : t;
    }
}