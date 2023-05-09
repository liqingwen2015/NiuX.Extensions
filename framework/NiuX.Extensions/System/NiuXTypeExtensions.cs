using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using NiuX;

namespace System;

/// <summary>
/// Type Extensions
/// </summary>
public static partial class NiuXTypeExtensions
{
    /// <summary>
    /// Gets the full name of the name with assembly.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public static string GetFullNameWithAssemblyName(this Type type)
    {
        return type.FullName + ", " + type.Assembly.GetName().Name;
    }

    /// <summary>
    /// Determines whether an instance of this type can be assigned to
    /// an instance of the <typeparamref name="TTarget"></typeparamref>.
    /// Internally uses <see cref="Type.IsAssignableFrom" />.
    /// </summary>
    /// <typeparam name="TTarget">Target type</typeparam>
    /// <param name="type">The type.</param>
    /// <returns>
    ///   <c>true</c> if [is assignable to] [the specified type]; otherwise, <c>false</c>.
    /// </returns>
    /// (as reverse).
    public static bool IsAssignableTo<TTarget>([NotNull] this Type type)
    {
        Checker.NotNull(type, nameof(type));

        return type.IsAssignableTo(typeof(TTarget));
    }

    /// <summary>
    /// Determines whether an instance of this type can be assigned to
    /// an instance of the <paramref name="targetType"></paramref>.
    /// Internally uses <see cref="Type.IsAssignableFrom" /> (as reverse).
    /// </summary>
    /// <param name="type">this type</param>
    /// <param name="targetType">Target type</param>
    /// <returns>
    ///   <c>true</c> if [is assignable to] [the specified target type]; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsAssignableTo([NotNull] this Type type, [NotNull] Type targetType)
    {
        Checker.NotNull(type, nameof(type));
        Checker.NotNull(targetType, nameof(targetType));

        return targetType.IsAssignableFrom(type);
    }

    /// <summary>
    /// Gets all base classes of this type.
    /// </summary>
    /// <param name="type">The type to get its base classes.</param>
    /// <param name="includeObject">True, to include the standard <see cref="object" /> type in the returned array.</param>
    /// <returns></returns>
    public static Type[] GetBaseClasses([NotNull] this Type type, bool includeObject = true)
    {
        Checker.NotNull(type, nameof(type));

        var types = new List<Type>();
        AddTypeAndBaseTypesRecursively(types, type.BaseType, includeObject);
        return types.ToArray();
    }

    /// <summary>
    /// Gets all base classes of this type.
    /// </summary>
    /// <param name="type">The type to get its base classes.</param>
    /// <param name="stoppingType">A type to stop going to the deeper base classes. This type will be be included in the returned array</param>
    /// <param name="includeObject">True, to include the standard <see cref="object" /> type in the returned array.</param>
    /// <returns></returns>
    public static Type[] GetBaseClasses([NotNull] this Type type, Type stoppingType, bool includeObject = true)
    {
        Checker.NotNull(type, nameof(type));

        var types = new List<Type>();
        AddTypeAndBaseTypesRecursively(types, type.BaseType, includeObject, stoppingType);
        return types.ToArray();
    }

    /// <summary>
    /// Adds the type and base types recursively.
    /// </summary>
    /// <param name="types">The types.</param>
    /// <param name="type">The type.</param>
    /// <param name="includeObject">if set to <c>true</c> [include object].</param>
    /// <param name="stoppingType">Type of the stopping.</param>
    private static void AddTypeAndBaseTypesRecursively(
        [NotNull] List<Type> types,
        [CanBeNull] Type type,
        bool includeObject,
        [CanBeNull] Type stoppingType = null)
    {
        if (type == null || type == stoppingType)
        {
            return;
        }

        if (!includeObject && type == typeof(object))
        {
            return;
        }

        AddTypeAndBaseTypesRecursively(types, type.BaseType, includeObject, stoppingType);
        types.Add(type);
    }
}

public static partial class NiuXTypeExtensions
{
    /// <summary>
    /// The floating types
    /// </summary>
    private readonly static HashSet<Type> FloatingTypes = new() { typeof(float), typeof(double), typeof(decimal) };

