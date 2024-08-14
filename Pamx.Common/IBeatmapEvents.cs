using System.Numerics;
using Pamx.Common.Data;

namespace Pamx.Common;

public interface IBeatmapEvents
{
    IList<FixedKeyframe<Vector2>> Movement { get; }
    IList<FixedKeyframe<float>> Zoom { get; }
    IList<FixedKeyframe<float>> Rotation { get; }
    IList<FixedKeyframe<float>> Shake { get; }
    IList<FixedKeyframe<IReference<ITheme>>> Theme { get; }
    IList<FixedKeyframe<float>> Chroma { get; }
    IList<FixedKeyframe<BloomData>> Bloom { get; }
    IList<FixedKeyframe<VignetteData>> Vignette { get; }
    IList<FixedKeyframe<LensDistortionData>> LensDistortion { get; }
    IList<FixedKeyframe<GrainData>> Grain { get; }
    IList<FixedKeyframe<GradientData>> Gradient { get; }
    IList<FixedKeyframe<GlitchData>> Glitch { get; }
    IList<FixedKeyframe<float>> Hue { get; }
    IList<FixedKeyframe<Vector2>> Player { get; }
}