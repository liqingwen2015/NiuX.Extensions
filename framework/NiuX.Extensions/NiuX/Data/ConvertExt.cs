using System;

namespace NiuX.Data;

/// <summary>
/// 转换方法扩展
/// </summary>
internal static class ConvertExt
{
    /// <summary>
    /// Converts to int32.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns></returns>
    public static int ToInt32(object obj) => obj is int value ? value : Convert.ToInt32(obj);

    /// <summary>
    /// Converts to int64.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns></returns>
    public static long ToInt64(object obj) => obj is long value ? value : Convert.ToInt64(obj);

    /// <summary>
    /// Converts to datetime.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns></returns>
    public static DateTime ToDateTime(object obj) => obj is DateTime value ? value : Convert.ToDateTime(obj);

    /// <summary>
    /// Converts to boolean.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns></returns>
    public static bool ToBoolean(object obj) => obj is bool value ? value : Convert.ToBoolean(obj);

    /// <summary>
    /// Converts to decimal.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns></returns>
    public static decimal ToDecimal(object obj) => obj is decimal value ? value : Convert.ToDecimal(obj);

    /// <summary>
    /// Converts to double.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns></returns>
    public static double ToDouble(object obj) => obj is double value ? value : Convert.ToDouble(obj);

    /// <summary>
    /// Converts to single.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns></returns>
    public static float ToSingle(object obj) => obj is float value ? value : Convert.ToSingle(obj);

    /// <summary>
    /// Converts to float.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns></returns>
    public static float ToFloat(object obj) => ToSingle(obj);

    #region Try

    /// <summary>
    /// Tries to int32.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns></returns>
    public static int TryToInt32(object obj) => obj is int value ? value : ((string)obj).TryParseInt();

    /// <summary>
    /// Tries to int64.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns></returns>
    public static long TryToInt64(object obj) => obj is long value ? value : ((string)obj).TryParseLong();

    /// <summary>
    /// Tries to unique identifier.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns></returns>
    public static Guid TryToGuid(object obj) => obj is Guid value ? value : ((string)obj).TryParseGuid();

    /// <summary>
    /// Tries to date time.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns></returns>
    public static DateTime TryToDateTime(object obj) => obj is DateTime value ? value : ((string)obj).TryParseDateTime();

    /// <summary>
    /// Tries to date time offset.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns></returns>
    public static DateTimeOffset TryToDateTimeOffset(object obj) => obj is DateTimeOffset value ? value : ((string)obj).TryParseDateTimeOffset();

    /// <summary>
    /// Tries to boolean.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns></returns>
    public static bool TryToBoolean(object obj) => obj is bool value ? value : ((string)obj).TryParseBoolean();

    /// <summary>
    /// Tries to decimal.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns></returns>
    public static decimal TryToDecimal(object obj) => obj is decimal value ? value : ((string)obj).TryParseDecimal();

    /// <summary>
    /// Tries to double.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns></returns>
    public static double TryToDouble(object obj) => obj is double value ? value : ((string)obj).TryParseDouble();

    /// <summary>
    /// Tries to single.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns></returns>
    public static float TryToSingle(object obj) => obj is float value ? value : ((string)obj).TryParseFloat();

    /// <summary>
    /// Tries to float.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns></returns>
    public static float TryToFloat(object obj) => TryToSingle(obj);

    #endregion


}