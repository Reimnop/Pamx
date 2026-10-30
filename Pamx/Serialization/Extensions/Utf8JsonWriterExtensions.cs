namespace System.Text.Json;

public static class Utf8JsonWriterExtensions
{
    /// <summary>Writes a number; one that is not finite is written as the text the game uses for it (<c>"Infinity"</c>, <c>"-Infinity"</c>, <c>"NaN"</c>), as it cannot be a JSON number.</summary>
    public static void WriteFloat(this Utf8JsonWriter writer, float value)
    {
        if (float.IsFinite(value))
            writer.WriteNumberValue(value);
        else
            writer.WriteStringValue(float.IsNaN(value) ? "NaN" : float.IsPositiveInfinity(value) ? "Infinity" : "-Infinity");
    }

    public static void WriteFloat(this Utf8JsonWriter writer, string propertyName, float value)
    {
        if (float.IsFinite(value))
            writer.WriteNumber(propertyName, value);
        else
            writer.WriteString(propertyName, float.IsNaN(value) ? "NaN" : float.IsPositiveInfinity(value) ? "Infinity" : "-Infinity");
    }

    public static void WriteFloat(this Utf8JsonWriter writer, JsonEncodedText propertyName, float value)
    {
        if (float.IsFinite(value))
            writer.WriteNumber(propertyName, value);
        else
            writer.WriteString(propertyName, float.IsNaN(value) ? "NaN" : float.IsPositiveInfinity(value) ? "Infinity" : "-Infinity");
    }
}
