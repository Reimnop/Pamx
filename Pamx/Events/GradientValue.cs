namespace Pamx.Events;

/// <summary>
/// The value of the gradient overlay event keyframe.
/// </summary>
public record struct GradientValue()
{
    public static readonly GradientValue Zero = new();

    /// <summary>
    /// The intensity of the gradient overlay effect.
    /// </summary>
    public float Intensity { get; set; } = 0.0f;

    /// <summary>
    /// The rotation of the gradient overlay, in degrees.
    /// </summary>
    public float Rotation { get; set; } = 0.0f;

    /// <summary>
    /// The first color of the gradient overlay.
    /// </summary>
    public int StartColor { get; set; } = 9;

    /// <summary>
    /// The second color of the gradient overlay.
    /// </summary>
    public int EndColor { get; set; } = 9;

    /// <summary>
    /// The mode of the gradient overlay.
    /// </summary>
    public GradientOverlayMode Mode { get; set; } = GradientOverlayMode.Multiply;
}

/// <summary>
/// The mode of the gradient overlay.
/// </summary>
public enum GradientOverlayMode
{
    /// <summary>
    /// Linear gradient overlay mode.
    /// </summary>
    Linear,

    /// <summary>
    /// Additive gradient overlay mode.
    /// </summary>
    Additive,

    /// <summary>
    /// Multiply gradient overlay mode.
    /// </summary>
    Multiply,

    /// <summary>
    /// Screen gradient overlay mode.
    /// </summary>
    Screen
}