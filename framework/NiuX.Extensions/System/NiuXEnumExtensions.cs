using System.ComponentModel;
using System.Reflection;

namespace System;

/// <summary>
/// 枚举扩展
/// </summary>
public static class NiuXEnumExtensions
{
    /// <summary>
    /// 获取描述
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        return field.IsDefined(typeof(DescriptionAttribute))
            ? field.GetCustomAttribute(typeof(DescriptionAttribute)).As<DescriptionAttribute>().Description
            : value.ToString();
    }
}