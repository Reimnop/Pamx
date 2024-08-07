using System.Drawing;

namespace Pamx.Common;

public interface ITheme : IReference<ITheme>
{
    ITheme IReference<ITheme>.Value => this;
    string Name { get; set; }
    IList<Color> Player { get; }
    IList<Color> Object { get; }
    IList<Color> Effect { get; }
    IList<Color> BackgroundObject { get; }
    IList<Color> ParallaxObject { get; }
    Color Background { get; set; }
    Color Gui { get; set; }
    Color GuiAccent { get; set; }
}