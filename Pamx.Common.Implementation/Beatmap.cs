using Pamx.Common.Data;

namespace Pamx.Common.Implementation;

public class Beatmap : IBeatmap
{
    public EditorSettings EditorSettings { get; set; }
    public IList<Trigger> Triggers { get; set; } = [];
    public IList<EditorPrefabSpawn> PrefabSpawns { get; set; } = [];
    public IList<ICheckpoint> Checkpoints { get; set; } = [];
    public IList<IMarker> Markers { get; set; } = [];
    public IParallax Parallax { get; set; } = new Parallax();
    public IList<BackgroundObject> BackgroundObjects { get; set; } = [];
    public IList<IPrefab> Prefabs { get; set; } = [];
    public IList<ITheme> Themes { get; set; } = [];
    public IList<IPrefabObject> PrefabObjects { get; set; } = [];
    public IList<IObject> Objects { get; set; } = [];
    public IBeatmapEvents Events { get; set; } = new BeatmapEvents();
}