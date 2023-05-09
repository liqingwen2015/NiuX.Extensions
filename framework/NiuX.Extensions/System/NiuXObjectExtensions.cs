using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.Json;

// ReSharper disable once CheckNamespace
namespace System;

/// <summary>
/// Extension methods for all objects.
/// </summary>
public static class NiuXObjectExtensions
{
    /// <summary>
    /// Used to simplify and beautify casting an object to a type.
    /// </summary>
    /// <typeparam name="T">Type to be casted</typeparam>
    /// <param name="obj">Object to cast</param>
    /// <returns>
    /// Casted object
    /// </returns>
    public static T As<T>(this object obj)
        where T : class
    {
        return (T)obj;
    }

    /// <summary>
    /// Can be used to conditionally perform a function
    /// on an object and return the modified or the original object.
    /// It is useful for chained calls.
    /// </summary>
    /// <typeparam name="T">Type of the object</typeparam>
    /// <param name="obj">An object</param>
    /// <param name="condition">A condition</param>
    /// <param name="func">A function that is executed only if the condition is <code>true</code></param>
    /// <returns>
    /// Returns the modified object (by the <paramref name="func" /> if the <paramref name="condition" /> is <code>true</code>)
    /// or the original object if the <paramref name="condition" /> is <code>false</code>
    /// </returns>
    public static T If<T>(this T obj, bool condition, Func<T, T> func)
    {
        return condition ? func(obj) : obj;
    }

    /// <summary>
    /// Can be used to conditionally perform an action
    /// on an object and return the original object.
    /// It is useful for chained calls on the object.
    /// </summary>
    /// <typeparam name="T">Type of the object</typeparam>
    /// <param name="obj">An object</param>
    /// <param name="condition">A condition</param>
    /// <param name="action">An action that is executed only if the condition is <code>true</code></param>
    /// <returns>
    /// Returns the original object.
    /// </returns>
    public static T If<T>(this T obj, bool condition, Action<T> action)
    {
        if (condition)
        {
            action(obj);
        }

        return obj;
    }

    /// <summary>
    /// Determines whether this instance is function.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns>
    ///   <c>true</c> if the specified object is function; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsFunc(this object obj)
    {
        if (obj == null)
        {
            return false;
        }

        var type = obj.GetType();

        if (!type.GetTypeInfo().IsGenericType)
        {
            return false;
        }

        return type.GetGenericTypeDefinition() == typeof(Func<>);
    }

    /// <summary>
    /// Determines whether this instance is function.
    /// </summary>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    /// <param name="obj">The object.</param>
    /// <returns>
    ///   <c>true</c> if the specified object is function; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsFunc<TReturn>(this object obj)
    {
        return obj != null && obj.GetType() == typeof(Func<TReturn>);
    }

    /// <summary>
    /// Check if an item is in a list.
    /// </summary>
    /// <typeparam name="T">Type of the items</typeparam>
    /// <param name="item">Item to check</param>
    /// <param name="list">List of items</param>
    /// <returns>
    ///   <c>true</c> if the specified list is in; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsIn<T>(this T item, params T[] list)
    {
        return list.Contains(item);
    }

    /// <summary>
    /// Check if an item is in the given enumerable.
    /// </summary>
    /// <typeparam name="T">Type of the items</typeparam>
    /// <param name="item">Item to check</param>
    /// <param name="items">Items</param>
    /// <returns>
    ///   <c>true</c> if the specified items is in; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsIn<T>(this T item, IEnumerable<T> items)
    {
        return items.Contains(item);
    }

    /// <summary>
    /// Converts given object to a value type using <see cref="Convert.ChangeType(object,System.Type)" /> method.
    /// </summary>
    /// <typeparam name="T">Type of the target object</typeparam>
    /// <param name="obj">Object to be converted</param>
    /// <returns>
    /// Converted object
    /// </returns>
    public static T To<T>(this object obj)
        where T : struct
    {
        if (typeof(T) == typeof(Guid))
        {
            return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(obj.ToString());
        }

        return (T)Convert.ChangeType(obj, typeof(T), CultureInfo.InvariantCulture);
    }

    #region To

    /// <summary>
    /// Converts to boolean.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static bool ToBoolean(this object str)
    {
        return Convert.ToBoolean(str);
    }

    /// <summary>
    /// Converts to datetime.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static DateTime ToDateTime(this object str)
    {
        return Convert.ToDateTime(str);
    }

    /// <summary>
    /// Converts the string representation of a number to an integer.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static int ToInt(this object str)
    {
        return Convert.ToInt32(str);
    }

    /// <summary>
    /// Converts to int16.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static short ToInt16(this object str)
    {
        return Convert.ToInt16(str);
    }

    /// <summary>
    /// Converts to int32.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static int ToInt32(this object str)
    {
        return Convert.ToInt32(str);
    }

    /// <summary>
    /// Converts to int64.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static long ToInt64(this object str)
    {
        return Convert.ToInt64(str);
    }

    /// <summary>
    /// Converts to long.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static long ToLong(this object str)
    {
        return Convert.ToInt64(str);
    }

    /// <summary>
    /// Converts to short.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static short ToShort(this object str)
    {
        return Convert.ToInt16(str);
    }

    #endregion To

    #region DeepClone

    /// <summary>
    /// 使用 Json 序列化器进行深拷贝
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static T? DeepCloneWithJsonSerializer<T>(this T source)
    {
        return source is null ? default : source.ToJson().FromJson<T>();
    }

    /// <summary>
    /// 使用反射进行深拷贝
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static T? DeepCloneWithReflection<T>(this T obj)
    {
        //如果是字符串或值类型则直接返回
        if (obj == null || obj is string || obj.GetType().IsValueType)
        {
            return obj;
        }

        var retval = Activator.CreateInstance(obj.GetType());

        foreach (var field in obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic |
                                                      BindingFlags.Instance | BindingFlags.Static))
        {
            try
            {
                field.SetValue(retval, DeepCloneWithReflection(field.GetValue(obj)));
            }
            catch
            {
                // ignored
            }
        }

        return (T)retval;
    }

    #endregion DeepClone
}