using System.Numerics;
using Pamx.Neo.Keyframes;

namespace Pamx.Neo.Events;

/// <summary>
/// Represents the event keyframes of the beatmap itself.
/// </summary>
public sealed class BeatmapEvents
{
    public List<FixedKeyframe<Vector2>> Move { get; set; } = [new(new Vector2(0.0f, 0.0f))];
    public List<FixedKeyframe<float>> Zoom { get; set; } = [new(30.0f)];
    public List<FixedKeyframe<float>> Rotate { get; set; } = [new(0.0f)];
    public List<FixedKeyframe<float>> Shake { get; set; } = [new(0.0f)];
    public List<FixedKeyframe<string>> Theme { get; set; } = [new("0")];
    public List<FixedKeyframe<float>> Chroma { get; set; } = [new(0.0f)];
}