using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace System.Text.Json;

/// <summary>
/// 
/// </summary>
public class JsonSerializerOptionsInstance
{
    /// <summary>
    /// The ignore null and camel case and indented
    /// </summary>
    public static JsonSerializerOptions IgnoreNullAndCamelCaseAndIndented = new() {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = true
        //Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
    };

    /// <summary>
    /// The unicode ranges all encoder
    /// </summary>
    public static JsonSerializerOptions UnicodeRangesAllEncoder = new() {
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
    };

    /// <summary>
    /// The unsafe relaxed json escaping encoder
    /// </summary>
    public static JsonSerializerOptions UnsafeRelaxedJsonEscapingEncoder = new() {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
}