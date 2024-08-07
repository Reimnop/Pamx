using Pamx.Common;
using Pamx.Common.Data;

namespace Pamx.Ls;

public class LsBeatmap : IBeatmap
{
    public EditorSettings EditorSettings { get; set; }
    public IList<Trigger> Triggers { get; } = [];
    public IList<EditorPrefabSpawn> PrefabSpawns { get; } = [];
    public IList<Checkpoint> Checkpoints { get; } = [];
    public IList<Marker> Markers { get; } = [];
    public IList<BackgroundObject> BackgroundObjects { get; } = [];
    public IList<IPrefab> Prefabs { get; } = [];
    public IList<ITheme> Themes { get; } = [];
    public IList<IPrefabObject> PrefabObjects { get; } = [];
    public IList<IObject> Objects { get; } = [];
}