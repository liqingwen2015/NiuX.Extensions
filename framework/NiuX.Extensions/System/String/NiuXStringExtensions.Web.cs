// ReSharper disable CheckNamespace

using System.Web;

namespace NiuX.String;

/// <summary>
/// 
/// </summary>
public static class NiuXStringExtensions
{
    /// <summary>
    /// Converts to htmldecode.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static string ToHtmlDecode(this string str)
    {
        return HttpUtility.HtmlDecode(str);
    }

    /// <summary>
    /// Converts to htmlencode.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static string ToHtmlEncode(this string str)
    {
        return HttpUtility.HtmlEncode(str);
    }

    /// <summary>
    /// Mays the be date time of ymd.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static bool MayBeDateTimeOfYmd(this string str)
    {
        if (str.Length < 8)
        {
            return false;
        }

        if (int.TryParse(str.Substring(0, 4), out var year) && year > 1900 &&
            int.TryParse(str.Substring(4, 2), out var month) && month is > 0 and < 13 &&
            int.TryParse(str.Substring(6, 2), out var day) && day is > 0 and < 32)
        {
            return true;
        }

        // [132]={[dba_tmp_Pack_Item_n_n_yyyyMMdd, dba_tmp_Pack_Item_1_10016927_20211026180000]}
        return false;
    }

    /// <summary>
    /// Lefts the specified text.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="leftLength">Length of the left.</param>
    /// <returns></returns>
    public static string Left(string text, int leftLength)
    {
        return string.IsNullOrEmpty(text) || text.Length <= leftLength ? text : text.Substring(0, leftLength);
    }

    /// <summary>
    /// Rights the specified text.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="rightLength">Length of the right.</param>
    /// <returns></returns>
    public static string Right(string text, int rightLength)
    {
        return string.IsNullOrEmpty(text) || text.Length <= rightLength
            ? text
            : text.Substring(text.Length - rightLength, rightLength);
    }
}