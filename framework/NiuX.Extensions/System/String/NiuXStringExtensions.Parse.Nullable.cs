// ReSharper disable CheckNamespace

namespace System;

/// <summary>
/// 
/// </summary>
public static partial class NiuXStringExtensions
{
    /// <summary>
    /// Parses the nullable byte.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static byte? ParseNullableByte(this string str)
    {
        return str.IsNullOrEmpty() ? default : str.ParseByte();
    }

    /// <summary>
    /// Parses the nullable int.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static int? ParseNullableInt(this string str)
    {
        return str.IsNullOrEmpty() ? default : str.ParseInt();
    }

    /// <summary>
    /// Parses the nullable long.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static long? ParseNullableLong(this string str)
    {
        return str.IsNullOrEmpty() ? default : str.ParseLong();
    }

    /// <summary>
    /// Parses the nullable decimal.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static decimal? ParseNullableDecimal(this string str)
    {
        return str.IsNullOrEmpty() ? default : str.ParseDecimal();
    }

    /// <summary>
    /// Parses the nullable short.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static short? ParseNullableShort(this string str)
    {
        return str.IsNullOrEmpty() ? default : str.ParseShort();
    }

    /// <summary>
    /// Parses the nullable float.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static float? ParseNullableFloat(this string str)
    {
        return str.IsNullOrEmpty() ? default : str.ParseFloat();
    }

    /// <summary>
    /// Parses the nullable bool.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static bool? ParseNullableBool(this string str)
    {
        return str.IsNullOrEmpty() ? default : str.ParseBool();
    }

    /// <summary>
    /// Parses the nullable date time.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static DateTime? ParseNullableDateTime(this string str)
    {
        return str.IsNullOrEmpty() ? default : str.ParseDateTime();
    }

    /// <summary>
    /// Parses the nullable unique identifier.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static Guid? ParseNullableGuid(this string str)
    {
        return str.IsNullOrEmpty() ? default : str.ParseGuid();
    }

    /// <summary>
    /// Parses the nullable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static T? ParseNullable<T>(this string str) where T : struct
    {
        return str.IsNullOrEmpty() ? default : str.Parse<T>();
    }
}