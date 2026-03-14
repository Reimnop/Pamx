namespace Pamx.Neo.Events;

/// <summary>
/// The value of the glitch event keyframe.
/// </summary>
public record struct GlitchValue()
{
    public static readonly GlitchValue Zero = new();

    /// <summary>
    /// The intensity of the glitch.
    /// </summary>
    public float Intensity { get; set; } = 0.0f;

    /// <summary>
    /// The speed of the glitch.
    /// </summary>
    public float Speed { get; set; } = 1.0f;

    /// <summary>
    /// The width of the glitch.
    /// </summary>
    public float Width { get; set; } = 0.88f;
}