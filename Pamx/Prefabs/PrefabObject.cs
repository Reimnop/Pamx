using System.Numerics;
using Pamx.Editor;

namespace Pamx.Prefabs;

/// <summary>
/// Represents an instance object of a prefab in the beatmap.
/// </summary>
public sealed class PrefabObject : IIdentifiable<string>
{
    /// <summary>
    /// The ID of the prefab object.
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// The ID of the prefab which this object is an instance of.
    /// </summary>
    public string PrefabId { get; set; } = string.Empty;

    /// <summary>
    /// The spawn time of the prefab object.
    /// </summary>
    public float StartTime { get; set; }

    /// <summary>
    /// The prefab object's editor settings.
    /// </summary>
    public ObjectEditorSettings EditorSettings { get; set; } = new();

    /// <summary>
    /// The prefab object's position
    /// </summary>
    public Vector2 Position { get; set; } = Vector2.Zero;

    /// <summary>
    /// The prefab object's scale
    /// </summary>
    public Vector2 Scale { get; set; } = Vector2.One;

    /// <summary>
    /// The prefab object's rotation
    /// </summary>
    public float Rotation { get; set; }
}