using Pamx.Common.Data;

namespace Pamx.Common;

public interface IBeatmap
{
    EditorSettings EditorSettings { get; set; }
    IList<Trigger> Triggers { get; }
    IList<EditorPrefabSpawn> PrefabSpawns { get; }
    IList<Checkpoint> Checkpoints { get; }
    IList<Marker> Markers { get; }
    IList<BackgroundObject> BackgroundObjects { get; }
    IList<IPrefab> Prefabs { get; }
    IList<ITheme> Themes { get; }
    IList<IPrefabObject> PrefabObjects { get; }
    IList<IObject> Objects { get; }
    
    
    // TODO: Add event keyframes
}