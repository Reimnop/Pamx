using Pamx.Common.Data;

namespace Pamx.Common;

/// <summary>
/// Represents a beatmap with various settings, triggers, objects, and themes.
/// </summary>
public interface IBeatmap
{
    /// <summary>
    /// The beatmap's editor settings
    /// </summary>
    EditorSettings EditorSettings { get; set; }
    
    /// <summary>
    /// The beatmap's triggers
    /// </summary>
    IList<Trigger> Triggers { get; set; }
    
    /// <summary>
    /// The beatmap's editor prefab spawns
    /// </summary>
    IList<EditorPrefabSpawn> PrefabSpawns { get; set; }
    
    /// <summary>
    /// The beatmap's checkpoints
    /// </summary>
    IList<ICheckpoint> Checkpoints { get; set; }
    
    /// <summary>
    /// The beatmap's markers
    /// </summary>
    IList<IMarker> Markers { get; set; }
    
    /// <summary>
    /// The beatmap's parallax settings
    /// </summary>
    IParallax Parallax { get; set; }
    
    /// <summary>
    /// The beatmap's background objects
    /// </summary>
    IList<BackgroundObject> BackgroundObjects { get; set; }
    
    /// <summary>
    /// The beatmap's prefabs
    /// </summary>
    IList<IPrefab> Prefabs { get; set; }
    
    /// <summary>
    /// The beatmap's themes
    /// </summary>
    IList<ITheme> Themes { get; set; }
    
    /// <summary>
    /// The beatmap's prefab objects
    /// </summary>
    IList<IPrefabObject> PrefabObjects { get; set; }
    
    /// <summary>
    /// The beatmap's objects
    /// </summary>
    IList<IObject> Objects { get; set; }
    
    /// <summary>
    /// The beatmap's events
    /// </summary>
    IBeatmapEvents Events { get; set; }
}