    /// <summary>
    /// The non nullable primitive types
    /// </summary>
    private readonly static HashSet<Type> NonNullablePrimitiveTypes = new() {
        typeof(byte),
        typeof(short),
        typeof(int),
        typeof(long),
        typeof(sbyte),
        typeof(ushort),
        typeof(uint),
        typeof(ulong),
        typeof(bool),
        typeof(float),
        typeof(decimal),
        typeof(DateTime),
        typeof(DateTimeOffset),
        typeof(TimeSpan),
        typeof(Guid)
    };

    /// <summary>
    /// Determines whether [is non nullable primitive type].
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>
    ///   <c>true</c> if [is non nullable primitive type] [the specified type]; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsNonNullablePrimitiveType(this Type type)
    {
        return NonNullablePrimitiveTypes.Contains(type);
    }

    /// <summary>
    /// Determines whether this instance is function.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns>
    ///   <c>true</c> if the specified object is function; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsFunc(this object obj)
    {
        if (obj == null)
        {
            return false;
        }

        var type = obj.GetType();
        if (!type.GetTypeInfo().IsGenericType)
        {
            return false;
        }

        return type.GetGenericTypeDefinition() == typeof(Func<>);
    }

    /// <summary>
    /// Determines whether this instance is function.
    /// </summary>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    /// <param name="obj">The object.</param>
    /// <returns>
    ///   <c>true</c> if the specified object is function; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsFunc<TReturn>(this object obj)
    {
        return obj != null && obj.GetType() == typeof(Func<TReturn>);
    }

    /// <summary>
    /// Determines whether [is primitive extended] [the specified include nullables].
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="includeNullables">if set to <c>true</c> [include nullables].</param>
    /// <param name="includeEnums">if set to <c>true</c> [include enums].</param>
    /// <returns>
    ///   <c>true</c> if [is primitive extended] [the specified include nullables]; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsPrimitiveExtended(this Type type, bool includeNullables = true, bool includeEnums = false)
    {
        if (IsPrimitiveExtendedInternal(type, includeEnums))
        {
            return true;
        }

        if (includeNullables && IsNullable(type) && type.GenericTypeArguments.Any())
        {
            return IsPrimitiveExtendedInternal(type.GenericTypeArguments[0], includeEnums);
        }

        return false;
    }

    /// <summary>
    /// Determines whether this instance is nullable.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>
    ///   <c>true</c> if the specified type is nullable; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsNullable(this Type type)
    {
        return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
    }

    /// <summary>
    /// Gets the first generic argument if nullable.
    /// </summary>
    /// <param name="t">The t.</param>
    /// <returns></returns>
    public static Type GetFirstGenericArgumentIfNullable(this Type t)
    {
        if (t.GetGenericArguments().Length > 0 && t.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            return t.GetGenericArguments().FirstOrDefault();
        }

        return t;
    }

    /// <summary>
    /// Determines whether [is primitive extended internal] [the specified include enums].
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="includeEnums">if set to <c>true</c> [include enums].</param>
    /// <returns>
    ///   <c>true</c> if [is primitive extended internal] [the specified include enums]; otherwise, <c>false</c>.
    /// </returns>
    private static bool IsPrimitiveExtendedInternal(this Type type, bool includeEnums)
    {
        if (type.IsPrimitive)
        {
            return true;
        }

        if (includeEnums && type.IsEnum)
        {
            return true;
        }

        return type == typeof(string) ||
               type == typeof(decimal) ||
               type == typeof(DateTime) ||
               type == typeof(DateTimeOffset) ||
               type == typeof(TimeSpan) ||
               type == typeof(Guid);
    }

    /// <summary>
    /// Gets the default value.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public static object GetDefaultValue(this Type type)
    {
        if (type.IsValueType)
        {
            return Activator.CreateInstance(type);
        }

        return null;
    }

    /// <summary>
    /// Gets the full name handling nullable and generics.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public static string GetFullNameHandlingNullableAndGenerics([NotNull] this Type type)
    {
        Checker.NotNull(type, nameof(type));

        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            return type.GenericTypeArguments[0].FullName + "?";
        }

