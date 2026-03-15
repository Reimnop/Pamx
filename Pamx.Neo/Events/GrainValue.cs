namespace Pamx.Neo.Events;

/// <summary>
/// The value of the grain event keyframe.
/// </summary>
public record struct GrainValue()
{
    public static readonly GrainValue Zero = new();

    /// <summary>
    /// The intensity of the grain effect.
    /// </summary>
    public float Intensity { get; set; } = 0.0f;

    /// <summary>
    /// The size of each individual grain.
    /// </summary>
    public float Size { get; set; } = 0.0f;

    /// <summary>
    /// How much of the original image to mix with the grain effect.
    /// </summary>
    public float Mix { get; set; } = 0.0f;

    /// <summary>
    /// Whether the grain effect should be colored. (legacy option, not used in modern versions of PA)
    /// </summary>
    public bool IsColored { get; set; } = false;
}