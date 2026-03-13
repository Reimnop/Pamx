using System.Text.Json;
using System.Text.Json.Serialization;
using Pamx.Neo.Objects;

namespace Pamx.Neo.Serialization.Converters.Objects;

internal sealed class ParentTypeConverter : JsonConverter<ParentType>
{
    public override ParentType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, ParentType value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}