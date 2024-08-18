using Pamx.Common;
using Pamx.Common.Data;
using Pamx.Common.Implementation;

namespace Pamx.Vg;

public class VgBeatmap : IBeatmap
{
    public EditorSettings EditorSettings { get; set; }
    public IList<Trigger> Triggers { get; } = [];
    public IList<EditorPrefabSpawn> PrefabSpawns { get; } = [];
    public IList<ICheckpoint> Checkpoints { get; } = [];
    public IList<IMarker> Markers { get; } = [];
    public IParallax Parallax { get; set; } = new Parallax();
    public IList<BackgroundObject> BackgroundObjects { get; } = [];
    public IList<IPrefab> Prefabs { get; } = [];
    public IList<ITheme> Themes { get; } = [];
    public IList<IPrefabObject> PrefabObjects { get; } = [];
    public IList<IObject> Objects { get; } = [];
    public IBeatmapEvents Events { get; set; } = new BeatmapEvents();
}