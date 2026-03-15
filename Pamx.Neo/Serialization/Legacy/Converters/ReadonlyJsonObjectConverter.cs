using System.Text.Json;
using Pamx.Neo.Serialization.Converters;

namespace Pamx.Neo.Serialization.Legacy.Converters;

public abstract class ReadonlyJsonObjectConverter<T> : JsonObjectConverter<T>
{
    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options) =>
        throw new InvalidOperationException("This converter is read-only and cannot write JSON data.");

    protected override void WriteProperties(Utf8JsonWriter writer, T value, JsonSerializerOptions options) =>
        throw new InvalidOperationException("This converter is read-only and cannot write JSON data.");
}