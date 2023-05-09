using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NiuX.Extensions;

/// <summary>
/// DataTable Extensions
/// </summary>
public static class DataTableExtensions
{
    /// <summary>
    /// 将 DataTable 转 List
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dataTable">DataTable</param>
    /// <returns>
    /// object
    /// </returns>
    public static List<T> ToList<T>(this DataTable dataTable) where T : new()
    {
        var list = new List<T>();

        // 获取所有的数据列和类公开实例属性
        var dataColumns = dataTable.Columns;
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var columnNameToPropertyInfos = new Dictionary<string, PropertyInfo>();

        // 遍历所有行
        foreach (DataRow dataRow in dataTable.Rows)
        {
            var model = new T();

            if (columnNameToPropertyInfos.Count > 0)
            {
                foreach (var propertyInfo in columnNameToPropertyInfos)
                {
                    // 获取列值
                    var columnValue = dataRow[propertyInfo.Key];
                    // 如果列值未空，则跳过
                    if (columnValue == DBNull.Value)
                    {
                        continue;
                    }

                    // 转换成目标类型数据
                    propertyInfo.Value.SetValue(model, columnValue);
                }
            }
            else
            {
                // 遍历所有属性并一一赋值
                foreach (var property in properties)
                {
                    // 获取属性对应的真实列名
                    var columnName = property.Name;
                    if (property.IsDefined(typeof(ColumnAttribute), true))
                    {
                        var columnAttribute = property.GetCustomAttribute<ColumnAttribute>(true);
                        if (!string.IsNullOrWhiteSpace(columnAttribute.Name))
                        {
                            columnName = columnAttribute.Name;
                        }
                    }

                    // 如果 DataTable 不包含该列名，则跳过
                    if (!dataColumns.Contains(columnName))
                    {
                        var splitColumnName = string.Join("_", property.Name.SplitCamelCase());
                        if (dataColumns.Contains(splitColumnName))
                        {
                            columnName = splitColumnName;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    columnNameToPropertyInfos.Add(columnName, property);

                    // 获取列值
                    var columnValue = dataRow[columnName];
                    // 如果列值未空，则跳过
                    if (columnValue == DBNull.Value)
                    {
                        continue;
                    }

                    // 转换成目标类型数据
                    property.SetValue(model, columnValue);
                }
            }

            list.Add(model);
        }

        dataTable.Dispose();
        return list;
    }

    /// <summary>
    /// 将 DataTable 转 List
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dataTableTask">The data table task.</param>
    /// <returns>
    /// object
    /// </returns>
    public async static Task<List<T>> ToListAsync<T>(this Task<DataTable> dataTableTask) where T : new()
    {
        var list = new List<T>();
        using var dataTable = await dataTableTask;

        // 获取所有的数据列和类公开实例属性
        var dataColumns = dataTable.Columns;
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var columnNameToPropertyInfos = new Dictionary<string, PropertyInfo>();

        // 遍历所有行
        foreach (DataRow dataRow in dataTable.Rows)
        {
            var model = new T();

            if (columnNameToPropertyInfos.Count > 0)
            {
                foreach (var propertyInfo in columnNameToPropertyInfos)
                {
                    // 获取列值
                    var columnValue = dataRow[propertyInfo.Key];
                    // 如果列值未空，则跳过
                    if (columnValue == DBNull.Value)
                    {
                        continue;
                    }

                    // 转换成目标类型数据
                    propertyInfo.Value.SetValue(model, columnValue);
                }
            }
            else
            {
                // 遍历所有属性并一一赋值
                foreach (var property in properties)
                {
                    // 获取属性对应的真实列名
                    var columnName = property.Name;
                    if (property.IsDefined(typeof(ColumnAttribute), true))
                    {
                        var columnAttribute = property.GetCustomAttribute<ColumnAttribute>(true);
                        if (!string.IsNullOrWhiteSpace(columnAttribute.Name))
                        {
                            columnName = columnAttribute.Name;
                        }
                    }

                    // 如果 DataTable 不包含该列名，则跳过
                    if (!dataColumns.Contains(columnName))
                    {
                        var splitColumnName = string.Join("_", property.Name.SplitCamelCase());
                        if (dataColumns.Contains(splitColumnName))
                        {
                            columnName = splitColumnName;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    columnNameToPropertyInfos.Add(columnName, property);

                    // 获取列值
                    var columnValue = dataRow[columnName];
                    // 如果列值未空，则跳过
                    if (columnValue == DBNull.Value)
                    {
                        continue;
                    }

                    // 转换成目标类型数据
                    property.SetValue(model, columnValue);
                }
            }

            list.Add(model);
        }

        return list;
    }

    /// <summary>
    /// 切割骆驼命名式字符串
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    static internal string[] SplitCamelCase(this string str)
    {
        if (str == null)
        {
            return Array.Empty<string>();
        }

        if (string.IsNullOrWhiteSpace(str))
        {
            return new[] { str };
        }

        if (str.Length == 1)
        {
            return new[] { str };
        }

        return Regex.Split(str, @"(?=\p{Lu}\p{Ll})|(?<=\p{Ll})(?=\p{Lu})")
            .Where(u => u.Length > 0)
            .ToArray();
    }
}