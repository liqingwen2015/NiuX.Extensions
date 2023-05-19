using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace NiuX.Data;

/// <summary>
/// DbDataReader Extensions
/// </summary>
public static class DbDataReaderExtensions
{
    /// <summary>
    /// 读取并转换成集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="reader">The reader.</param>
    /// <returns></returns>
    public static List<T> ReadAsList<T>(this DbDataReader reader) where T : new() => Inner.ReadAsList<T>(reader);

    /// <summary>
    /// 读取并转换成兼容性集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="reader">The reader.</param>
    /// <returns></returns>
    public static List<T> ReadAsCompatibleList<T>(this DbDataReader reader) where T : new() => Inner.ReadAsCompatibleList<T>(reader);


    /// <summary>
    /// Inner
    /// </summary>
    private static class Inner
    {
        /// <summary>
        /// The database data reader type
        /// </summary>
        private static readonly Type DbDataReaderType = typeof(DbDataReader);

        /// <summary>
        /// The parameter expression
        /// </summary>
        private static readonly ParameterExpression ParameterExpression = Expression.Parameter(DbDataReaderType);

        /// <summary>
        /// Reads as list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        internal static List<T> ReadAsList<T>(DbDataReader reader) where T : new() => InnerCore<T>.Main.ReadAsList(reader);

        /// <summary>
        /// Reads as compatible list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        internal static List<T> ReadAsCompatibleList<T>(DbDataReader reader) where T : new() => InnerCore<T>.Compatibility.ReadAsList(reader);

