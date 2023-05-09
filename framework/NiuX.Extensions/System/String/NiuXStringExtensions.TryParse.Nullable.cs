// ReSharper disable CheckNamespace

namespace System;

/// <summary>
/// 
/// </summary>
public static partial class NiuXStringExtensions
{
    /// <summary>
    /// Tries the parse nullable byte.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static byte? TryParseNullableByte(this string str)
    {
        return str.IsNullOrEmpty() ? default : str.TryParseByte();
    }

    /// <summary>
    /// Tries the parse nullable byte.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns></returns>
    public static byte? TryParseNullableByte(this string str, byte defaultValue)
    {
        return str.IsNullOrEmpty() ? defaultValue : str.TryParseByte(defaultValue);
    }

    /// <summary>
    /// Tries the parse nullable int.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static int? TryParseNullableInt(this string str)
    {
        return str.IsNullOrEmpty() ? default : str.TryParseInt();
    }

    /// <summary>
    /// Tries the parse nullable int.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns></returns>
    public static int? TryParseNullableInt(this string str, int defaultValue)
    {
        return str.IsNullOrEmpty() ? defaultValue : str.TryParseInt(defaultValue);
    }

    /// <summary>
    /// Tries the parse nullable long.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static long? TryParseNullableLong(this string str)
    {
        return str.IsNullOrEmpty() ? default : str.TryParseLong();
    }

    /// <summary>
    /// Tries the parse nullable long.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns></returns>
    public static long? TryParseNullableLong(this string str, long defaultValue)
    {
        return str.IsNullOrEmpty() ? defaultValue : str.TryParseLong(defaultValue);
    }

    /// <summary>
    /// Tries the parse nullable decimal.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static decimal? TryParseNullableDecimal(this string str)
    {
        return str.IsNullOrEmpty() ? default : str.TryParseDecimal();
    }

    /// <summary>
    /// Tries the parse nullable decimal.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns></returns>
    public static decimal? TryParseNullableDecimal(this string str, decimal defaultValue)
    {
        return str.IsNullOrEmpty() ? defaultValue : str.TryParseDecimal(defaultValue);
    }

    /// <summary>
    /// Tries the parse nullable short.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static short? TryParseNullableShort(this string str)
    {
        return str.IsNullOrEmpty() ? default : str.TryParseShort();
    }

    /// <summary>
    /// Tries the parse nullable short.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns></returns>
    public static short? TryParseNullableShort(this string str, short defaultValue)
    {
        return str.IsNullOrEmpty() ? defaultValue : str.TryParseShort(defaultValue);
    }

    /// <summary>
    /// Tries the parse nullable float.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static float? TryParseNullableFloat(this string str)
    {
        return str.IsNullOrEmpty() ? default : str.TryParseFloat();
    }

    /// <summary>
    /// Tries the parse nullable float.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns></returns>
    public static float? TryParseNullableFloat(this string str, float defaultValue)
    {
        return str.IsNullOrEmpty() ? defaultValue : str.TryParseFloat(defaultValue);
    }

    /// <summary>
    /// Tries the parse nullable boolean.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static bool? TryParseNullableBoolean(this string str)
    {
        return str.IsNullOrEmpty() ? default : str.TryParseBoolean();
    }

    /// <summary>
    /// Tries the parse nullable boolean.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="defaultValue">if set to <c>true</c> [default value].</param>
    /// <returns></returns>
    public static bool? TryParseNullableBoolean(this string str, bool defaultValue)
    {
        return str.IsNullOrEmpty() ? defaultValue : str.TryParseBoolean(defaultValue);
    }

    /// <summary>
    /// Tries the parse nullable date time.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static DateTime? TryParseNullableDateTime(this string str)
    {
        return str.IsNullOrEmpty() ? default : str.TryParseDateTime();
    }

    /// <summary>
    /// Tries the parse nullable date time.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns></returns>
    public static DateTime? TryParseNullableDateTime(this string str, DateTime defaultValue)
    {
        return str.IsNullOrEmpty() ? defaultValue : str.TryParseDateTime(defaultValue);
    }

    /// <summary>
    /// Tries the parse nullable unique identifier.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static Guid? TryParseNullableGuid(this string str)
    {
        return str.IsNullOrEmpty() ? default : str.TryParseGuid();
    }

    /// <summary>
    /// Tries the parse nullable unique identifier.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns></returns>
    public static Guid? TryParseNullableGuid(this string str, Guid defaultValue)
    {
        return str.IsNullOrEmpty() ? defaultValue : str.TryParseGuid(defaultValue);
    }

    /// <summary>
    /// Tries the parse nullable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static T? TryParseNullable<T>(this string str) where T : struct
    {
        return str.IsNullOrEmpty() ? default : str.TryParse<T>();
    }

    /// <summary>
    /// Tries the parse nullable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="str">The string.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns></returns>
    public static T? TryParseNullable<T>(this string str, T defaultValue) where T : struct
    {
        return str.IsNullOrEmpty() ? defaultValue : str.TryParse(defaultValue);
    }
}