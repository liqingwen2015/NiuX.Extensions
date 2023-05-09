namespace System.Collections.Generic;

/// <summary>
/// Func Extensions
/// </summary>
public static class NiuXFuncExtensions
{
    /// <summary>
    /// 生成前 count 项
    /// </summary>
    /// <param name="func">通项公式</param>
    /// <param name="count">生成的数量</param>
    /// <returns>
    /// 队列前count项
    /// </returns>
    public static IEnumerable<int> GenerateSequence(this Func<int, int> func, int count)
    {
        for (var i = 0; i < count; i++)
        {
            yield return func(i);
        }
    }
}