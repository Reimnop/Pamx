using System.Numerics;
using Pamx.Common;
using Pamx.Common.Data;
using Pamx.Common.Enum;
using Pamx.Ls;

var lsBeatmap = new LsBeatmap();
lsBeatmap.Checkpoints.Add(new Checkpoint
{
    Name = "Lorem ipsum dolor sit amet",
    Position = new Vector2(0.0f, 0.0f),
    Time = 0.0f
});
lsBeatmap.Objects.Add(new LsObject
{
    Name = "consectetur adipiscing elit",
    StartTime = 0.0f,
    AutoKillType = AutoKillType.FixedTime,
    AutoKillOffset = 5.0f,
    Shape = ObjectShape.Square,
    ShapeOption = (int) ObjectSquareShape.Solid,
    PositionEvents =
    {
        new Keyframe<Vector2>
        {
            Time = 0.0f,
            Value = new Vector2(0.0f, 0.0f)
        }
    },
    ScaleEvents =
    {
        new Keyframe<Vector2>
        {
            Time = 0.0f,
            Value = new Vector2(1.0f, 1.0f)
        }
    },
    RotationEvents =
    {
        new Keyframe<float>
        {
            Time = 0.0f,
            Value = 0.0f
        }
    },
    ColorEvents =
    {
        new FixedKeyframe<ThemeColor>
        {
            Time = 0.0f,
            Value = new ThemeColor
            {
                Index = 0,
                Opacity = 1.0f
            }
        }
    },
});
var events = lsBeatmap.Events;
events.Movement.Add(new FixedKeyframe<Vector2>
{
    Time = 0.0f,
    Value = new Vector2(0.0f, 0.0f)
});
events.Zoom.Add(new FixedKeyframe<float>
{
    Time = 0.0f,
    Value = 0.0f
});
events.Rotation.Add(new FixedKeyframe<float>
{
    Time = 0.0f,
    Value = 0.0f
});
events.Shake.Add(new FixedKeyframe<float>
{
    Time = 0.0f,
    Value = 0.0f
});
events.Theme.Add(new FixedKeyframe<IReference<ITheme>>
{
    Time = 0.0f,
    Value = new LsReferenceTheme(7)
});
events.Chroma.Add(new FixedKeyframe<float>
{
    Time = 0.0f,
    Value = 0.0f
});
events.Bloom.Add(new FixedKeyframe<float>
{
    Time = 0.0f,
    Value = 0.0f
});
events.Vignette.Add(new FixedKeyframe<VignetteData>
{
    Time = 0.0f,
    Value = new VignetteData()
});
events.LensDistortion.Add(new FixedKeyframe<float>
{
    Time = 0.0f,
    Value = 0.0f
});
events.Grain.Add(new FixedKeyframe<GrainData>
{
    Time = 0.0f,
    Value = new GrainData()
});

using var standardOutput = Console.OpenStandardOutput();
LsSerialization.WriteBeatmap(lsBeatmap, standardOutput);