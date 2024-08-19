using System.Numerics;
using Pamx.Common.Data;

namespace Pamx.Common;

/// <summary>
/// Represents the event keyframes of effects in the beatmap
/// </summary>
public interface IBeatmapEvents
{
    /// <summary>
    /// Camera movement keyframes
    /// </summary>
    IList<FixedKeyframe<Vector2>> Movement { get; set; }
    
    /// <summary>
    /// Camera zoom keyframes
    /// </summary>
    IList<FixedKeyframe<float>> Zoom { get; set; }
    
    /// <summary>
    /// Camera rotation keyframes
    /// </summary>
    IList<FixedKeyframe<float>> Rotation { get; set; }
    
    /// <summary>
    /// Camera shake keyframes
    /// </summary>
    IList<FixedKeyframe<float>> Shake { get; set; }
    
    /// <summary>
    /// Theme keyframes
    /// </summary>
    IList<FixedKeyframe<IReference<ITheme>>> Theme { get; set; }
    
    /// <summary>
    /// Chroma keyframes
    /// </summary>
    IList<FixedKeyframe<float>> Chroma { get; set; }
    
    /// <summary>
    /// Bloom keyframes
    /// </summary>
    IList<FixedKeyframe<BloomData>> Bloom { get; set; }
    
    /// <summary>
    /// Vignette keyframes
    /// </summary>
    IList<FixedKeyframe<VignetteData>> Vignette { get; set; }
    
    /// <summary>
    /// Lens keyframes
    /// </summary>
    IList<FixedKeyframe<LensDistortionData>> LensDistortion { get; set; }
    
    /// <summary>
    /// Grain keyframes
    /// </summary>
    IList<FixedKeyframe<GrainData>> Grain { get; set; }
    
    /// <summary>
    /// Gradient keyframes
    /// </summary>
    IList<FixedKeyframe<GradientData>> Gradient { get; set; }
    
    /// <summary>
    /// Glitch keyframes
    /// </summary>
    IList<FixedKeyframe<GlitchData>> Glitch { get; set; }
    
    /// <summary>
    /// Hue keyframes
    /// </summary>
    IList<FixedKeyframe<float>> Hue { get; set; }
    
    /// <summary>
    /// Player force keyframes
    /// </summary>
    IList<FixedKeyframe<Vector2>> Player { get; set; }
}