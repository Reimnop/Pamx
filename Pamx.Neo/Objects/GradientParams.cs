namespace Pamx.Neo.Objects;

/// <summary>
/// The beatmap object's gradient parameters.
/// </summary>
public sealed class GradientParams
{
    /// <summary>
    /// The type of the gradient
    /// </summary>
    public GradientType Type { get; set; } = GradientType.None;
    
    /// <summary>
    /// The scale of the gradient.
    /// </summary>
    public float Scale { get; set; } = 1.0f;
    
    /// <summary>
    /// The rotation of the gradient, in degrees.
    /// </summary>
    public float Rotation { get; set; } = 0.0f;
}