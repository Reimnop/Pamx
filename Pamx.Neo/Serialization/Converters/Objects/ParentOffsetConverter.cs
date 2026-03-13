using System.Text.Json;
using System.Text.Json.Serialization;
using Pamx.Neo.Objects;

namespace Pamx.Neo.Serialization.Converters.Objects;

internal sealed class ParentOffsetConverter : JsonConverter<ParentOffset>
{
    public override ParentOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, ParentOffset value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}