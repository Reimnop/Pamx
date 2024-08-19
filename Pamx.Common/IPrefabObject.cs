using System.Numerics;
using Pamx.Common.Data;

namespace Pamx.Common;

/// <summary>
/// Represents a prefab instance object in the beatmap
/// </summary>
public interface IPrefabObject
{
    /// <summary>
    /// The prefab object's name
    /// </summary>
    float Time { get; set; }
    
    /// <summary>
    /// The prefab of which this object is an instance of
    /// </summary>
    IReference<IPrefab> Prefab { get; set; }
    
    /// <summary>
    /// The prefab object's editor settings
    /// </summary>
    ObjectEditorSettings EditorSettings { get; set; }
    
    /// <summary>
    /// The prefab object's position
    /// </summary>
    Vector2 Position { get; set; }
    
    /// <summary>
    /// The prefab object's scale
    /// </summary>
    Vector2 Scale { get; set; }
    
    /// <summary>
    /// The prefab object's rotation
    /// </summary>
    float Rotation { get; set; }
}