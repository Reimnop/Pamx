using System.Drawing;
using System.Numerics;
using System.Text.Encodings.Web;
using System.Text.Json;
using Pamx.Common.Data;
using Pamx.Common.Enum;
using Pamx.Vg;

var vgTheme = new VgBeatmapTheme
{
    Name = "Lorem ipsum dolor sit amet",
    Player =
    {
        Color.FromArgb(255, 12, 255, 80),
        Color.FromArgb(255, 0, 124, 136),
    },
    Object =
    {
        Color.FromArgb(255, 50, 60, 255),
        Color.FromArgb(255, 0, 0, 123),
    },
    Effect =
    {
        Color.FromArgb(255, 255, 255, 123),
        Color.FromArgb(255, 79, 0, 31),
    },
    ParallaxObject =
    {
        Color.FromArgb(255, 255, 121, 255),
        Color.FromArgb(255, 214, 0, 0),
    },
    Background = Color.FromArgb(255, 0, 0, 124),
    Gui = Color.FromArgb(255, 0, 0, 255),
    GuiAccent = Color.FromArgb(255, 0, 128, 0),
};

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
        new Keyframe<Vector2>
        {
            Time = 0.0f,
            Value = new Vector2(5.0f, 5.0f),
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

var vgCheckpoint = new VgCheckpoint
{
    Name = "Lorem ipsum dolor sit amet",
    Time = 0.0f,
    Position = new Vector2(0.0f, 0.0f),
};

var vgBeatmap = new VgBeatmap
{
    Checkpoints =
    {
        vgCheckpoint
    },
    Objects =
    {
        vgObject
    },
    Themes =
    {
        vgTheme
    }
};

using var stream = Console.OpenStandardOutput();
using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions
{
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
});
VgSerialization.SerializeBeatmap(vgBeatmap, writer);