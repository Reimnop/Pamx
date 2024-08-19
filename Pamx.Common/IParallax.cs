namespace Pamx.Common;

public interface IParallax
{
    int? DepthOfField { get; set; }
    IList<IParallaxLayer> Layers { get; set; }
}