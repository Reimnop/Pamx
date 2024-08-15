using System.Text.Encodings.Web;
using System.Text.Json;

namespace Pamx.Common.Implementation;

public static class JsonUtil
{
    public static Utf8JsonWriter CreateJsonWriter(Stream stream)
    {
        var options = new JsonWriterOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        };
        return new Utf8JsonWriter(stream, options);
    }
}