using System.Numerics;
using Pamx.Common.Data;

namespace Pamx.Common.Implementation;

public class BeatmapEvents : IBeatmapEvents
{
    public IList<FixedKeyframe<Vector2>> Movement { get; } = [];
    public IList<FixedKeyframe<float>> Zoom { get; } = [];
    public IList<FixedKeyframe<float>> Rotation { get; } = [];
    public IList<FixedKeyframe<float>> Shake { get; } = [];
    public IList<FixedKeyframe<IReference<ITheme>>> Theme { get; } = [];
    public IList<FixedKeyframe<float>> Chroma { get; } = [];
    public IList<FixedKeyframe<BloomData>> Bloom { get; } = [];
    public IList<FixedKeyframe<VignetteData>> Vignette { get; } = [];
    public IList<FixedKeyframe<LensDistortionData>> LensDistortion { get; } = [];
    public IList<FixedKeyframe<GrainData>> Grain { get; } = [];
    public IList<FixedKeyframe<GradientData>> Gradient { get; } = [];
    public IList<FixedKeyframe<GlitchData>> Glitch { get; } = [];
    public IList<FixedKeyframe<float>> Hue { get; } = [];
    public IList<FixedKeyframe<Vector2>> Player { get; } = [];
}