        if (type.IsGenericType)
        {
            var genericType = type.GetGenericTypeDefinition();
            return
                $"{genericType.FullName.Left(genericType.FullName.IndexOf('`'))}<{type.GenericTypeArguments.Select(GetFullNameHandlingNullableAndGenerics).JoinAsString(",")}>";
        }

        return type.FullName ?? type.Name;
    }

    /// <summary>
    /// Gets the name of the simplified.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public static string GetSimplifiedName([NotNull] this Type type)
    {
        Checker.NotNull(type, nameof(type));

        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            return GetSimplifiedName(type.GenericTypeArguments[0]) + "?";
        }

        if (type.IsGenericType)
        {
            var genericType = type.GetGenericTypeDefinition();
            return
                $"{genericType.FullName.Left(genericType.FullName.IndexOf('`'))}<{type.GenericTypeArguments.Select(GetSimplifiedName).JoinAsString(",")}>";
        }

        if (type == typeof(string))
        {
            return "string";
        }

        if (type == typeof(int))
        {
            return "number";
        }

        if (type == typeof(long))
        {
            return "number";
        }

        if (type == typeof(bool))
        {
            return "boolean";
        }

        if (type == typeof(char))
        {
            return "string";
        }

        if (type == typeof(double))
        {
            return "number";
        }

        if (type == typeof(float))
        {
            return "number";
        }

        if (type == typeof(decimal))
        {
            return "number";
        }

        if (type == typeof(DateTime))
        {
            return "string";
        }

        if (type == typeof(DateTimeOffset))
        {
            return "string";
        }

        if (type == typeof(TimeSpan))
        {
            return "string";
        }

        if (type == typeof(Guid))
        {
            return "string";
        }

        if (type == typeof(byte))
        {
            return "number";
        }

        if (type == typeof(sbyte))
        {
            return "number";
        }

        if (type == typeof(short))
        {
            return "number";
        }

        if (type == typeof(ushort))
        {
            return "number";
        }

        if (type == typeof(uint))
        {
            return "number";
        }

        if (type == typeof(ulong))
        {
            return "number";
        }

        if (type == typeof(IntPtr))
        {
            return "number";
        }

        if (type == typeof(UIntPtr))
        {
            return "number";
        }

        if (type == typeof(object))
        {
            return "object";
        }

        return type.FullName ?? type.Name;
    }

    /// <summary>
    /// Determines whether [is floating type] [the specified include nullable].
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="includeNullable">if set to <c>true</c> [include nullable].</param>
    /// <returns>
    ///   <c>true</c> if [is floating type] [the specified include nullable]; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsFloatingType(this Type type, bool includeNullable = true)
    {
        if (FloatingTypes.Contains(type))
        {
            return true;
        }

        if (includeNullable &&
            IsNullable(type) &&
            FloatingTypes.Contains(type.GenericTypeArguments[0]))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Converts from.
    /// </summary>
    /// <typeparam name="TTargetType">The type of the target type.</typeparam>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static object ConvertFrom<TTargetType>(this object value)
    {
        return ConvertFrom(typeof(TTargetType), value);
    }

    /// <summary>
    /// Converts from.
    /// </summary>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static object ConvertFrom(this Type targetType, object value)
    {
        return TypeDescriptor
            .GetConverter(targetType)
            .ConvertFrom(value);
    }

    /// <summary>
    /// Strips the nullable.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public static Type StripNullable(this Type type)
    {
        return IsNullable(type)
            ? type.GenericTypeArguments[0]
            : type;
    }

    /// <summary>
    /// Determines whether [is default value].
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns>
    ///   <c>true</c> if [is default value] [the specified object]; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsDefaultValue([CanBeNull] this object obj)
    {
        if (obj == null)
        {
            return true;
        }

        return obj.Equals(GetDefaultValue(obj.GetType()));
    }
}

public static partial class NiuXTypeExtensions
{
    /// <summary>
    /// Collection of numeric types.
    /// </summary>
    private readonly static List<Type> NumericTypes = new() {
        typeof(decimal),
        typeof(byte),
        typeof(sbyte),
        typeof(short),
        typeof(ushort),
        typeof(int),
        typeof(uint),
        typeof(long),
        typeof(ulong),
        typeof(float),
        typeof(double)
    };

