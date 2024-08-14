namespace Pamx.Common.Implementation;

public class Parallax : IParallax
{
    public int? DepthOfField { get; set; }
    public IList<IParallaxLayer> Layers { get; } = [];
}