namespace Pamx.Common;

/// <summary>
/// Represents the parallax settings of the beatmap
/// </summary>
public interface IParallax
{
    /// <summary>
    /// Amount of depth of field effect. Set to null to disable
    /// </summary>
    int? DepthOfField { get; set; }
    
    /// <summary>
    /// Layers of the parallax
    /// </summary>
    IList<IParallaxLayer> Layers { get; set; }
}