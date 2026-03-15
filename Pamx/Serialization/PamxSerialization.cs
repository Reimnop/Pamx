using System.Text.Json;
using Pamx.Legacy;

namespace Pamx.Serialization;

public static class PamxSerialization
{
    public static JsonSerializerOptions Options => JsonContext.CustomOptions;
    public static JsonSerializerOptions LegacyOptions => LegacyJsonContext.CustomOptions;
}