using System.Diagnostics.CodeAnalysis;
using Pamx.Common;
using Pamx.Common.Implementation;
using Pamx.Ls;
using Pamx.Vg;

namespace Pamx;

public static class ThemeUtil
{
    public static ITheme CloneWithLsId(this ITheme theme, int? id = null)
    {
        var newId = id ?? (theme.TryGetLsId(out var themeId) ? themeId : RandomUtil.GenerateLsThemeId());
        var newTheme = new LsBeatmapTheme(newId)
        {
            Name = theme.Name,
            Background = theme.Background,
            Gui = theme.Gui,
            GuiAccent = theme.GuiAccent,
        };
        newTheme.Player.AddRange(theme.Player);
        newTheme.Object.AddRange(theme.Object);
        newTheme.Effect.AddRange(theme.Effect);
        newTheme.BackgroundObject.AddRange(theme.BackgroundObject);
        newTheme.ParallaxObject.AddRange(theme.ParallaxObject);
        return newTheme;
    }
    
    public static ITheme CloneWithVgId(this ITheme theme, string? id = null)
    {
        var newId = string.IsNullOrEmpty(id)
            ? theme.TryGetVgId(out var themeId)
                ? themeId
                : RandomUtil.GenerateId()
            : id;
        var newTheme = new VgBeatmapTheme(newId)
        {
            Name = theme.Name,
            Background = theme.Background,
            Gui = theme.Gui,
            GuiAccent = theme.GuiAccent,
        };
        newTheme.Player.AddRange(theme.Player);
        newTheme.Object.AddRange(theme.Object);
        newTheme.Effect.AddRange(theme.Effect);
        newTheme.BackgroundObject.AddRange(theme.BackgroundObject);
        newTheme.ParallaxObject.AddRange(theme.ParallaxObject);
        return newTheme;
    }
    
    private static bool TryGetVgId(this ITheme theme, [NotNullWhen(true)] out string? id)
    {
        if (theme is IIdentifiable<string> identifiable)
        {
            id = identifiable.Id;
            return true;
        }
        
        id = null;
        return false;
    }
    
    private static bool TryGetLsId(this ITheme theme, out int id)
    {
        if (theme is IIdentifiable<int> identifiable)
        {
            id = identifiable.Id;
            return true;
        }
        
        id = 0;
        return false;
    }
}