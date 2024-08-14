using System.Drawing;
using System.Numerics;
using System.Text.Encodings.Web;
using System.Text.Json;
using Pamx.Common;
using Pamx.Common.Data;
using Pamx.Common.Enum;
using Pamx.Common.Implementation;
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
    PrefabSpawns =
    {
        new EditorPrefabSpawn(),
        new EditorPrefabSpawn(),
        new EditorPrefabSpawn(),
        new EditorPrefabSpawn(),
        new EditorPrefabSpawn(),
        new EditorPrefabSpawn(),
    },
    Parallax =
    {
        Layers  =
        {
            new ParallaxLayer(), 
            new ParallaxLayer(), 
            new ParallaxLayer(), 
            new ParallaxLayer(), 
            new ParallaxLayer(),
        }
    },
    Events =
    {
        Movement =
        {
            new FixedKeyframe<Vector2>
            {
                Time = 0.0f,
                Value = Vector2.One * 10.0f,
            }
        },
        Zoom =
        {
            new FixedKeyframe<float>
            {
                Time = 0.0f,
                Value = 25.0f,
            }
        },
        Rotation =
        {
            new FixedKeyframe<float>
            {
                Time = 0.0f,
                Value = 10.0f,
            }
        },
        Shake =
        {
            new FixedKeyframe<float>
            {
                Time = 0.0f,
                Value = 5.0f,
            }
        },
        Theme =
        {
            new FixedKeyframe<IReference<ITheme>>
            {
                Time = 0.0f,
                Value = new VgReferenceTheme("20"),
            }
        },
        Chroma =
        {
            new FixedKeyframe<float>
            {
                Time = 0.0f,
                Value = 2.0f,
            }
        },
        Bloom =
        {
            new FixedKeyframe<BloomData>
            {
                Time = 0.0f,
                Value = new BloomData
                {
                    Intensity = 20.0f,
                    Diffusion = 0.4f,
                    Color = 1,
                },
            }
        },
        Vignette =
        {
            new FixedKeyframe<VignetteData>
            {
                Time = 0.0f,
                Value = new VignetteData(),
            }
        },
        LensDistortion =
        {
            new FixedKeyframe<LensDistortionData>
            {
                Time = 0.0f,
                Value = new LensDistortionData
                {
                    Intensity = 1.0f,
                }
            },
        },
        Grain =
        {
            new FixedKeyframe<GrainData>
            {
                Time = 0.0f,
                Value = new GrainData
                {
                    Intensity = 0.5f,
                    Size = 1.0f,
                },
            },
        },
        Gradient =
        {
            new FixedKeyframe<GradientData>
            {
                Time = 0.0f,
                Value = new GradientData
                {
                    Intensity = 2.0f,
                    Rotation = 25.0f,
                    ColorA = 0,
                    ColorB = 1,
                },
            },
        },
        Glitch =
        {
            new FixedKeyframe<GlitchData>
            {
                Time = 0.0f,
                Value = new GlitchData
                {
                    Intensity = 0.5f,
                    Speed = 0.5f,
                    Width = 0.5f,
                }
            }
        },
        Hue =
        {
            new FixedKeyframe<float>
            {
                Time = 0.0f,
                Value = 50.0f,
            }
        },
        Player =
        {
            new FixedKeyframe<Vector2>
            {
                Time = 0.0f,
                Value = Vector2.Zero,
            }
        }
    }
};

using var stream = Console.OpenStandardOutput();
using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions
{
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
});
VgSerialization.SerializeBeatmap(vgBeatmap, writer);