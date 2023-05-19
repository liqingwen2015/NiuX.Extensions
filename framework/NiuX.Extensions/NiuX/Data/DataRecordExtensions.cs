using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;

namespace NiuX.Data;

/// <summary>
/// DataRecord Extensions
/// </summary>
public static class DataRecordExtensions
{
    /// <summary>
    /// 获取所有字段
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    public static IEnumerable<string> GetAllFields(this IDataRecord reader) => Enumerable.Range(0, reader.FieldCount).Select(reader.GetName);

    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    public static string? GetField(this IDataRecord reader, Func<string, bool> func) => reader.GetAllFields().FirstOrDefault(func);

    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="property"></param>
    /// <returns></returns>
    public static string? GetField(this IDataRecord reader, MemberInfo property) =>
        property.GetCustomAttribute<ColumnAttribute>()?.Name
        ?? reader.GetField(x => x == property.Name)
        ?? reader.GetField(x => x == property.Name.ToSnakeCase());
}