using System.Collections.Concurrent;
using System.Linq;

// ReSharper disable CheckNamespace

namespace System.Collections.Generic;

/// <summary>
/// Extension methods for Dictionary.
/// </summary>
public static class NiuXDictionaryExtensions
{
    /// <summary>
    /// This method is used to try to get a value in a dictionary if it does exists.
    /// </summary>
    /// <typeparam name="T">Type of the value</typeparam>
    /// <param name="dictionary">The collection object</param>
    /// <param name="key">Key</param>
    /// <param name="value">Value of the key (or default value if key not exists)</param>
    /// <returns>True if key does exists in the dictionary</returns>
    static internal bool TryGetValue<T>(this IDictionary<string, object> dictionary, string key, out T value)
    {
        object valueObj;
        if (dictionary.TryGetValue(key, out valueObj) && valueObj is T)
        {
            value = (T)valueObj;
            return true;
        }

        value = default;
        return false;
    }

    /// <summary>
    /// Gets a value from the dictionary with given key. Returns default value if can not find.
    /// </summary>
    /// <param name="dictionary">Dictionary to check and get</param>
    /// <param name="key">Key to find the value</param>
    /// <typeparam name="TKey">Type of the key</typeparam>
    /// <typeparam name="TValue">Type of the value</typeparam>
    /// <returns>Value if found, default if can not found.</returns>
    public static TValue GetOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
    {
        TValue obj;
        return dictionary.TryGetValue(key, out obj) ? obj : default;
    }

    /// <summary>
    /// Gets a value from the dictionary with given key. Returns default value if can not find.
    /// </summary>
    /// <param name="dictionary">Dictionary to check and get</param>
    /// <param name="key">Key to find the value</param>
    /// <typeparam name="TKey">Type of the key</typeparam>
    /// <typeparam name="TValue">Type of the value</typeparam>
    /// <returns>Value if found, default if can not found.</returns>
    public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
    {
        return dictionary.TryGetValue(key, out var obj) ? obj : default;
    }

    /// <summary>
    /// Gets a value from the dictionary with given key. Returns default value if can not find.
    /// </summary>
    /// <param name="dictionary">Dictionary to check and get</param>
    /// <param name="key">Key to find the value</param>
    /// <typeparam name="TKey">Type of the key</typeparam>
    /// <typeparam name="TValue">Type of the value</typeparam>
    /// <returns>Value if found, default if can not found.</returns>
    public static TValue GetOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key)
    {
        return dictionary.TryGetValue(key, out var obj) ? obj : default;
    }

    /// <summary>
    /// Gets a value from the dictionary with given key. Returns default value if can not find.
    /// </summary>
    /// <param name="dictionary">Dictionary to check and get</param>
    /// <param name="key">Key to find the value</param>
    /// <typeparam name="TKey">Type of the key</typeparam>
    /// <typeparam name="TValue">Type of the value</typeparam>
    /// <returns>Value if found, default if can not found.</returns>
    public static TValue GetOrDefault<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key)
    {
        return dictionary.TryGetValue(key, out var obj) ? obj : default;
    }

    /// <summary>
    /// Gets a value from the dictionary with given key. Returns default value if can not find.
    /// </summary>
    /// <param name="dictionary">Dictionary to check and get</param>
    /// <param name="key">Key to find the value</param>
    /// <param name="factory">A factory method used to create the value if not found in the dictionary</param>
    /// <typeparam name="TKey">Type of the key</typeparam>
    /// <typeparam name="TValue">Type of the value</typeparam>
    /// <returns>Value if found, default if can not found.</returns>
    public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
        Func<TKey, TValue> factory)
    {
        TValue obj;
        if (dictionary.TryGetValue(key, out obj))
        {
            return obj;
        }

        return dictionary[key] = factory(key);
    }

    /// <summary>
    /// Gets a value from the dictionary with given key. Returns default value if can not find.
    /// </summary>
    /// <param name="dictionary">Dictionary to check and get</param>
    /// <param name="key">Key to find the value</param>
    /// <param name="factory">A factory method used to create the value if not found in the dictionary</param>
    /// <typeparam name="TKey">Type of the key</typeparam>
    /// <typeparam name="TValue">Type of the value</typeparam>
    /// <returns>Value if found, default if can not found.</returns>
    public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
        Func<TValue> factory)
    {
        return dictionary.GetOrAdd(key, k => factory());
    }

    #region 获取值

    /// <summary>
    /// 获取与指定的键相关联的值，如果没有则返回输入的默认值
    /// </summary>
    public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key,
        TValue defaultValue = default)
    {
        return dict.ContainsKey(key) ? dict[key] : defaultValue;
    }

    #endregion 获取值

    #region 批量添加

    /// <summary>
    /// 向字典中批量添加键值对
    /// </summary>
    /// <param name="replaceExisted">如果已存在，是否替换</param>
    public static Dictionary<TKey, TValue> AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dict,
        IEnumerable<KeyValuePair<TKey, TValue>> values, bool replaceExisted)
    {
        foreach (var pair in values.Where(item => !dict.ContainsKey(item.Key) || replaceExisted))
        {
            dict[pair.Key] = pair.Value;
        }

        return dict;
    }

    #endregion 批量添加

    #region 向字典中添加键和值

    /// <summary>
    /// 尝试将键和值添加到字典中：如果不存在，才添加；存在，不添加也不抛导常
    /// </summary>
    public static Dictionary<TKey, TValue> TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key,
        TValue value)
    {
        if (!dict.ContainsKey(key))
        {
            dict.Add(key, value);
        }

        return dict;
    }

    /// <summary>
    /// 将键和值添加或替换到字典中：如果不存在，则添加；存在，则替换
    /// </summary>
    public static Dictionary<TKey, TValue> AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key,
        TValue value)
    {
        dict[key] = value;
        return dict;
    }

    #endregion 向字典中添加键和值
}