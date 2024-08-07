using System.Drawing;
using Pamx.Common;

namespace Pamx.Ls;

public class LsTheme : ITheme, IIdentifiable<int>
{
    public int Id { get; } = LsRandomUtil.GenerateThemeId();
    public string Name { get; set; } = string.Empty;
    public IList<Color> Player { get; } = [];
    public IList<Color> Object { get; } = [];
    public IList<Color> Effect { get; } = [];
    public IList<Color> BackgroundObject { get; } = [];
    public IList<Color> ParallaxObject { get; } = [];
    public Color Background { get; set; }
    public Color Gui { get; set; }
    public Color GuiAccent { get; set; }
}