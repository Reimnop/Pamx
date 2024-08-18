using Pamx.Common;
using Pamx.Common.Implementation;
using Pamx.Ls;
using Pamx.Vg;

namespace Pamx;

public static class Asset
{
    public static IReference<IObject> CameraParent { get; } = new BeatmapReferenceObject("camera");
    
    public static IBeatmap CreateBeatmap()
        => new Beatmap();

    public static IPrefab CreatePrefab(string? id = null)
        => string.IsNullOrEmpty(id)
            ? new Prefab()
            : new BeatmapPrefab(id);

    public static ITheme CreateTheme()
        => new Theme();

    public static ITheme CreateTheme(int id)
        => new LsBeatmapTheme(id);
    
    public static ITheme CreateTheme(string id)
        => new VgBeatmapTheme(id);

    public static IReference<ITheme> CreateReferenceTheme(int id)
        => new LsReferenceTheme(id);
    
    public static IReference<ITheme> CreateReferenceTheme(string id)
        => new VgReferenceTheme(id);
    
    public static IObject CreateObject(string? id = null)
    {
        var newId = string.IsNullOrEmpty(id)
            ? RandomUtil.GenerateId()
            : id;
        return new BeatmapObject(newId);
    }
}