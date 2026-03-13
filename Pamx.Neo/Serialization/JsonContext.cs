using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Text.Unicode;
using Pamx.Neo.Serialization.Converters.Primitives;

namespace Pamx.Neo.Serialization;

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
    Converters =
    [
        typeof(Vector2Converter),
        typeof(ColorConverter)
    ]
)]
[JsonSerializable(typeof(Beatmap))]
public partial class JsonContext : JsonSerializerContext
{
    private static JsonSerializerOptions? _customOptions;

    public static JsonSerializerOptions CustomOptions => _customOptions ??= new JsonSerializerOptions(Default.Options)
    {
        TypeInfoResolver = Default.WithAddedModifier(IgnoreEmptyStrings),
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    };
    
    private static void IgnoreEmptyStrings(JsonTypeInfo typeInfo)
    {
        foreach (var property in typeInfo.Properties)
            if (property.PropertyType == typeof(string))
                property.ShouldSerialize = (_, value) => value is not string s || s.Length > 0;
    }
}