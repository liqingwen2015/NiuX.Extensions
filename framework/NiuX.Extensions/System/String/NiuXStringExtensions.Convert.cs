// ReSharper disable CheckNamespace

using System.ComponentModel;

namespace System;

/// <summary>
/// String Extensions
/// </summary>
public static partial class NiuXStringExtensions
{
    /// <summary>
    /// Converts to int16.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static short ToInt16(this string str)
    {
        return Convert.ToInt16(str);
    }

    /// <summary>
    /// Converts to short.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static short ToShort(this string str)
    {
        return Convert.ToInt16(str);
    }

    /// <summary>
    /// Converts to int32.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static int ToInt32(this string str)
    {
        return Convert.ToInt32(str);
    }

    /// <summary>
    /// Converts the string representation of a number to an integer.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static int ToInt(this string str)
    {
        return Convert.ToInt32(str);
    }

    /// <summary>
    /// Converts to int64.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static long ToInt64(this string str)
    {
        return Convert.ToInt64(str);
    }

    /// <summary>
    /// Converts to long.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static long ToLong(this string str)
    {
        return Convert.ToInt64(str);
    }

    /// <summary>
    /// Converts to datetime.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static DateTime ToDateTime(this string str)
    {
        return Convert.ToDateTime(str);
    }

    /// <summary>
    /// Converts to boolean.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static bool ToBoolean(this string str)
    {
        return Convert.ToBoolean(str);
    }

    /// <summary>
    /// Converts to bool.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static bool ToBool(this string str)
    {
        return Convert.ToBoolean(str);
    }

    /// <summary>
    /// To the specified input.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="input">The input.</param>
    /// <returns></returns>
    public static T To<T>(this string input)
    {
        try
        {
            return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(input);
        }
        catch (Exception)
        {
            return default;
        }
    }

    /// <summary>
    /// To the specified type.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public static object To(this string input, Type type)
    {
        try
        {
            return TypeDescriptor.GetConverter(type).ConvertFromString(input);
        }
        catch (Exception)
        {
            return null;
        }
    }
}