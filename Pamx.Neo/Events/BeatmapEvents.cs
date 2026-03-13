using System.Numerics;
using Pamx.Neo.Keyframes;

namespace Pamx.Neo.Events;

/// <summary>
/// Represents the event keyframes of the beatmap itself.
/// </summary>
public sealed class BeatmapEvents
{
    /// <summary>
    /// Movement event keyframes.
    /// </summary>
    public List<FixedKeyframe<Vector2>> Move { get; set; } = [new(new Vector2(0.0f, 0.0f))];

    /// <summary>
    /// Zoom event keyframes.
    /// </summary>
    public List<FixedKeyframe<float>> Zoom { get; set; } = [new(30.0f)];

    /// <summary>
    /// Rotation event keyframes.
    /// </summary>
    public List<FixedKeyframe<float>> Rotate { get; set; } = [new(0.0f)];

    /// <summary>
    /// Shake event keyframes.
    /// </summary>
    public List<FixedKeyframe<float>> Shake { get; set; } = [new(0.0f)];

    /// <summary>
    /// Theme ID event keyframes.
    /// </summary>
    public List<FixedKeyframe<string>> Theme { get; set; } = [new("0")];

    /// <summary>
    /// Chromatic aberration event keyframes.
    /// </summary>
    public List<FixedKeyframe<float>> Chromatic { get; set; } = [new(0.0f)];

    /// <summary>
    /// Bloom event keyframes.
    /// </summary>
    public List<FixedKeyframe<BloomValue>> Bloom { get; set; } = [new(BloomValue.Zero)];

    /// <summary>
    /// Vignette event keyframes.
    /// </summary>
    public List<FixedKeyframe<VignetteValue>> Vignette { get; set; } = [new(VignetteValue.Zero)];

    /// <summary>
    /// Vignette event keyframes.
    /// </summary>
    public List<FixedKeyframe<LensDistortionValue>> LensDistortion { get; set; } = [new(LensDistortionValue.Zero)];

    /// <summary>
    /// Grain event keyframes.
    /// </summary>
    public List<FixedKeyframe<GrainValue>> Grain { get; set; } = [new(GrainValue.Zero)];
    
    /// <summary>
    /// Gradient event keyframes.
    /// </summary>
    public List<FixedKeyframe<GradientValue>> Gradient { get; set; } = [new(GradientValue.Zero)];
    
    /// <summary>
    /// Gradient event keyframes.
    /// </summary>
    public List<FixedKeyframe<GlitchValue>> Glitch { get; set; } = [new(GlitchValue.Zero)];

    /// <summary>
    /// Hue shift event keyframes.
    /// </summary>
    public List<FixedKeyframe<float>> Hue { get; set; } = [new(0.0f)];

    /// <summary>
    /// Player force event keyframes.
    /// </summary>
    public List<FixedKeyframe<Vector2>> Player { get; set; } = [new(Vector2.Zero)];
}