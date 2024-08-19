using System.Numerics;
using Pamx.Common.Data;

namespace Pamx.Common;

public interface IBeatmapEvents
{
    IList<FixedKeyframe<Vector2>> Movement { get; set; }
    IList<FixedKeyframe<float>> Zoom { get; set; }
    IList<FixedKeyframe<float>> Rotation { get; set; }
    IList<FixedKeyframe<float>> Shake { get; set; }
    IList<FixedKeyframe<IReference<ITheme>>> Theme { get; set; }
    IList<FixedKeyframe<float>> Chroma { get; set; }
    IList<FixedKeyframe<BloomData>> Bloom { get; set; }
    IList<FixedKeyframe<VignetteData>> Vignette { get; set; }
    IList<FixedKeyframe<LensDistortionData>> LensDistortion { get; set; }
    IList<FixedKeyframe<GrainData>> Grain { get; set; }
    IList<FixedKeyframe<GradientData>> Gradient { get; set; }
    IList<FixedKeyframe<GlitchData>> Glitch { get; set; }
    IList<FixedKeyframe<float>> Hue { get; set; }
    IList<FixedKeyframe<Vector2>> Player { get; set; }
}