    /// <summary>
    /// Collection of numeric types.
    /// </summary>
    private readonly static List<Type> NullabledNumericTypes = new() {
        typeof(decimal?),
        typeof(byte?),
        typeof(sbyte?),
        typeof(short?),
        typeof(ushort?),
        typeof(int?),
        typeof(uint?),
        typeof(long?),
        typeof(ulong?),
        typeof(float?),
        typeof(double?)
    };
    ///// <summary>
    ///// 是否可空类型
    ///// </summary>
    ///// <param name="type"></param>
    ///// <returns></returns>
    //public static bool IsNullableType(this Type type) => (((type != null) && type.IsGenericType) && (type.GetGenericTypeDefinition() == typeof(Nullable<>)));

    /// <summary>
    /// 获取不可空类型
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public static Type GetNonNullableType(this Type type)
    {
        return type.IsNullable() ? type.GetGenericArguments()[0] : type;
    }

    /// <summary>
    /// 是否泛型可枚举类型
    /// </summary>
    /// <param name="enumerableType">Type of the enumerable.</param>
    /// <returns>
    ///   <c>true</c> if [is generic enumerable type] [the specified enumerable type]; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsGenericEnumerableType(this Type enumerableType)
    {
        return FindGenericType(typeof(IEnumerable<>), enumerableType) != null;
    }

    /// <summary>
    /// 获取泛型元素类型
    /// </summary>
    /// <param name="enumerableType">Type of the enumerable.</param>
    /// <param name="argumentIndex">参数下标</param>
    /// <returns></returns>
    public static Type GetGenericElementType(this Type enumerableType, int argumentIndex = 0)
    {
        var type = FindGenericType(typeof(IEnumerable<>), enumerableType);
        return type != null ? type.GetGenericArguments()[argumentIndex] : enumerableType;
    }

    /// <summary>
    /// 是否实现了某泛型类型
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="definition">The definition.</param>
    /// <returns>
    ///   <c>true</c> if [is kind of generic] [the specified definition]; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsKindOfGeneric(this Type type, Type definition)
    {
        return FindGenericType(definition, type) != null;
    }

    /// <summary>
    /// 查找泛型类型
    /// </summary>
    /// <param name="definition">The definition.</param>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public static Type FindGenericType(this Type definition, Type type)
    {
        while (type != null && type != typeof(object))
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == definition)
            {
                return type;
            }

            if (definition.IsInterface)
            {
                foreach (var type2 in type.GetInterfaces())
                {
                    var type3 = FindGenericType(definition, type2);

                    if (type3 != null)
                    {
                        return type3;
                    }
                }
            }

            type = type.BaseType;
        }

        return null;
    }

    /// <summary>
    /// Check if the given type is a numeric type.
    /// </summary>
    /// <param name="type">The type to be checked.</param>
    /// <param name="isContainNullable">if set to <c>true</c> [is contain nullable].</param>
    /// <returns>
    ///   <c>true</c> if it's numeric; otherwise <c>false</c>.
    /// </returns>
    public static bool IsNumeric(this Type type, bool isContainNullable = false)
    {
        return NumericTypes.Contains(type) || (isContainNullable && NullabledNumericTypes.Contains(type));
    }

    /// <summary>
    /// 判断为结构体
    /// </summary>
    /// <param name="targetType">Type of the target.</param>
    /// <returns>
    ///   <c>true</c> if the specified target type is structure; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsStruct(this Type targetType)
    {
        if (!targetType.IsPrimitive && !targetType.IsClass && !targetType.IsEnum && targetType.IsValueType)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 获取默认值
    /// </summary>
    /// <param name="targetType">Type of the target.</param>
    /// <returns></returns>
    public static object GetDefault(this Type targetType)
    {
        return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;
    }

    /// <summary>
    /// Gets the writeable property infos.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public static PropertyInfo[] GetWriteablePropertyInfos(this Type type)
    {
        return type.GetProperties().Where(x => x.CanWrite).ToArray();
    }

    /// <summary>
    /// Gets the readable property infos.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public static PropertyInfo[] GetReadablePropertyInfos(this Type type)
    {
        return type.GetProperties().Where(x => x.CanRead).ToArray();
    }
}