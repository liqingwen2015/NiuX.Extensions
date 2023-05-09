// ReSharper disable CheckNamespace

using System.Collections.Generic;

namespace System;

/// <summary>
/// 
/// </summary>
public static partial class NiuXStringExtensions
{
    #region private fields

    /// <summary>
    /// 待转换方法
    /// </summary>
    private readonly static Dictionary<Type, Func<string, object>> ToParseMethods = new() {
        { typeof(byte), x => x.ParseByte() },
        { typeof(int), x => x.ParseInt() },
        { typeof(long), x => x.ParseLong() },
        { typeof(float), x => x.ParseFloat() },
        { typeof(double), x => x.ParseDouble() },
        { typeof(decimal), x => x.ParseDecimal() },
        { typeof(bool), x => x.ParseBool() },
        { typeof(DateTime), x => x.ParseDateTime() },
        { typeof(Guid), x => x.ParseGuid() }
    };

    #endregion private fields

    /// <summary>
    /// Parses the byte.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static byte ParseByte(this string str)
    {
        return byte.Parse(str);
    }

    /// <summary>
    /// Parses the int.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static int ParseInt(this string str)
    {
        return int.Parse(str);
    }

    /// <summary>
    /// Parses the long.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static long ParseLong(this string str)
    {
        return long.Parse(str);
    }

    /// <summary>
    /// Parses the short.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static short ParseShort(this string str)
    {
        return short.Parse(str);
    }

    /// <summary>
    /// Parses the date time.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static DateTime ParseDateTime(this string str)
    {
        return DateTime.Parse(str);
    }

    /// <summary>
    /// Parses the decimal.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static decimal ParseDecimal(this string str)
    {
        return decimal.Parse(str);
    }

    /// <summary>
    /// Parses the float.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static float ParseFloat(this string str)
    {
        return float.Parse(str);
    }

    /// <summary>
    /// Parses the double.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static double ParseDouble(this string str)
    {
        return double.Parse(str);
    }

    /// <summary>
    /// Parses the bool.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static bool ParseBool(this string str)
    {
        return bool.Parse(str);
    }

    /// <summary>
    /// Parses the unique identifier.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static Guid ParseGuid(this string str)
    {
        return Guid.Parse(str);
    }

    /// <summary>
    /// Parses the specified string.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static T Parse<T>(this string str) where T : struct
    {
        return (T)ToParseMethods!.GetValue(typeof(T))!(str);
    }
}