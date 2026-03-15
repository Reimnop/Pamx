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
    public List<FixedKeyframe<Vector2>> Move { get; set; } = [];

    /// <summary>
    /// Zoom event keyframes.
    /// </summary>
    public List<FixedKeyframe<float>> Zoom { get; set; } = [];

    /// <summary>
    /// Rotation event keyframes.
    /// </summary>
    public List<FixedKeyframe<float>> Rotate { get; set; } = [];

    /// <summary>
    /// Shake event keyframes.
    /// </summary>
    public List<FixedKeyframe<float>> Shake { get; set; } = [];

    /// <summary>
    /// Theme ID event keyframes.
    /// </summary>
    public List<FixedKeyframe<string>> Theme { get; set; } = [];

    /// <summary>
    /// Chromatic aberration event keyframes.
    /// </summary>
    public List<FixedKeyframe<float>> Chromatic { get; set; } = [];

    /// <summary>
    /// Bloom event keyframes.
    /// </summary>
    public List<FixedKeyframe<BloomValue>> Bloom { get; set; } = [];

    /// <summary>
    /// Vignette event keyframes.
    /// </summary>
    public List<FixedKeyframe<VignetteValue>> Vignette { get; set; } = [];

    /// <summary>
    /// Vignette event keyframes.
    /// </summary>
    public List<FixedKeyframe<LensDistortionValue>> LensDistortion { get; set; } = [];

    /// <summary>
    /// Grain event keyframes.
    /// </summary>
    public List<FixedKeyframe<GrainValue>> Grain { get; set; } = [];
    
    /// <summary>
    /// Gradient event keyframes.
    /// </summary>
    public List<FixedKeyframe<GradientValue>> Gradient { get; set; } = [];
    
    /// <summary>
    /// Gradient event keyframes.
    /// </summary>
    public List<FixedKeyframe<GlitchValue>> Glitch { get; set; } = [];

    /// <summary>
    /// Hue shift event keyframes.
    /// </summary>
    public List<FixedKeyframe<float>> Hue { get; set; } = [];

    /// <summary>
    /// Player force event keyframes.
    /// </summary>
    public List<FixedKeyframe<Vector2>> Player { get; set; } = [];
}