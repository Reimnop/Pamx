using System.Numerics;
using Pamx.Common;
using Pamx.Common.Data;

namespace Pamx.Vg;

public class VgBeatmapEvents : IBeatmapEvents
{
    public IList<FixedKeyframe<Vector2>> Movement { get; } = [];
    public IList<FixedKeyframe<float>> Zoom { get; } = [];
    public IList<FixedKeyframe<float>> Rotation { get; } = [];
    public IList<FixedKeyframe<float>> Shake { get; } = [];
    public IList<FixedKeyframe<IReference<ITheme>>> Theme { get; } = [];
    public IList<FixedKeyframe<float>> Chroma { get; } = [];
    public IList<FixedKeyframe<float>> Bloom { get; } = [];
    public IList<FixedKeyframe<VignetteData>> Vignette { get; } = [];
    public IList<FixedKeyframe<float>> LensDistortion { get; } = [];
    public IList<FixedKeyframe<GrainData>> Grain { get; } = [];
}