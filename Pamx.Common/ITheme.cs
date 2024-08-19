using System.Drawing;

namespace Pamx.Common;

public interface ITheme : IReference<ITheme>
{
    ITheme IReference<ITheme>.Value => this;
    string Name { get; set; }
    IList<Color> Player { get; set; }
    IList<Color> Object { get; set; }
    IList<Color> Effect { get; set; }
    IList<Color> BackgroundObject { get; set; }
    IList<Color> ParallaxObject { get; set; }
    Color Background { get; set; }
    Color Gui { get; set; }
    Color GuiAccent { get; set; }
}