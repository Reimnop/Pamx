using System.Drawing;

namespace Pamx.Common;

/// <summary>
/// Represents a theme in the game
/// </summary>
public interface ITheme : IReference<ITheme>
{
    /// <inheritdoc />
    ITheme IReference<ITheme>.Value => this;
    
    /// <summary>
    /// The theme's name
    /// </summary>
    string Name { get; set; }
    
    /// <summary>
    /// The colors array of the player. There should be 9 colors in this array
    /// </summary>
    IList<Color> Player { get; set; }
    
    /// <summary>
    /// The colors array of the objects. There should be 9 colors in this array
    /// </summary>
    IList<Color> Object { get; set; }
    
    /// <summary>
    /// The colors array of the effect. There should be 9 colors in this array
    /// </summary>
    IList<Color> Effect { get; set; }
    
    /// <summary>
    /// The colors array of the background objects. There should be 9 colors in this array
    /// </summary>
    IList<Color> BackgroundObject { get; set; }
    
    /// <summary>
    /// The colors array of the parallax objects. There should be 9 colors in this array
    /// </summary>
    IList<Color> ParallaxObject { get; set; }
    
    /// <summary>
    /// The background color
    /// </summary>
    Color Background { get; set; }
    
    /// <summary>
    /// The GUI color
    /// </summary>
    Color Gui { get; set; }
    
    /// <summary>
    /// The GUI accent color
    /// </summary>
    Color GuiAccent { get; set; }
}