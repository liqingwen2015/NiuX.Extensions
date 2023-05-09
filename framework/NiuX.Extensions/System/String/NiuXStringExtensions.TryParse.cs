// ReSharper disable CheckNamespace

using System.Collections.Generic;

namespace System;

/// <summary>
/// 
/// </summary>
public static partial class NiuXStringExtensions
{
    /// <summary>
    /// Tries the parse byte.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns></returns>
    public static byte TryParseByte(this string str, byte defaultValue = default)
    {
        return byte.TryParse(str, out var result) ? result : defaultValue;
    }

    /// <summary>
    /// Tries the parse int.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns></returns>
    public static int TryParseInt(this string str, int defaultValue = default)
    {
        return int.TryParse(str, out var result) ? result : defaultValue;
    }

    /// <summary>
    /// Tries the parse long.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns></returns>
    public static long TryParseLong(this string str, long defaultValue = default)
    {
        return long.TryParse(str, out var result) ? result : defaultValue;
    }

    /// <summary>
    /// Tries the parse short.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns></returns>
    public static short TryParseShort(this string str, short defaultValue = default)
    {
        return short.TryParse(str, out var result) ? result : defaultValue;
    }

    /// <summary>
    /// Tries the parse date time.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns></returns>
    public static DateTime TryParseDateTime(this string str, DateTime defaultValue = default)
    {
        return DateTime.TryParse(str, out var result) ? result : defaultValue;
    }

    /// <summary>
    /// Tries the parse date time offset.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns></returns>
    public static DateTimeOffset TryParseDateTimeOffset(this string str, DateTime defaultValue = default)
    {
        return DateTimeOffset.TryParse(str, out var result) ? result : defaultValue;
    }

    /// <summary>
    /// Tries the parse decimal.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns></returns>
    public static decimal TryParseDecimal(this string str, decimal defaultValue = default)
    {
        return decimal.TryParse(str, out var result) ? result : defaultValue;
    }

    /// <summary>
    /// Tries the parse float.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns></returns>
    public static float TryParseFloat(this string str, float defaultValue = default)
    {
        return float.TryParse(str, out var result) ? result : defaultValue;
    }

    /// <summary>
    /// Tries the parse double.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns></returns>
    public static double TryParseDouble(this string str, double defaultValue = default)
    {
        return double.TryParse(str, out var result) ? result : defaultValue;
    }

    /// <summary>
    /// Tries the parse boolean.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="defaultValue">if set to <c>true</c> [default value].</param>
    /// <returns></returns>
    public static bool TryParseBoolean(this string str, bool defaultValue = default)
    {
        return bool.TryParse(str, out var result) ? result : defaultValue;
    }

    /// <summary>
    /// Tries the parse unique identifier.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns></returns>
    public static Guid TryParseGuid(this string str, Guid defaultValue = default)
    {
        return Guid.TryParse(str, out var result) ? result : defaultValue;
    }

    /// <summary>
    /// Tries the parse.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static T TryParse<T>(this string str) where T : struct
    {
        return ToTryParseMethods.TryGetValue(typeof(T), out var func) ? (T)func(str) : default;
    }

    /// <summary>
    /// Tries the parse.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="str">The string.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns></returns>
    public static T TryParse<T>(this string str, T defaultValue) where T : struct
    {
        return ToTryParseMethodsOfContainDefaultValue.TryGetValue(typeof(T), out var func)
            ? (T)func(str, defaultValue)
            : defaultValue;
    }

    #region private fields

    /// <summary>
    /// 待转换方法
    /// </summary>
    private readonly static Dictionary<Type, Func<string, object>> ToTryParseMethods = new() {
        { typeof(byte), x => x.TryParseByte() },
        { typeof(int), x => x.TryParseInt() },
        { typeof(long), x => x.TryParseLong() },
        { typeof(float), x => x.TryParseFloat() },
        { typeof(double), x => x.TryParseDouble() },
        { typeof(decimal), x => x.TryParseDecimal() },
        { typeof(bool), x => x.TryParseBoolean() },
        { typeof(DateTime), x => x.TryParseDateTime() },
        { typeof(Guid), x => x.TryParseGuid() }
    };

    /// <summary>
    /// 待转换含默认值的方法
    /// </summary>
    private readonly static Dictionary<Type, Func<string, object, object>> ToTryParseMethodsOfContainDefaultValue =
        new() {
            { typeof(byte), (x, y) => x.TryParseByte((byte)y) },
            { typeof(int), (x, y) => x.TryParseInt((int)y) },
            { typeof(long), (x, y) => x.TryParseLong((long)y) },
            { typeof(float), (x, y) => x.TryParseFloat((float)y) },
            { typeof(double), (x, y) => x.TryParseDouble((double)y) },
            { typeof(decimal), (x, y) => x.TryParseDecimal((decimal)y) },
            { typeof(bool), (x, y) => x.TryParseBoolean((bool)y) },
            { typeof(DateTime), (x, y) => x.TryParseDateTime((DateTime)y) },
            { typeof(Guid), (x, y) => x.TryParseGuid((Guid)y) }
        };

    #endregion private fields
}