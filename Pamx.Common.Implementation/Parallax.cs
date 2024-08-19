namespace Pamx.Common.Implementation;

/// <inheritdoc />
public class Parallax : IParallax
{
    public int? DepthOfField { get; set; }
    public IList<IParallaxLayer> Layers { get; set; } = [];
}