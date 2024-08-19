namespace Pamx.Common.Implementation;

/// <inheritdoc />
public class ParallaxLayer : IParallaxLayer
{
    public int Depth { get; set; }
    public int Color { get; set; }
    public IList<IParallaxObject> Objects { get; set; } = [];
}