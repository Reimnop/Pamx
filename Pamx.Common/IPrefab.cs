using Pamx.Common.Enum;

namespace Pamx.Common;

/// <summary>
/// Represents a prefab in the game
/// </summary>
public interface IPrefab : IReference<IPrefab>
{
    /// <inheritdoc />
    IPrefab IReference<IPrefab>.Value => this;
    
    /// <summary>
    /// The prefab's name
    /// </summary>
    string Name { get; set; }
    
    /// <summary>
    /// The prefab's description
    /// </summary>
    string? Description { get; set; }
    
    /// <summary>
    /// The prefab's preview. Must be a base64 png, or set to null to disable preview
    /// </summary>
    string? Preview { get; set; }
    
    /// <summary>
    /// The prefab's time offset
    /// </summary>
    float Offset { get; set; }
    
    /// <summary>
    /// The prefab's type
    /// </summary>
    PrefabType Type { get; set; }
    
    /// <summary>
    /// The prefab's objects
    /// </summary>
    IList<IObject> BeatmapObjects { get; set; }
}