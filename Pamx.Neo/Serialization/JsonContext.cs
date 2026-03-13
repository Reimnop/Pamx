using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Pamx.Neo.Serialization.Converters.Primitives;

namespace Pamx.Neo.Serialization;

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    Converters =
    [
        typeof(ColorConverter)
    ]
)]
[JsonSerializable(typeof(Beatmap))]
public partial class JsonContext : JsonSerializerContext
{
    static JsonContext()
    {
        Default = new JsonContext(new JsonSerializerOptions(Default.GeneratedSerializerOptions!)
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });
    }
}