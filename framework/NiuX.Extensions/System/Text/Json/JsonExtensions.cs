namespace System.Text.Json;

/// <summary>
/// Json 扩展方法
/// </summary>
public static class JsonExtensions
{
    /// <summary>
    /// Gets or sets the default json serializer options.
    /// </summary>
    /// <value>
    /// The default json serializer options.
    /// </value>
    public static JsonSerializerOptions? DefaultJsonSerializerOptions { get; set; }

    /// <summary>
    /// 从 Json 反序列化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    //public static T? FromJson<T>(this string str) => JsonConvert.DeserializeObject<T>(str);
    public static T? FromJson<T>(this string str) => JsonSerializer.Deserialize<T>(str);

    public static object? FromJson(this string str, Type type) => JsonSerializer.Deserialize(str, type);

    /// <summary>
    /// 从 Json 反序列化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="str">The string.</param>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    //public static T? FromJson<T>(this string str, Type type) => (T?)JsonConvert.DeserializeObject(str, type);
    public static T? FromJson<T>(this string str, Type type) => (T)JsonSerializer.Deserialize(str, type);

    /// <summary>
    /// 序列化成 Json
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns></returns>
    public static string ToJson(this object obj) => JsonSerializer.Serialize(obj, DefaultJsonSerializerOptions);


    /// <summary>
    /// 序列化成 Json
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <param name="options">The options.</param>
    /// <returns></returns>
    public static string ToJson(this object obj, JsonSerializerOptions options) => JsonSerializer.Serialize(obj, options);
}