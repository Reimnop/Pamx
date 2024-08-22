namespace Pamx.Common.Data;

/// <summary>
/// The settings for quick spawning prefabs in the editor
/// </summary>
public struct EditorPrefabSpawn()
{
    /// <summary>
    /// Whether the current prefab spawn is expanded
    /// </summary>
    public bool Expanded { get; set; } = false;
    
    /// <summary>
    /// Whether the current prefab spawn is active
    /// </summary>
    public bool Active { get; set; } = false;
    
    /// <summary>
    /// The prefab reference to spawn
    /// </summary>
    public IReference<IPrefab>? Prefab { get; set; } = null;
    
    /// <summary>
    /// The keycodes to be used to spawn the prefab
    /// </summary>
    public IList<int> Keycodes { get; set; } = [];
}