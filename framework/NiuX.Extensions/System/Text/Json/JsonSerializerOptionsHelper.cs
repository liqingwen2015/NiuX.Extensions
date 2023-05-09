using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace System.Text.Json;

/// <summary>
/// 
/// </summary>
public static class JsonSerializerOptionsHelper
{
    /// <summary>
    /// Creates the specified base options.
    /// </summary>
    /// <param name="baseOptions">The base options.</param>
    /// <param name="removeConverter">The remove converter.</param>
    /// <param name="addConverters">The add converters.</param>
    /// <returns></returns>
    public static JsonSerializerOptions Create(JsonSerializerOptions baseOptions, JsonConverter removeConverter,
        params JsonConverter[] addConverters)
    {
        return Create(baseOptions, x => x == removeConverter, addConverters);
    }

    /// <summary>
    /// Creates the specified base options.
    /// </summary>
    /// <param name="baseOptions">The base options.</param>
    /// <param name="removeConverterPredicate">The remove converter predicate.</param>
    /// <param name="addConverters">The add converters.</param>
    /// <returns></returns>
    public static JsonSerializerOptions Create(JsonSerializerOptions baseOptions,
        Func<JsonConverter, bool> removeConverterPredicate, params JsonConverter[] addConverters)
    {
        var options = new JsonSerializerOptions(baseOptions);
        options.Converters.RemoveAll(removeConverterPredicate);
        options.Converters.AddIfNotContains(addConverters);
        return options;
    }
}