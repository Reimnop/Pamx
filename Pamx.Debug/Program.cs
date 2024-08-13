using System.Numerics;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Pamx.Common.Data;
using Pamx.Common.Enum;
using Pamx.Vg;

var vgObject = new VgObject
{
    Name = "Lorem ipsum dolor sit amet",
    StartTime = 8.0f,
    AutoKillType = AutoKillType.FixedTime,
    AutoKillOffset = 5.0f,
    PositionEvents =
    {
        new Keyframe<Vector2>
        {
            Time = 0.0f,
            Value = new Vector2(0.0f, 0.0f),
        },
    },
    ScaleEvents =
    {
        new Keyframe<Vector2>
        {
            Time = 0.0f,
            Value = new Vector2(1.0f, 1.0f),
        },
    },
    RotationEvents =
    {
        new Keyframe<float>
        {
            Time = 0.0f,
            Value = 0.0f,
        },
        new Keyframe<float>
        {
            Time = 5.0f,
            Value = 50.0f,
            RandomMode = RandomMode.Range,
            RandomValue = 80.0f,
            RandomInterval = 6.0f
        },
    },
    ColorEvents =
    {
        new FixedKeyframe<ThemeColor>
        {
            Time = 0.0f,
            Value = new ThemeColor(),
        },
    },
    EditorSettings = new ObjectEditorSettings
    {
        Bin = 5,
        Layer = 2,
        BackgroundColor = ObjectTimelineColor.Yellow,
        TextColor = ObjectTimelineColor.Red
    }
};

using var stream = Console.OpenStandardOutput();
using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions
{
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
});
VgSerialization.SerializeObject(vgObject, writer);