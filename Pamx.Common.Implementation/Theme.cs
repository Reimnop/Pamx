using System.Drawing;

namespace Pamx.Common.Implementation;

public class Theme : ITheme
{
    public string Name { get; set; } = string.Empty;
    public IList<Color> Player { get; set; } = [];
    public IList<Color> Object { get; set; } = [];
    public IList<Color> Effect { get; set; } = [];
    public IList<Color> BackgroundObject { get; set; } = [];
    public IList<Color> ParallaxObject { get; set; } = [];
    public Color Background { get; set; }
    public Color Gui { get; set; }
    public Color GuiAccent { get; set; }
}