        /// <summary>
        /// Inner Core
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private static class InnerCore<T> where T : new()
        {
            #region private fields

            /// <summary>
            /// The readers
            /// </summary>
            private static readonly ConcurrentDictionary<string, Func<DbDataReader, List<T>>> Readers = new();

            /// <summary>
            /// The compatible readers
            /// </summary>
            private static readonly ConcurrentDictionary<string, Func<DbDataReader, List<T>>> CompatibleReaders = new();

            /// <summary>
            /// The list type
            /// </summary>
            private static readonly Type ListType = typeof(List<T>);

            /// <summary>
            /// The list variable
            /// </summary>
            private static readonly ParameterExpression ListVariable = Expression.Variable(ListType);

            /// <summary>
            /// The list label target
            /// </summary>
            private static readonly LabelTarget ListLabelTarget = Expression.Label(ListType);

            /// <summary>
            /// The model type
            /// </summary>
            private static readonly Type ModelType = typeof(T);

            /// <summary>
            /// The property infos
            /// </summary>
            private static readonly PropertyInfo[] PropertyInfos = ModelType.GetProperties();

            #endregion

            /// <summary>
            /// 主要的
            /// </summary>
            internal static class Main
            {
                /// <summary>
                /// 读取并转换为集合
                /// </summary>
                /// <param name="dataRecord">The data record.</param>
                /// <returns></returns>
                internal static List<T> ReadAsList(DbDataReader dataRecord) =>
                    InnerCore<T>.ReadAsList(dataRecord, Readers, x =>
                        Expression.Call(ParameterExpression, DbDataReaderType.GetMethod("Get" + TypeAlias[x.Value.PropertyType], new[] { typeof(int) })!, Expression.Constant(x.Key, typeof(int))));
            }

            /// <summary>
            /// 兼容
            /// </summary>
            internal static class Compatibility
            {
                /// <summary>
                /// Reads as list.
                /// </summary>
                /// <param name="dataRecord">The data record.</param>
                /// <returns></returns>
                internal static List<T> ReadAsList(DbDataReader dataRecord) =>
                    InnerCore<T>.ReadAsList(dataRecord, CompatibleReaders, x =>
                        Expression.Call(null, typeof(ConvertExt).GetMethod("TryTo" + TypeAlias[x.Value.PropertyType], new[] { typeof(object) })!,
                            Expression.Call(ParameterExpression, DbDataReaderType.GetMethod("GetValue", new[] { typeof(int) })!, Expression.Constant(x.Key, typeof(int)))));
            }

            /// <summary>
            /// 读取并转换为集合
            /// </summary>
            /// <param name="reader">The reader.</param>
            /// <param name="dbDataReaders">The database data readers.</param>
            /// <param name="func">The function.</param>
            /// <returns></returns>
            private static List<T> ReadAsList(DbDataReader reader, ConcurrentDictionary<string, Func<DbDataReader, List<T>>> dbDataReaders, Func<KeyValuePair<int, PropertyInfo>, Expression> func) =>
                dbDataReaders.GetOrAdd(string.Concat(reader.GetAllFields()),
                    _ => Expression.Lambda<Func<DbDataReader, List<T>>>(
                        Expression.Block(new List<ParameterExpression>() { ListVariable },
                            Expression.Assign(ListVariable, Expression.New(ListType)),
                            Expression.Loop(Expression.IfThenElse(Expression.Equal(
                                    Expression.Call(ParameterExpression, DbDataReaderType.GetMethod("Read")!),
                                    Expression.Constant(true)),
                                Expression.Call(ListVariable,
                                    ListType.GetMethod("Add", new[] { ModelType })!,
                                    CreateExpressionOfMemberInit(reader, func)),
                                Expression.Break(ListLabelTarget, ListVariable))),
                            Expression.Label(ListLabelTarget, ListVariable)), ParameterExpression).Compile())(reader);

            /// <summary>
            /// 创建成员初始化表达式
            /// </summary>
            /// <param name="reader">The reader.</param>
            /// <param name="func">The function.</param>
            /// <returns></returns>
            private static Expression CreateExpressionOfMemberInit(IDataRecord reader, Func<KeyValuePair<int, PropertyInfo>, Expression> func) =>
                Expression.MemberInit(Expression.New(ModelType), GetMapping(reader).Select(p =>
                    Expression.Bind(p.Value, Expression.Condition(
                        Expression.Call(ParameterExpression, DbDataReaderType.GetMethod("IsDBNull", new[] { typeof(int) })!,
                            Expression.Constant(p.Key, typeof(int))), Expression.Constant(p.Value.PropertyType.GetDefaultValue(), p.Value.PropertyType),
                        Expression.Convert(func(p), p.Value.PropertyType)))));

            /// <summary>
            /// 获取映射关系
            /// </summary>
            /// <param name="reader">The reader.</param>
            /// <returns></returns>
            private static Dictionary<int, PropertyInfo> GetMapping(IDataRecord reader) =>
                Enumerable.Range(0, reader.FieldCount).ToDictionary(x => x, reader.GetName)
                    .ToDictionary(x => x.Key, x => Array.Find(PropertyInfos, y => (x.Value == y.GetCustomAttribute<ColumnAttribute>()?.Name || x.Value.IsEqual(y.Name) || x.Value == y.Name.ToSnakeCase()) && TypeAlias.ContainsKey(y.PropertyType)))
                    .Where(x => x.Value != null).ToDictionary(x => x.Key, x => x.Value);
        }
    }

    /// <summary>
    /// 类型别名
    /// </summary>
    private static readonly Dictionary<Type, string> TypeAlias = new()
    {
        { typeof(short?), "Int16" },
        { typeof(short), "Int16" },
        { typeof(int?), "Int32" },
        { typeof(int), "Int32" },
        { typeof(byte?), "Byte" },
        { typeof(byte), "Byte" },
        { typeof(long?), "Int64" },
        { typeof(long), "Int64" },
        { typeof(decimal?), "Decimal" },
        { typeof(decimal), "Decimal" },
        { typeof(double?), "Double" },
        { typeof(double), "Double" },
        { typeof(string), "String" },
        { typeof(DateTime?), "DateTime" },
        { typeof(DateTime), "DateTime" },
        { typeof(bool), "Boolean" },
        { typeof(bool?), "Boolean" },
        { typeof(Guid), "Guid" },
        { typeof(Guid?), "Guid" }
    };
}