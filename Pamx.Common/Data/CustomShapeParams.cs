using JetBrains.Annotations;

namespace Pamx.Common.Data;

/// <summary>
/// The object's custom shape parameters
/// </summary>
public struct CustomShapeParams()
{
    /// <summary>
    /// The number of sides. Can range from 3 to 32
    /// </summary>
    [ValueRange(3, 32)]
    public int Sides { get; set; }

    /// <summary>
    /// The roundness of the shape. Can range from 0 to 1
    /// </summary>
    [ValueRange(0, 1)]
    public float Roundness { get; set; }

    /// <summary>
    /// The thickness of the shape. Can range from 0 to 1
    /// </summary>
    [ValueRange(0, 1)]
    public float Thickness { get; set; }

    /// <summary>
    /// The number of slices. Can range from 1 to the number of <see cref="Sides"/>
    /// </summary>
    [ValueRange(1, 32)]
    public int Slices { get; set; }

    /// <summary>
    /// Creates new <see cref="CustomShapeParams"/>
    /// </summary>
    /// <param name="sides">The number of sides. Can range from 3 to 32</param>
    /// <param name="roundness">The roundness of the shape. Can range from 0 to 1</param>
    /// <param name="thickness">The thickness of the shape. Can range from 0 to 1</param>
    /// <param name="slices">The number of slices. Can range from 1 to the number of <see cref="Sides"/></param>
    public CustomShapeParams(int sides, float roundness, float thickness, int slices) : this()
    {
        Sides = sides;
        Roundness = roundness;
        Thickness = thickness;
        Slices = slices;
    }
}