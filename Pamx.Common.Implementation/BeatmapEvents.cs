using System.Numerics;
using Pamx.Common.Data;

namespace Pamx.Common.Implementation;

/// <inheritdoc />
public class BeatmapEvents : IBeatmapEvents
{
    public IList<FixedKeyframe<Vector2>> Movement { get; set; } = [];
    public IList<FixedKeyframe<float>> Zoom { get; set; } = [];
    public IList<FixedKeyframe<float>> Rotation { get; set; } = [];
    public IList<FixedKeyframe<float>> Shake { get; set; } = [];
    public IList<FixedKeyframe<IReference<ITheme>>> Theme { get; set; } = [];
    public IList<FixedKeyframe<float>> Chroma { get; set; } = [];
    public IList<FixedKeyframe<BloomData>> Bloom { get; set; } = [];
    public IList<FixedKeyframe<VignetteData>> Vignette { get; set; } = [];
    public IList<FixedKeyframe<LensDistortionData>> LensDistortion { get; set; } = [];
    public IList<FixedKeyframe<GrainData>> Grain { get; set; } = [];
    public IList<FixedKeyframe<GradientData>> Gradient { get; set; } = [];
    public IList<FixedKeyframe<GlitchData>> Glitch { get; set; } = [];
    public IList<FixedKeyframe<float>> Hue { get; set; } = [];
    public IList<FixedKeyframe<Vector2>> Player { get; set; } = [];
}