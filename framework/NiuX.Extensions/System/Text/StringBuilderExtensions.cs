namespace System.Text;

/// <summary>
/// 
/// </summary>
public static class StringBuilderExtensions
{
    /// <summary>
    /// Trims the end.
    /// </summary>
    /// <param name="sb">The sb.</param>
    /// <param name="trimEOL">if set to <c>true</c> [trim eol].</param>
    /// <returns></returns>
    public static StringBuilder TrimEnd(this StringBuilder sb, bool trimEOL = true)
    {
        if (sb.Length == 0)
        {
            return sb;
        }

        int index;
        for (index = sb.Length - 1; index >= 0; --index)
        {
            var c = sb[index];
            if (!char.IsWhiteSpace(c) || (!trimEOL && (c == '\n' || c == '\r')))
            {
                break;
            }
        }

        if (index < sb.Length - 1)
        {
            sb.Length = index + 1;
        }

        return sb;
    }

    /// <summary>
    /// 移除最后一个字符
    /// </summary>
    /// <param name="sb">The sb.</param>
    /// <returns></returns>
    public static StringBuilder RemoveLastChar(this StringBuilder sb)
    {
        if (sb.Length == 0)
        {
            return sb;
        }

        sb.Remove(sb.Length - 1, 1);
        return sb;
    }
}