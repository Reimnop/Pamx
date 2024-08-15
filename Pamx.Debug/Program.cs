using System.Numerics;
using System.Text.Encodings.Web;
using System.Text.Json;
using Pamx.Common;
using Pamx.Common.Data;
using Pamx.Common.Enum;
using Pamx.Common.Implementation;
using Pamx.Vg;

var vgBeatmap = new VgBeatmap
{
    Checkpoints =
    {
        new VgCheckpoint
        {
            Name = "Lorem ipsum dolor sit amet",
            Time = 0.0f,
            Position = new Vector2(0.0f, 0.0f),
        },
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
                Value = Vector2.Zero,
            }
        },
        Zoom =
        {
            new FixedKeyframe<float>
            {
                Time = 0.0f,
                Value = 20.0f,
            }
        },
        Rotation =
        {
            new FixedKeyframe<float>
            {
                Time = 0.0f,
                Value = 0.0f,
            }
        },
        Shake =
        {
            new FixedKeyframe<float>
            {
                Time = 0.0f,
                Value = 0.0f,
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
                Value = 0.0f,
            }
        },
        Bloom =
        {
            new FixedKeyframe<BloomData>
            {
                Time = 0.0f,
                Value = new BloomData(),
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
                Value = new LensDistortionData(),
            },
        },
        Grain =
        {
            new FixedKeyframe<GrainData>
            {
                Time = 0.0f,
                Value = new GrainData(),
            },
        },
        Gradient =
        {
            new FixedKeyframe<GradientData>
            {
                Time = 0.0f,
                Value = new GradientData(),
            },
        },
        Glitch =
        {
            new FixedKeyframe<GlitchData>
            {
                Time = 0.0f,
                Value = new GlitchData(),
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

// Generate a grid of parallax objects
var layer = vgBeatmap.Parallax.Layers[0];
for (var x = -5; x <= 5; x++)
{
    for (var y = -5; y <= 5; y++)
    {
        layer.Objects.Add(new ParallaxObject
        {
            Position = new Vector2(x, y) * 3.0f,
            Scale = new Vector2(3.0f, 3.0f),
            Animation = new ParallaxObjectAnimation
            {
                Scale = new Vector2(0.5f, 0.5f),
                LoopLength = 1.0f,
                LoopDelay = (x + y + 10) * 0.3f,
            },
            Shape = ObjectShape.Square,
            ShapeOption = (int) ObjectSquareShape.Solid,
            Color = 5,
        });
    }
}

using var stream = File.Open("level.vgd", FileMode.Create);
using var writer = JsonUtil.CreateJsonWriter(stream);
VgSerialization.SerializeBeatmap(vgBeatmap, writer);