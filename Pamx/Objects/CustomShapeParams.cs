namespace Pamx.Objects;

/// <summary>
/// The beatmap object's custom shape parameters.
/// </summary>
public sealed class CustomShapeParams
{
    /// <summary>
    /// The number of sides. Can range from 3 to 32.
    /// </summary>
    public int Sides { get; set; }

    /// <summary>
    /// The roundness of the shape. Can range from 0 to 1.
    /// </summary>
    public float Roundness { get; set; }

    /// <summary>
    /// The thickness of the shape. Can range from 0 to 1.
    /// </summary>
    public float Thickness { get; set; }

    /// <summary>
    /// The number of slices. Can range from 1 to the number of <see cref="Sides"/>.
    /// </summary>
    public int Slices { get; set; }
}