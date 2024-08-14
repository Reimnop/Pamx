using Pamx.Common.Enum;

namespace Pamx.Common.Data;

public struct GradientData()
{
    public float Intensity { get; set; } = 0.0f;
    public float Rotation { get; set; } = 0.0f;
    public int ColorA { get; set; } = 0;
    public int ColorB { get; set; } = 0;
    public GradientOverlayMode Mode { get; set; } = GradientOverlayMode.Linear;
}