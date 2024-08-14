namespace Pamx.Common;

public interface IParallaxLayer
{
    int Depth { get; set; }
    int Color { get; set; }
    IList<IParallaxObject> Objects { get; }
}