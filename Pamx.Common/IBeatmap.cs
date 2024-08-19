using Pamx.Common.Data;

namespace Pamx.Common;

public interface IBeatmap
{
    EditorSettings EditorSettings { get; set; }
    IList<Trigger> Triggers { get; set; }
    IList<EditorPrefabSpawn> PrefabSpawns { get; set; }
    IList<ICheckpoint> Checkpoints { get; set; }
    IList<IMarker> Markers { get; set; }
    IParallax Parallax { get; set; }
    IList<BackgroundObject> BackgroundObjects { get; set; }
    IList<IPrefab> Prefabs { get; set; }
    IList<ITheme> Themes { get; set; }
    IList<IPrefabObject> PrefabObjects { get; set; }
    IList<IObject> Objects { get; set; }
    IBeatmapEvents Events { get; set; }
}